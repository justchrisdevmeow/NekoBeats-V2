using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

namespace NekoBeats
{
    public partial class ControlPanel : Form
    {
        private VisualizerForm visualizer;
        private PluginLoader pluginLoader;
        private string activePresetsFile = "active_presets.json";
        private Panel currentTabPanel;
        private Panel tabButtonPanel;
        private int nextTabX = 8;

        public ControlPanel(VisualizerForm visualizer, PluginLoader loader)
        {
            this.visualizer = visualizer;
            this.pluginLoader = loader;
            this.Icon = visualizer.Icon;
            InitializeComponents();
        }

        public void AddPluginTab(string tabName, Action<Panel> buildTab)
        {
            if (tabButtonPanel == null) return;

            var tabBtn = new Button
            {
                Text = tabName,
                Location = new Point(nextTabX, 8),
                Size = new Size(75, 29),
                BackColor = Color.FromArgb(30, 30, 40),
                ForeColor = dimText,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Courier New", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            tabBtn.FlatAppearance.BorderColor = dimText;
            tabBtn.FlatAppearance.BorderSize = 1;
            tabBtn.Click += (s, e) =>
            {
                currentTabPanel.Controls.Clear();
                var panel = new Panel
                {
                    Dock = DockStyle.Fill,
                    BackColor = darkBg,
                    AutoScroll = true
                };
                buildTab(panel);
                currentTabPanel.Controls.Add(panel);
            };

            tabButtonPanel.Controls.Add(tabBtn);
            nextTabX += 82;
        }

        private List<string> LoadActivePresets()
        {
            try
            {
                if (File.Exists(activePresetsFile))
                {
                    string json = File.ReadAllText(activePresetsFile);
                    return JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
                }
            }
            catch { }
            return new List<string>();
        }

        private void SaveActivePreset(string presetName)
        {
            try
            {
                var activePresets = LoadActivePresets();
                if (!activePresets.Contains(presetName)) activePresets.Add(presetName);
                File.WriteAllText(activePresetsFile, JsonSerializer.Serialize(activePresets, new JsonSerializerOptions { WriteIndented = true }));
            }
            catch { }
        }

        private void RemoveActivePreset(string presetName)
        {
            try
            {
                var activePresets = LoadActivePresets();
                activePresets.Remove(presetName);
                File.WriteAllText(activePresetsFile, JsonSerializer.Serialize(activePresets, new JsonSerializerOptions { WriteIndented = true }));
            }
            catch { }
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (File.Exists(activePresetsFile))
                    File.Delete(activePresetsFile);
            }
            catch { }
        }
    }
}
