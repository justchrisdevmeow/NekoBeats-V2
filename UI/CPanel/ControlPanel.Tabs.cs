using System;
using System.Drawing;
using System.Windows.Forms;

namespace NekoBeats
{
    public partial class ControlPanel
    {
        private void InitializeComponents()
        {
            this.Text = LanguageManager.Get("WindowTitle");
            this.Size = new Size(950, 750);
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 50);
            this.BackColor = darkBg;
            this.ForeColor = textColor;
            this.MinimumSize = new Size(900, 700);
            this.Font = new Font("Courier New", 9);
            this.DoubleBuffered = true;
            this.FormClosing += OnFormClosing;

            var mainContainer = new Panel { Dock = DockStyle.Fill, BackColor = darkBg, Padding = new Padding(0) };

            tabButtonPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 45,
                BackColor = Color.FromArgb(15, 15, 20),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(8)
            };

            string[] tabs = { "VIZ", "COLORS", "FX", "AUDIO", "WINDOW", "PRESETS", "CREDITS" };
            string[] tabKeys = { "VIZTab", "COLORSTab", "FXTab", "AUDIOTab", "WINDOWTab", "PRESETSTab", "CREDITSTab" };

            for (int i = 0; i < tabs.Length; i++)
            {
                var tabBtn = new Button
                {
                    Text = LanguageManager.Get(tabKeys[i]),
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
                string tabName = tabs[i];
                tabBtn.Click += (s, e) => ShowTab(tabName);
                tabButtonPanel.Controls.Add(tabBtn);
                nextTabX += 82;
            }

            mainContainer.Controls.Add(tabButtonPanel);

            var contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = darkBg,
                Padding = new Padding(12)
            };

            currentTabPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = darkBg,
                AutoScroll = true
            };
            contentPanel.Controls.Add(currentTabPanel);
            mainContainer.Controls.Add(contentPanel);

            var footerPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 55,
                BackColor = Color.FromArgb(5, 5, 10),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(8)
            };

            var resetBtn = new Button
            {
                Text = LanguageManager.Get("Reset"),
                Location = new Point(10, 12),
                Size = new Size(85, 31),
                BackColor = neonCyan,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Courier New", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            resetBtn.Click += (s, e) =>
            {
                var result = MessageBox.Show(LanguageManager.Get("ResetConfirm"), LanguageManager.Get("ConfirmReset"), MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    visualizer.Logic.ResetToDefault();
                    ShowTab("VIZ");
                }
            };
            footerPanel.Controls.Add(resetBtn);

            var exitBtn = new Button
            {
                Text = LanguageManager.Get("Exit"),
                Location = new Point(850, 12),
                Size = new Size(85, 31),
                BackColor = neonCyan,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Courier New", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            exitBtn.Click += (s, e) => Environment.Exit(0);
            footerPanel.Controls.Add(exitBtn);

            mainContainer.Controls.Add(footerPanel);
            this.Controls.Add(mainContainer);

            ShowTab("VIZ");
        }

        private void ShowTab(string tabName)
        {
            currentTabPanel.Controls.Clear();
            int y = 10;

            switch (tabName)
            {
                case "VIZ": ShowVizTab(y); break;
                case "COLORS": ShowColorsTab(y); break;
                case "FX": ShowFxTab(y); break;
                case "AUDIO": ShowAudioTab(y); break;
                case "WINDOW": ShowWindowTab(y); break;
                case "PRESETS": ShowPresetsTab(y); break;
                case "CREDITS": ShowCreditsTab(y); break;
            }
        }
    }
}
