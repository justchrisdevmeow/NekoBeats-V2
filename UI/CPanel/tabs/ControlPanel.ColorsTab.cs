using System;
using System.Drawing;
using System.Windows.Forms;

namespace NekoBeats
{
    public partial class ControlPanel
    {
        private CheckBox rainbowCheck;
        private CheckBox gradientToggle;
        private ComboBox themeCombo;
        private CheckBox colorCycleCheck;
        private TrackBar colorSpeedTrack;

        private void ShowColorsTab(int y)
        {
            var colorGroup = CreateGroupBox(LanguageManager.Get("ColorsEffects"), 10, y, 900, 450);
            int gy = 25;

            var colorBtn = new Button { Text = LanguageManager.Get("BarColor"), Location = new Point(20, gy), Size = new Size(100, 32), BackColor = neonCyan, ForeColor = Color.Black, FlatStyle = FlatStyle.Flat, Font = new Font("Courier New", 9, FontStyle.Bold), Cursor = Cursors.Hand };
            colorBtn.Click += (s, e) => ShowColorDialog();
            colorGroup.Controls.Add(colorBtn);
            gy += 45;

            rainbowCheck = new CheckBox { Text = LanguageManager.Get("RainbowBars"), Location = new Point(20, gy), Size = new Size(200, 25), ForeColor = neonCyan, BackColor = boxBg, Checked = visualizer.Logic.rainbowBars };
            rainbowCheck.CheckedChanged += (s, e) => { visualizer.Logic.rainbowBars = rainbowCheck.Checked; visualizer.Invalidate(); };
            colorGroup.Controls.Add(rainbowCheck);
            gy += 35;

            var themeLabel = new Label { Text = LanguageManager.Get("BarTheme"), Location = new Point(20, gy + 5), Size = new Size(140, 20), ForeColor = dimText };
            colorGroup.Controls.Add(themeLabel);
            themeCombo = new ComboBox { Location = new Point(170, gy), Size = new Size(220, 25), DropDownStyle = ComboBoxStyle.DropDownList, BackColor = Color.FromArgb(30, 30, 40), ForeColor = neonCyan, FlatStyle = FlatStyle.Flat };
            themeCombo.Items.AddRange(System.Enum.GetNames(typeof(BarRenderer.BarTheme)));
            themeCombo.SelectedIndex = (int)visualizer.Logic.BarLogic.currentTheme;
            themeCombo.SelectedIndexChanged += (s, e) => { visualizer.Logic.BarLogic.currentTheme = (BarRenderer.BarTheme)themeCombo.SelectedIndex; visualizer.Invalidate(); };
            colorGroup.Controls.Add(themeCombo);
            gy += 40;

            gradientToggle = new CheckBox { Text = LanguageManager.Get("RainbowGradient"), Location = new Point(20, gy), Size = new Size(200, 25), ForeColor = neonCyan, BackColor = boxBg, Checked = visualizer.Logic.useGradient };
            gradientToggle.CheckedChanged += (s, e) =>
            {
                if (gradientToggle.Checked)
                {
                    Color[] gradient = new Color[] { Color.Red, Color.Yellow, Color.Green, Color.Cyan, Color.Blue, Color.Magenta };
                    visualizer.Logic.ApplyGradient(gradient);
                }
                else
                {
                    visualizer.Logic.ClearGradient();
                }
                visualizer.Invalidate();
            };
            colorGroup.Controls.Add(gradientToggle);
            gy += 35;

            colorCycleCheck = new CheckBox { Text = LanguageManager.Get("ColorCycle"), Location = new Point(20, gy), Size = new Size(200, 25), ForeColor = neonCyan, BackColor = boxBg, Checked = visualizer.Logic.colorCycling };
            colorCycleCheck.CheckedChanged += (s, e) => { visualizer.Logic.colorCycling = colorCycleCheck.Checked; visualizer.Invalidate(); };
            colorGroup.Controls.Add(colorCycleCheck);
            gy += 35;

            var colorSpeedLabel = new Label { Text = LanguageManager.Get("ColorSpeed"), Location = new Point(20, gy), Size = new Size(140, 20), ForeColor = dimText };
            colorGroup.Controls.Add(colorSpeedLabel);
            var colorSpeedValue = new Label { Text = ((int)(visualizer.Logic.colorSpeed * 10)).ToString(), Location = new Point(800, gy), Size = new Size(70, 20), ForeColor = neonCyan, TextAlign = ContentAlignment.TopRight };
            colorGroup.Controls.Add(colorSpeedValue);
            colorSpeedTrack = new TrackBar { Location = new Point(170, gy - 5), Size = new Size(620, 45), Minimum = 1, Maximum = 100, Value = (int)(visualizer.Logic.colorSpeed * 10), TickStyle = TickStyle.None, BackColor = boxBg };
            colorSpeedTrack.ValueChanged += (s, e) => { visualizer.Logic.colorSpeed = colorSpeedTrack.Value / 10f; colorSpeedValue.Text = colorSpeedTrack.Value.ToString(); visualizer.Invalidate(); };
            colorGroup.Controls.Add(colorSpeedTrack);

            currentTabPanel.Controls.Add(colorGroup);
        }
    }
}
