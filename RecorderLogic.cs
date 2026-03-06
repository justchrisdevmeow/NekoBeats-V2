using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using NAudio.Wave;

namespace NekoBeats
{
    public class RecorderLogic : IDisposable
    {
        private bool isRecording = false;
        private int frameCount = 0;
        private string tempDir;
        private string tempVideoPath;
        private string audioFilePath;
        private string outputPath;
        
        public int RecordingWidth { get; set; } = 1920;
        public int RecordingHeight { get; set; } = 1080;
        public int RecordingFPS { get; set; } = 60;
        public int MaxDurationSeconds { get; set; } = 300;
        
        private WasapiLoopbackCapture audioCapture;
        private WaveFileWriter audioFileWriter;
        private Bitmap frameBuffer;
        private Graphics frameGraphics;
        private VisualizerForm visualizerForm;
        private VisualizerLogic visualizerLogic;
        
        public RecorderLogic(VisualizerForm form)
        {
            visualizerForm = form;
            visualizerLogic = form.Logic;
        }
        
        public bool StartRecording(string outputFilePath, int width, int height, int durationSeconds, int fps)
        {
            if (isRecording) return false;
            
            try
            {
                RecordingWidth = width;
                RecordingHeight = height;
                RecordingFPS = fps;
                MaxDurationSeconds = durationSeconds;
                outputPath = outputFilePath;
                frameCount = 0;
                
                tempDir = Path.Combine(Path.GetTempPath(), "NekoBeatsRecording", Guid.NewGuid().ToString());
                Directory.CreateDirectory(tempDir);
                
                tempVideoPath = Path.Combine(tempDir, "video.raw");
                audioFilePath = Path.Combine(tempDir, "audio.wav");
                
                frameBuffer = new Bitmap(RecordingWidth, RecordingHeight, PixelFormat.Format32bppArgb);
                frameGraphics = Graphics.FromImage(frameBuffer);
                
                InitializeAudioCapture();
                isRecording = true;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to start recording: {ex.Message}", "Recording Error");
                return false;
            }
        }
        
        public void StopRecording()
        {
            if (!isRecording) return;
            isRecording = false;
            
            try
            {
                audioCapture?.StopRecording();
                audioFileWriter?.Dispose();
            }
            catch { }
        }
        
        public void CaptureFrame()
        {
            if (!isRecording) return;
            
            if (frameCount >= MaxDurationSeconds * RecordingFPS)
            {
                StopRecording();
                EncodeToMP4();
                return;
            }
            
            try
            {
                frameGraphics.Clear(Color.Magenta);
                visualizerLogic.Render(frameGraphics, new Size(RecordingWidth, RecordingHeight));
                SaveFrameToRaw(frameBuffer, tempVideoPath);
                frameCount++;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Frame capture error: {ex.Message}", "Recording Error");
                StopRecording();
            }
        }
        
        private void InitializeAudioCapture()
        {
            try
            {
                audioCapture = new WasapiLoopbackCapture();
                audioFileWriter = new WaveFileWriter(audioFilePath, audioCapture.WaveFormat);
                audioCapture.DataAvailable += (s, e) =>
                {
                    if (isRecording)
                        audioFileWriter.Write(e.Buffer, 0, e.BytesRecorded);
                };
                audioCapture.StartRecording();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Audio capture failed: {ex.Message}", "Audio Error");
            }
        }
        
        private void SaveFrameToRaw(Bitmap frame, string rawPath)
        {
            using (FileStream fs = new FileStream(rawPath, FileMode.Append, FileAccess.Write, FileShare.None, 65536))
            {
                BitmapData bmpData = frame.LockBits(
                    new Rectangle(0, 0, frame.Width, frame.Height),
                    ImageLockMode.ReadOnly,
                    PixelFormat.Format32bppArgb);
                
                IntPtr ptr = bmpData.Scan0;
                int bytes = Math.Abs(bmpData.Stride) * frame.Height;
                byte[] rgbValues = new byte[bytes];
                System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);
                
                fs.Write(rgbValues, 0, bytes);
                frame.UnlockBits(bmpData);
            }
        }
        
        public void EncodeToMP4()
        {
            if (!File.Exists(tempVideoPath) || !File.Exists(audioFilePath))
            {
                MessageBox.Show("Video or audio file not found.", "Encoding Error");
                CleanupTempFiles();
                return;
            }
            
            try
            {
                string ffmpegPath = FindFFmpeg();
                if (string.IsNullOrEmpty(ffmpegPath))
                {
                    MessageBox.Show("FFmpeg not found. Install from ffmpeg.org or add to PATH.", "FFmpeg Error");
                    return;
                }
                
                string args = $"-f rawvideo -pixel_format bgra -video_size {RecordingWidth}x{RecordingHeight} -framerate {RecordingFPS} " +
                              $"-i \"{tempVideoPath}\" -i \"{audioFilePath}\" -c:v libx264 -preset fast -c:a aac \"{outputPath}\"";
                
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = ffmpegPath,
                    Arguments = args,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };
                
                using (Process process = Process.Start(psi))
                {
                    process.WaitForExit();
                    
                    if (process.ExitCode == 0)
                    {
                        MessageBox.Show($"Recording saved to:\n{outputPath}", "Success!");
                        CleanupTempFiles();
                    }
                    else
                    {
                        string error = process.StandardError.ReadToEnd();
                        MessageBox.Show($"FFmpeg error:\n{error}", "Encoding Error");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Encoding error: {ex.Message}", "Error");
            }
        }
        
        private string FindFFmpeg()
        {
            string[] locations = new[]
            {
                "ffmpeg.exe",
                "C:\\FFmpeg\\bin\\ffmpeg.exe",
                "C:\\Program Files\\FFmpeg\\bin\\ffmpeg.exe",
                "C:\\Program Files (x86)\\FFmpeg\\bin\\ffmpeg.exe"
            };
            
            foreach (string location in locations)
            {
                if (File.Exists(location))
                    return location;
            }
            
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "where",
                    Arguments = "ffmpeg",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };
                
                using (Process process = Process.Start(psi))
                {
                    string output = process.StandardOutput.ReadToEnd().Trim();
                    if (!string.IsNullOrEmpty(output))
                        return output.Split('\n')[0];
                }
            }
            catch { }
            
            return null;
        }
        
        private void CleanupTempFiles()
        {
            try
            {
                if (Directory.Exists(tempDir))
                    Directory.Delete(tempDir, true);
            }
            catch { }
        }
        
        public bool IsRecording => isRecording;
        public int FramesRecorded => frameCount;
        public int MaxFrames => MaxDurationSeconds * RecordingFPS;
        public double RecordingProgress => MaxFrames > 0 ? (double)frameCount / MaxFrames : 0;
        
        public void Dispose()
        {
            StopRecording();
            frameGraphics?.Dispose();
            frameBuffer?.Dispose();
            audioCapture?.Dispose();
            audioFileWriter?.Dispose();
            CleanupTempFiles();
        }
    }
}
