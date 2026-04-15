using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace NekoBeats
{
    public partial class ControlPanel
    {
        private void ShowPresetsTab(int y)
        {
            var presetsGroup = CreateGroupBox(LanguageManager.Get("PresetsPlugins"), 10, y, 900, 650);
            int gy = 25;

            var pluginLabel = new Label { Text = LanguageManager.Get("Plugins"), Location = new Point(20, gy), Size = new Size(860, 20), ForeColor = neonCyan, Font = new Font("Courier New", 10, FontStyle.Bold) };
            presetsGroup.Controls.Add(pluginLabel);
            gy += 30;

            if (pluginLoader != null)
            {
                var loadedPlugins = pluginLoader.GetLoadedPlugins();
                if (loadedPlugins.Count > 0)
                {
                    foreach (var plugin in loadedPlugins)
                    {
                        var checkbox = new CheckBox
                        {
                            Text = $"{plugin.Name} v{plugin.Version}",
                            Location = new Point(20, gy),
                            Size = new Size(400, 25),
                            ForeColor = neonCyan,
                            BackColor = boxBg,
                            Font = new Font("Courier New", 9),
                            Checked = true,
                            Tag = plugin
                        };
                        checkbox.CheckedChanged += (s, e) =>
                        {
                            if (checkbox.Checked) plugin.OnEnable();
                            else plugin.OnDisable();
                        };
                        presetsGroup.Controls.Add(checkbox);
                        gy += 30;
                    }
                }
                else
                {
                    var noPluginsLabel = new Label { Text = LanguageManager.Get("NoPlugins"), Location = new Point(20, gy), Size = new Size(860, 20), ForeColor = dimText, Font = new Font("Courier New", 9) };
                    presetsGroup.Controls.Add(noPluginsLabel);
                    gy += 30;
                }
            }
            else
            {
                var noPluginsLabel = new Label { Text = LanguageManager.Get("NoPlugins"), Location = new Point(20, gy), Size = new Size(860, 20), ForeColor = dimText, Font = new Font("Courier New", 9) };
                presetsGroup.Controls.Add(noPluginsLabel);
                gy += 30;
            }

            var nbpLabel = new Label { Text = LanguageManager.Get("NBPPresets"), Location = new Point(20, gy), Size = new Size(860, 20), ForeColor = neonCyan, Font = new Font("Courier New", 10, FontStyle.Bold) };
            presetsGroup.Controls.Add(nbpLabel);
            gy += 30;

            string presetsPath = "Presets";
            var activePresets = LoadActivePresets();

            if (Directory.Exists(presetsPath))
            {
                var nbpFiles = Directory.GetFiles(presetsPath, "*.nbp");
                if (nbpFiles.Length > 0)
                {
                    foreach (var file in nbpFiles)
                    {
                        string presetName = Path.GetFileNameWithoutExtension(file);
                        bool isActive = activePresets.Contains(presetName);
                        var checkbox = new CheckBox { Text = presetName, Location = new Point(20, gy), Size = new Size(400, 25), ForeColor = neonCyan, BackColor = boxBg, Font = new Font("Courier New", 9), Checked = isActive };
                        checkbox.CheckedChanged += (s, e) =>
                        {
                            if (checkbox.Checked) { visualizer.Logic.LoadPreset(file); SaveActivePreset(presetName); }
                            else RemoveActivePreset(presetName);
                            visualizer.Invalidate();
                        };
                        presetsGroup.Controls.Add(checkbox);
                        gy += 30;
                    }
                }
                else
                {
                    var noPresetsLabel = new Label { Text = LanguageManager.Get("NoNBPPresets"), Location = new Point(20, gy), Size = new Size(860, 20), ForeColor = dimText, Font = new Font("Courier New", 9) };
                    presetsGroup.Controls.Add(noPresetsLabel);
                    gy += 30;
                }
            }
            else
            {
                var noPresetsLabel = new Label { Text = LanguageManager.Get("NoNBPPresets"), Location = new Point(20, gy), Size = new Size(860, 20), ForeColor = dimText, Font = new Font("Courier New", 9) };
                presetsGroup.Controls.Add(noPresetsLabel);
                gy += 30;
            }

            var nbbarLabel = new Label { Text = LanguageManager.Get("NBBARPresets"), Location = new Point(20, gy), Size = new Size(860, 20), ForeColor = neonCyan, Font = new Font("Courier New", 10, FontStyle.Bold) };
            presetsGroup.Controls.Add(nbbarLabel);
            gy += 30;

            if (Directory.Exists(presetsPath))
            {
                var nbbarFiles = Directory.GetFiles(presetsPath, "*.nbbar");
                if (nbbarFiles.Length > 0)
                {
                    foreach (var file in nbbarFiles)
                    {
                        string presetName = Path.GetFileNameWithoutExtension(file);
                        bool isActive = activePresets.Contains(presetName);
                        var checkbox = new CheckBox { Text = presetName, Location = new Point(20, gy), Size = new Size(400, 25), ForeColor = neonCyan, BackColor = boxBg, Font = new Font("Courier New", 9), Checked = isActive };
                        checkbox.CheckedChanged += (s, e) =>
                        {
                            if (checkbox.Checked) { visualizer.Logic.LoadBarPreset(file); SaveActivePreset(presetName); }
                            else RemoveActivePreset(presetName);
                            visualizer.Invalidate();
                        };
                        presetsGroup.Controls.Add(checkbox);
                        gy += 30;
                    }
                }
                else
                {
                    var noPresetsLabel = new Label { Text = LanguageManager.Get("NoNBBARPresets"), Location = new Point(20, gy), Size = new Size(860, 20), ForeColor = dimText, Font = new Font("Courier New", 9) };
                    presetsGroup.Controls.Add(noPresetsLabel);
                    gy += 30;
                }
            }
            else
            {
                var noPresetsLabel = new Label { Text = LanguageManager.Get("NoNBBARPresets"), Location = new Point(20, gy), Size = new Size(860, 20), ForeColor = dimText, Font = new Font("Courier New", 9) };
                presetsGroup.Controls.Add(noPresetsLabel);
                gy += 30;
            }

            currentTabPanel.Controls.Add(presetsGroup);
        }
    }
}
