using System;
using System.Drawing;
using System.Windows.Forms;

namespace NekoBeats
{
    public partial class ControlPanel
    {
        private void ShowCreditsTab(int y)
        {
            var creditsGroup = CreateGroupBox(LanguageManager.Get("About"), 10, y, 900, 340);
            int gy = 25;

            if (File.Exists("NekoBeatsLogo.png"))
            {
                var logoBox = new PictureBox { Image = Image.FromFile("NekoBeatsLogo.png"), SizeMode = PictureBoxSizeMode.Zoom, Location = new Point(390, gy), Size = new Size(120, 120), BackColor = Color.Transparent };
                creditsGroup.Controls.Add(logoBox);
                gy += 130;
            }

            var createdLabel = new Label { Text = LanguageManager.Get("CreatedBy"), Location = new Point(20, gy), Size = new Size(860, 25), ForeColor = neonCyan, Font = new Font("Courier New", 10, FontStyle.Bold), AutoSize = false };
            creditsGroup.Controls.Add(createdLabel);
            gy += 35;

            var versionLabel = new Label { Text = LanguageManager.Get("Version"), Location = new Point(20, gy), Size = new Size(860, 25), ForeColor = dimText, Font = new Font("Courier New", 10), AutoSize = false };
            creditsGroup.Controls.Add(versionLabel);
            gy += 35;

            var githubLabel = new Label { Text = LanguageManager.Get("GitHub"), Location = new Point(20, gy), Size = new Size(860, 25), ForeColor = neonCyan, Font = new Font("Courier New", 9, FontStyle.Underline), AutoSize = false, Cursor = Cursors.Hand };
            githubLabel.Click += (s, e) => { try { System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo { FileName = "https://github.com/justdev-chris/NekoBeats-V2", UseShellExecute = true }); } catch { } };
            creditsGroup.Controls.Add(githubLabel);
            gy += 45;

            var uninstallBtn = new Button
            {
                Text = LanguageManager.Get("Uninstall"),
                Location = new Point(20, gy),
                Size = new Size(180, 32),
                BackColor = Color.FromArgb(80, 20, 20),
                ForeColor = Color.FromArgb(255, 100, 100),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Courier New", 9),
                Cursor = Cursors.Hand
            };
            uninstallBtn.FlatAppearance.BorderColor = Color.FromArgb(255, 100, 100);
            uninstallBtn.Click += (s, e) =>
            {
                var confirm = MessageBox.Show(LanguageManager.Get("UninstallConfirm"), LanguageManager.Get("Uninstall"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm == DialogResult.Yes)
                {
                    string uninstallerPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "NekoBeats", "unins000.exe");
                    if (System.IO.File.Exists(uninstallerPath))
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo { FileName = uninstallerPath, UseShellExecute = true });
                        Application.Exit();
                    }
                    else
                    {
                        MessageBox.Show(LanguageManager.Get("UninstallNotFound"), LanguageManager.Get("NotFound"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            };
            creditsGroup.Controls.Add(uninstallBtn);

            currentTabPanel.Controls.Add(creditsGroup);
        }
    }
}
