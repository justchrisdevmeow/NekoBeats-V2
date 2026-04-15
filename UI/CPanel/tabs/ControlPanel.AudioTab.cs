using System.Drawing;
using System.Windows.Forms;

namespace NekoBeats
{
    public partial class ControlPanel
    {
        private TrackBar sensitivityTrack;
        private TrackBar smoothSpeedTrack;
        private TrackBar latencyTrack;

        private void ShowAudioTab(int y)
        {
            var audioGroup = CreateGroupBox(LanguageManager.Get("AudioSettings"), 10, y, 900, 350);
            int gy = 25;

            var systemAudioLabel = new Label { Text = LanguageManager.Get("UsingSystemAudio"), Location = new Point(20, gy), Size = new Size(860, 25), ForeColor = neonCyan, TextAlign = ContentAlignment.MiddleLeft };
            audioGroup.Controls.Add(systemAudioLabel);
            gy += 45;

            var sensitivityLabel = new Label { Text = LanguageManager.Get("Sensitivity"), Location = new Point(20, gy), Size = new Size(140, 20), ForeColor = dimText };
            audioGroup.Controls.Add(sensitivityLabel);
            var sensitivityValue = new Label { Text = ((int)(visualizer.Logic.sensitivity * 100)).ToString(), Location = new Point(800, gy), Size = new Size(70, 20), ForeColor = neonCyan, TextAlign = ContentAlignment.TopRight };
            audioGroup.Controls.Add(sensitivityValue);
            sensitivityTrack = new TrackBar { Location = new Point(170, gy - 5), Size = new Size(620, 45), Minimum = 1, Maximum = 500, Value = (int)(visualizer.Logic.sensitivity * 100), TickStyle = TickStyle.None, BackColor = boxBg };
            sensitivityTrack.ValueChanged += (s, e) => { visualizer.Logic.sensitivity = sensitivityTrack.Value / 100f; sensitivityValue.Text = sensitivityTrack.Value.ToString(); visualizer.Invalidate(); };
            audioGroup.Controls.Add(sensitivityTrack);
            gy += 45;

            var smoothLabel = new Label { Text = LanguageManager.Get("SmoothSpeed"), Location = new Point(20, gy), Size = new Size(140, 20), ForeColor = dimText };
            audioGroup.Controls.Add(smoothLabel);
            var smoothValue = new Label { Text = ((int)(visualizer.Logic.smoothSpeed * 100)).ToString(), Location = new Point(800, gy), Size = new Size(70, 20), ForeColor = neonCyan, TextAlign = ContentAlignment.TopRight };
            audioGroup.Controls.Add(smoothValue);
            smoothSpeedTrack = new TrackBar { Location = new Point(170, gy - 5), Size = new Size(620, 45), Minimum = 1, Maximum = 100, Value = (int)(visualizer.Logic.smoothSpeed * 100), TickStyle = TickStyle.None, BackColor = boxBg };
            smoothSpeedTrack.ValueChanged += (s, e) => { visualizer.Logic.smoothSpeed = smoothSpeedTrack.Value / 100f; smoothValue.Text = smoothSpeedTrack.Value.ToString(); visualizer.Invalidate(); };
            audioGroup.Controls.Add(smoothSpeedTrack);
            gy += 45;

            var latencyLabel = new Label { Text = LanguageManager.Get("LatencyComp"), Location = new Point(20, gy), Size = new Size(140, 20), ForeColor = dimText };
            audioGroup.Controls.Add(latencyLabel);
            var latencyValue = new Label { Text = visualizer.Logic.latencyCompensationMs.ToString(), Location = new Point(800, gy), Size = new Size(70, 20), ForeColor = neonCyan, TextAlign = ContentAlignment.TopRight };
            audioGroup.Controls.Add(latencyValue);
            latencyTrack = new TrackBar { Location = new Point(170, gy - 5), Size = new Size(620, 45), Minimum = 0, Maximum = 200, Value = visualizer.Logic.latencyCompensationMs, TickStyle = TickStyle.None, BackColor = boxBg };
            latencyTrack.ValueChanged += (s, e) => { visualizer.Logic.SetLatencyCompensation(latencyTrack.Value); latencyValue.Text = latencyTrack.Value.ToString(); };
            audioGroup.Controls.Add(latencyTrack);

            var bpmSmoothingCheck = new CheckBox 
            { 
                Text = LanguageManager.Get("BPMAutoSmoothing"), 
                Location = new Point(20, gy), 
                Size = new Size(200, 25), 
                ForeColor = neonCyan, 
                BackColor = boxBg, 
                Checked = visualizer.Logic.bpmSmoothing 
            };
            bpmSmoothingCheck.CheckedChanged += (s, e) => 
            { 
                visualizer.Logic.bpmSmoothing = bpmSmoothingCheck.Checked;
                if (!bpmSmoothingCheck.Checked)
                    visualizer.Logic.smoothSpeed = 0.15f;
            };
            audioGroup.Controls.Add(bpmSmoothingCheck);

            currentTabPanel.Controls.Add(audioGroup);
        }
    }
}
