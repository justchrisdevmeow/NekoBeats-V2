using System.Drawing;
using System.Windows.Forms;

namespace NekoBeats
{
    public partial class ControlPanel
    {
        private TrackBar barCountTrack;
        private TrackBar barHeightTrack;
        private TrackBar opacityTrack;
        private TrackBar spacingTrack;

        private void ShowVizTab(int y)
        {
            var vizGroup = CreateGroupBox(LanguageManager.Get("Visualization"), 10, y, 900, 520);
            int gy = 25;

            var barCountLabel = new Label { Text = LanguageManager.Get("BarCount"), Location = new Point(20, gy), Size = new Size(140, 20), ForeColor = dimText };
            vizGroup.Controls.Add(barCountLabel);
            var barCountValue = new Label { Text = visualizer.Logic.barCount.ToString(), Location = new Point(800, gy), Size = new Size(70, 20), ForeColor = neonCyan, TextAlign = ContentAlignment.TopRight };
            vizGroup.Controls.Add(barCountValue);
            barCountTrack = new TrackBar { Location = new Point(170, gy - 5), Size = new Size(620, 45), Minimum = 32, Maximum = 512, Value = visualizer.Logic.barCount, TickStyle = TickStyle.None, BackColor = boxBg };
            barCountTrack.ValueChanged += (s, e) => { visualizer.Logic.barCount = barCountTrack.Value; barCountValue.Text = barCountTrack.Value.ToString(); visualizer.Invalidate(); };
            vizGroup.Controls.Add(barCountTrack);
            gy += 45;

            var barHeightLabel = new Label { Text = LanguageManager.Get("BarHeight"), Location = new Point(20, gy), Size = new Size(140, 20), ForeColor = dimText };
            vizGroup.Controls.Add(barHeightLabel);
            var barHeightValue = new Label { Text = visualizer.Logic.barHeight.ToString(), Location = new Point(800, gy), Size = new Size(70, 20), ForeColor = neonCyan, TextAlign = ContentAlignment.TopRight };
            vizGroup.Controls.Add(barHeightValue);
            barHeightTrack = new TrackBar { Location = new Point(170, gy - 5), Size = new Size(620, 45), Minimum = 20, Maximum = 400, Value = visualizer.Logic.barHeight, TickStyle = TickStyle.None, BackColor = boxBg };
            barHeightTrack.ValueChanged += (s, e) => { visualizer.Logic.barHeight = barHeightTrack.Value; barHeightValue.Text = barHeightTrack.Value.ToString(); visualizer.Invalidate(); };
            vizGroup.Controls.Add(barHeightTrack);
            gy += 45;

            var opacityLabel = new Label { Text = LanguageManager.Get("Opacity"), Location = new Point(20, gy), Size = new Size(140, 20), ForeColor = dimText };
            vizGroup.Controls.Add(opacityLabel);
            var opacityValue = new Label { Text = ((int)(visualizer.Logic.opacity * 100)).ToString(), Location = new Point(800, gy), Size = new Size(70, 20), ForeColor = neonCyan, TextAlign = ContentAlignment.TopRight };
            vizGroup.Controls.Add(opacityValue);
            opacityTrack = new TrackBar { Location = new Point(170, gy - 5), Size = new Size(620, 45), Minimum = 0, Maximum = 100, Value = (int)(visualizer.Logic.opacity * 100), TickStyle = TickStyle.None, BackColor = boxBg };
            opacityTrack.ValueChanged += (s, e) => { visualizer.Logic.opacity = opacityTrack.Value / 100f; opacityValue.Text = opacityTrack.Value.ToString(); visualizer.Invalidate(); };
            vizGroup.Controls.Add(opacityTrack);
            gy += 45;

            var spacingLabel = new Label { Text = LanguageManager.Get("BarSpacing"), Location = new Point(20, gy), Size = new Size(140, 20), ForeColor = dimText };
            vizGroup.Controls.Add(spacingLabel);
            var spacingValue = new Label { Text = visualizer.Logic.barSpacing.ToString(), Location = new Point(800, gy), Size = new Size(70, 20), ForeColor = neonCyan, TextAlign = ContentAlignment.TopRight };
            vizGroup.Controls.Add(spacingValue);
            spacingTrack = new TrackBar { Location = new Point(170, gy - 5), Size = new Size(620, 45), Minimum = 0, Maximum = 10, Value = visualizer.Logic.barSpacing, TickStyle = TickStyle.None, BackColor = boxBg };
            spacingTrack.ValueChanged += (s, e) => { visualizer.Logic.barSpacing = spacingTrack.Value; spacingValue.Text = spacingTrack.Value.ToString(); visualizer.Invalidate(); };
            vizGroup.Controls.Add(spacingTrack);

            currentTabPanel.Controls.Add(vizGroup);
        }
    }
}
