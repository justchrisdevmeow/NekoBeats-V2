using System.Drawing;
using System.Windows.Forms;

namespace NekoBeats
{
    public partial class ControlPanel
    {
        private TrackBar bloomIntensityTrack;
        private TrackBar particleCountTrack;
        private TrackBar circleRadiusTrack;
        private TrackBar fadeSpeedTrack;
        private CheckBox bloomCheck;
        private CheckBox particlesCheck;
        private CheckBox circleModeCheck;
        private CheckBox fadeEffectCheck;
        private CheckBox waveformCheck;
        private CheckBox spectrumCheck;

        private void ShowFxTab(int y)
        {
            var fxGroup = CreateGroupBox(LanguageManager.Get("Effects"), 10, y, 900, 750);
            int gy = 25;

            bloomCheck = new CheckBox { Text = LanguageManager.Get("Bloom"), Location = new Point(20, gy), Size = new Size(200, 25), ForeColor = neonCyan, BackColor = boxBg, Checked = visualizer.Logic.bloomEnabled };
            bloomCheck.CheckedChanged += (s, e) => { visualizer.Logic.bloomEnabled = bloomCheck.Checked; visualizer.Invalidate(); };
            fxGroup.Controls.Add(bloomCheck);
            gy += 35;

            var bloomLabel = new Label { Text = LanguageManager.Get("BloomIntensity"), Location = new Point(20, gy), Size = new Size(140, 20), ForeColor = dimText };
            fxGroup.Controls.Add(bloomLabel);
            var bloomValue = new Label { Text = visualizer.Logic.bloomIntensity.ToString(), Location = new Point(800, gy), Size = new Size(70, 20), ForeColor = neonCyan, TextAlign = ContentAlignment.TopRight };
            fxGroup.Controls.Add(bloomValue);
            bloomIntensityTrack = new TrackBar { Location = new Point(170, gy - 5), Size = new Size(620, 45), Minimum = 0, Maximum = 50, Value = visualizer.Logic.bloomIntensity, TickStyle = TickStyle.None, BackColor = boxBg };
            bloomIntensityTrack.ValueChanged += (s, e) => { visualizer.Logic.bloomIntensity = bloomIntensityTrack.Value; bloomValue.Text = bloomIntensityTrack.Value.ToString(); visualizer.Invalidate(); };
            fxGroup.Controls.Add(bloomIntensityTrack);
            gy += 45;

            particlesCheck = new CheckBox { Text = LanguageManager.Get("Particles"), Location = new Point(20, gy), Size = new Size(200, 25), ForeColor = neonCyan, BackColor = boxBg, Checked = visualizer.Logic.particlesEnabled };
            particlesCheck.CheckedChanged += (s, e) => { visualizer.Logic.particlesEnabled = particlesCheck.Checked; visualizer.Invalidate(); };
            fxGroup.Controls.Add(particlesCheck);
            gy += 35;

            var particleLabel = new Label { Text = LanguageManager.Get("ParticleCount"), Location = new Point(20, gy), Size = new Size(140, 20), ForeColor = dimText };
            fxGroup.Controls.Add(particleLabel);
            var particleValue = new Label { Text = visualizer.Logic.particleCount.ToString(), Location = new Point(800, gy), Size = new Size(70, 20), ForeColor = neonCyan, TextAlign = ContentAlignment.TopRight };
            fxGroup.Controls.Add(particleValue);
            particleCountTrack = new TrackBar { Location = new Point(170, gy - 5), Size = new Size(620, 45), Minimum = 10, Maximum = 500, Value = visualizer.Logic.particleCount, TickStyle = TickStyle.None, BackColor = boxBg };
            particleCountTrack.ValueChanged += (s, e) => { visualizer.Logic.particleCount = particleCountTrack.Value; particleValue.Text = particleCountTrack.Value.ToString(); visualizer.Invalidate(); };
            fxGroup.Controls.Add(particleCountTrack);
            gy += 45;

            circleModeCheck = new CheckBox { Text = LanguageManager.Get("CircleMode"), Location = new Point(20, gy), Size = new Size(200, 25), ForeColor = neonCyan, BackColor = boxBg, Checked = visualizer.Logic.BarLogic.isCircleMode };
            circleModeCheck.CheckedChanged += (s, e) => { visualizer.Logic.BarLogic.isCircleMode = circleModeCheck.Checked; visualizer.Invalidate(); };
            fxGroup.Controls.Add(circleModeCheck);
            gy += 35;

            var radiusLabel = new Label { Text = LanguageManager.Get("CircleRadius"), Location = new Point(20, gy), Size = new Size(140, 20), ForeColor = dimText };
            fxGroup.Controls.Add(radiusLabel);
            var radiusValue = new Label { Text = visualizer.Logic.circleRadius.ToString(), Location = new Point(800, gy), Size = new Size(70, 20), ForeColor = neonCyan, TextAlign = ContentAlignment.TopRight };
            fxGroup.Controls.Add(radiusValue);
            circleRadiusTrack = new TrackBar { Location = new Point(170, gy - 5), Size = new Size(620, 45), Minimum = 50, Maximum = 500, Value = (int)visualizer.Logic.circleRadius, TickStyle = TickStyle.None, BackColor = boxBg };
            circleRadiusTrack.ValueChanged += (s, e) => { visualizer.Logic.circleRadius = circleRadiusTrack.Value; radiusValue.Text = circleRadiusTrack.Value.ToString(); visualizer.Invalidate(); };
            fxGroup.Controls.Add(circleRadiusTrack);
            gy += 45;

            fadeEffectCheck = new CheckBox { Text = LanguageManager.Get("FadeEffect"), Location = new Point(20, gy), Size = new Size(200, 25), ForeColor = neonCyan, BackColor = boxBg, Checked = visualizer.Logic.fadeEffectEnabled };
            fadeEffectCheck.CheckedChanged += (s, e) => { visualizer.Logic.fadeEffectEnabled = fadeEffectCheck.Checked; visualizer.Invalidate(); };
            fxGroup.Controls.Add(fadeEffectCheck);
            gy += 35;

            var fadeLabel = new Label { Text = LanguageManager.Get("FadeSpeed"), Location = new Point(20, gy), Size = new Size(140, 20), ForeColor = dimText };
            fxGroup.Controls.Add(fadeLabel);
            var fadeValue = new Label { Text = ((int)(visualizer.Logic.fadeEffectSpeed * 100)).ToString(), Location = new Point(800, gy), Size = new Size(70, 20), ForeColor = neonCyan, TextAlign = ContentAlignment.TopRight };
            fxGroup.Controls.Add(fadeValue);
            fadeSpeedTrack = new TrackBar { Location = new Point(170, gy - 5), Size = new Size(620, 45), Minimum = 1, Maximum = 100, Value = (int)(visualizer.Logic.fadeEffectSpeed * 100), TickStyle = TickStyle.None, BackColor = boxBg };
            fadeSpeedTrack.ValueChanged += (s, e) => { visualizer.Logic.fadeEffectSpeed = fadeSpeedTrack.Value / 100f; fadeValue.Text = fadeSpeedTrack.Value.ToString(); visualizer.Invalidate(); };
            fxGroup.Controls.Add(fadeSpeedTrack);
            gy += 45;

            waveformCheck = new CheckBox { Text = LanguageManager.Get("WaveformView"), Location = new Point(20, gy), Size = new Size(200, 25), ForeColor = neonCyan, BackColor = boxBg, Checked = visualizer.Logic.WaveformMode };
            waveformCheck.CheckedChanged += (s, e) => { visualizer.Logic.WaveformMode = waveformCheck.Checked; visualizer.Invalidate(); };
            fxGroup.Controls.Add(waveformCheck);
            gy += 35;

            spectrumCheck = new CheckBox { Text = LanguageManager.Get("SpectrumAnalyzer"), Location = new Point(20, gy), Size = new Size(220, 25), ForeColor = neonCyan, BackColor = boxBg, Checked = visualizer.Logic.SpectrumMode };
            spectrumCheck.CheckedChanged += (s, e) => { visualizer.Logic.SpectrumMode = spectrumCheck.Checked; visualizer.Invalidate(); };
            fxGroup.Controls.Add(spectrumCheck);
            gy += 35;

            var beatPulseCheck = new CheckBox { Text = "Beat Pulse", Location = new Point(20, gy), Size = new Size(200, 25), ForeColor = neonCyan, BackColor = boxBg, Checked = visualizer.Logic.beatPulseEnabled };
            beatPulseCheck.CheckedChanged += (s, e) => { visualizer.Logic.beatPulseEnabled = beatPulseCheck.Checked; };
            fxGroup.Controls.Add(beatPulseCheck);
            gy += 35;

            var beatPulseLabel = new Label { Text = "Pulse Intensity:", Location = new Point(20, gy), Size = new Size(140, 20), ForeColor = dimText };
            fxGroup.Controls.Add(beatPulseLabel);
            var beatPulseValue = new Label { Text = ((int)(visualizer.Logic.beatPulseIntensity * 100)).ToString(), Location = new Point(800, gy), Size = new Size(70, 20), ForeColor = neonCyan, TextAlign = ContentAlignment.TopRight };
            fxGroup.Controls.Add(beatPulseValue);
            var beatPulseTrack = new TrackBar { Location = new Point(170, gy - 5), Size = new Size(620, 45), Minimum = 5, Maximum = 100, Value = (int)(visualizer.Logic.beatPulseIntensity * 100), TickStyle = TickStyle.None, BackColor = boxBg };
            beatPulseTrack.ValueChanged += (s, e) => { visualizer.Logic.beatPulseIntensity = beatPulseTrack.Value / 100f; beatPulseValue.Text = beatPulseTrack.Value.ToString(); };
            fxGroup.Controls.Add(beatPulseTrack);

            currentTabPanel.Controls.Add(fxGroup);
        }
    }
}
