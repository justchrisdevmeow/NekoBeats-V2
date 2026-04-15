using System.Windows.Forms;

namespace NekoBeats
{
    public partial class ControlPanel
    {
        private void ShowWindowTab(int y)
        {
            var windowGroup = CreateGroupBox(LanguageManager.Get("WindowDisplay"), 10, y, 900, 400);
            int gy = 25;

            var fpsLabel = new Label { Text = LanguageManager.Get("FPSLimit"), Location = new Point(20, gy + 5), Size = new Size(140, 20), ForeColor = dimText };
            windowGroup.Controls.Add(fpsLabel);
            var fpsCombo = new ComboBox { Location = new Point(170, gy), Size = new Size(220, 25), DropDownStyle = ComboBoxStyle.DropDownList, BackColor = Color.FromArgb(30, 30, 40), ForeColor = neonCyan };
            fpsCombo.Items.AddRange(new string[] { "30", "60", "120", LanguageManager.Get("Uncapped") });
            fpsCombo.SelectedIndex = visualizer.Logic.fpsLimit switch { 30 => 0, 60 => 1, 120 => 2, _ => 3 };
            fpsCombo.SelectedIndexChanged += (s, e) => { visualizer.Logic.fpsLimit = fpsCombo.Text switch { "30" => 30, "60" => 60, "120" => 120, _ => 999 }; visualizer.UpdateFPSTimer(); };
            windowGroup.Controls.Add(fpsCombo);
            gy += 45;

            var clickThroughCheck = new CheckBox { Text = LanguageManager.Get("ClickThrough"), Location = new Point(20, gy), Size = new Size(200, 25), ForeColor = neonCyan, BackColor = boxBg,
