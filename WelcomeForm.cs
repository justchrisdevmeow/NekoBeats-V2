using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace NekoBeats
{
    public class WelcomeForm : Form
    {
        private Color bg = Color.FromArgb(10, 10, 15);
        private Color accent = Color.FromArgb(168, 85, 247);
        private Color dimText = Color.FromArgb(150, 150, 180);
        private int currentPage = 0;
        private Panel[] pages;
        private Button nextBtn;
        private Button backBtn;
        private Label pageIndicator;
        private CheckBox dontShowCheck;

        public static string FlagPath => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "NekoBeats", "welcomed.flag"
        );

        public WelcomeForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Welcome to NekoBeats";
            this.Size = new Size(620, 560);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = bg;
            this.ForeColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Font = new Font("Courier New", 9);

            try
            {
                if (File.Exists("NekoBeatsLogo.ico"))
                    this.Icon = new Icon("NekoBeatsLogo.ico");
            }
            catch { }

            pages = new Panel[]
            {
                CreateWelcomePage(),
                CreateTutPage1(),
                CreateTutPage2(),
                CreateTutPage3(),
                CreateFinishPage()
            };

            foreach (var page in pages)
            {
                page.Visible = false;
                this.Controls.Add(page);
            }
            pages[0].Visible = true;

            // page indicator
            pageIndicator = new Label
            {
                Text = "1 / 5",
                Location = new Point(0, 460),
                Size = new Size(620, 20),
                ForeColor = dimText,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Courier New", 9)
            };
            this.Controls.Add(pageIndicator);

            // don't show again
            dontShowCheck = new CheckBox
            {
                Text = "Don't show this again",
                Location = new Point(20, 490),
                Size = new Size(200, 25),
                ForeColor = dimText,
                BackColor = bg,
                Font = new Font("Courier New", 9)
            };
            this.Controls.Add(dontShowCheck);

            // back button — LEFT side
            backBtn = new Button
            {
                Text = "◀ BACK",
                Location = new Point(250, 487),
                Size = new Size(100, 32),
                BackColor = Color.FromArgb(30, 30, 40),
                ForeColor = dimText,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Courier New", 9, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Visible = false
            };
            backBtn.FlatAppearance.BorderColor = dimText;
            backBtn.Click += (s, e) => NavigatePage(-1);
            this.Controls.Add(backBtn);

            // next button — RIGHT side
            nextBtn = new Button
            {
                Text = "NEXT ▶",
                Location = new Point(490, 487),
                Size = new Size(100, 32),
                BackColor = accent,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Courier New", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            nextBtn.FlatAppearance.BorderColor = accent;
            nextBtn.Click += (s, e) => NavigatePage(1);
            this.Controls.Add(nextBtn);
        }

        private void NavigatePage(int direction)
        {
            if (currentPage == pages.Length - 1 && direction == 1)
            {
                if (dontShowCheck.Checked)
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(FlagPath));
                    File.WriteAllText(FlagPath, "1");
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }

            pages[currentPage].Visible = false;
            currentPage = Math.Clamp(currentPage + direction, 0, pages.Length - 1);
            pages[currentPage].Visible = true;

            backBtn.Visible = currentPage > 0;
            nextBtn.Text = currentPage == pages.Length - 1 ? "LET'S GO! 🐱" : "NEXT ▶";
            pageIndicator.Text = $"{currentPage + 1} / 5";
        }

        private Panel CreateWelcomePage()
        {
            var panel = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(610, 450),
                BackColor = bg
            };

            try
            {
                if (File.Exists("NekoBeatsLogo.png"))
                {
                    var logo = new PictureBox
                    {
                        Image = Image.FromFile("NekoBeatsLogo.png"),
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Location = new Point(230, 20),
                        Size = new Size(130, 130),
                        BackColor = Color.Transparent
                    };
                    panel.Controls.Add(logo);
                }
            }
            catch { }

            AddLabel(panel, "Welcome to NekoBeats! 🐱", new Point(0, 165), new Size(600, 40),
                new Font("Courier New", 18, FontStyle.Bold), accent, ContentAlignment.MiddleCenter);

            AddLabel(panel, "v2.3.3", new Point(0, 210), new Size(600, 25),
                new Font("Courier New", 10), dimText, ContentAlignment.MiddleCenter);

            AddLabel(panel, "A sleek audio visualizer that turns your music\ninto floating light bars.",
                new Point(60, 245), new Size(480, 50),
                new Font("Courier New", 10), Color.White, ContentAlignment.MiddleCenter);

            AddLabel(panel, "Let's get you set up! ✨",
                new Point(0, 310), new Size(600, 25),
                new Font("Courier New", 10), dimText, ContentAlignment.MiddleCenter);

            return panel;
        }

        private Panel CreateTutPage1()
        {
            var panel = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(610, 450),
                BackColor = bg
            };

            AddLabel(panel, "🎵 Getting Started", new Point(30, 20), new Size(540, 35),
                new Font("Courier New", 14, FontStyle.Bold), accent, ContentAlignment.MiddleLeft);

            AddLabel(panel, "1. Play some music through your speakers or headphones.\n\n" +
                "2. NekoBeats automatically captures your system audio — no setup needed!\n\n" +
                "3. The floating bars will appear on your screen, reacting to the music in real time.\n\n" +
                "4. The control panel lets you customize everything — colors, bar count, height, effects and more.",
                new Point(30, 70), new Size(540, 280),
                new Font("Courier New", 10), Color.White, ContentAlignment.TopLeft);

            return panel;
        }

        private Panel CreateTutPage2()
        {
            var panel = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(610, 450),
                BackColor = bg
            };

            AddLabel(panel, "🎨 Customization", new Point(30, 20), new Size(540, 35),
                new Font("Courier New", 14, FontStyle.Bold), accent, ContentAlignment.MiddleLeft);

            string[] tips = new string[]
            {
                "VIZ tab — adjust bar count, height, opacity and spacing",
                "COLORS tab — pick bar colors, enable rainbow or gradient",
                "FX tab — bloom, particles, circle mode, fade effect",
                "AUDIO tab — sensitivity, smooth speed, latency comp",
                "WINDOW tab — click-through, streaming mode, FPS limit",
                "PRESETS tab — load .nbp and .nbbar preset files"
            };

            int y = 75;
            foreach (var tip in tips)
            {
                AddLabel(panel, $"• {tip}", new Point(30, y), new Size(540, 25),
                    new Font("Courier New", 9), Color.White, ContentAlignment.MiddleLeft);
                y += 38;
            }

            return panel;
        }

        private Panel CreateTutPage3()
        {
            var panel = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(610, 450),
                BackColor = bg
            };

            AddLabel(panel, "💡 Tips & Tricks", new Point(30, 20), new Size(540, 35),
                new Font("Courier New", 14, FontStyle.Bold), accent, ContentAlignment.MiddleLeft);

            AddLabel(panel, "• Enable Click-Through so bars don't block your mouse\n\n" +
                "• Use Streaming Mode to capture NekoBeats in OBS\n\n" +
                "• The system tray icon lets you show/hide the control panel\n\n" +
                "• Check for updates anytime via the tray icon menu\n\n" +
                "• Share your presets at catsdevs.online/NekoBeats-V2/community-themes.html",
                new Point(30, 70), new Size(540, 280),
                new Font("Courier New", 10), Color.White, ContentAlignment.TopLeft);

            return panel;
        }

        private Panel CreateFinishPage()
        {
            var panel = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(610, 450),
                BackColor = bg
            };

            AddLabel(panel, "You're all set! 🐱✨", new Point(0, 120), new Size(600, 45),
                new Font("Courier New", 16, FontStyle.Bold), accent, ContentAlignment.MiddleCenter);

            AddLabel(panel, "Play some music and watch the magic happen!\n\nMade with ❤️ by justdev-chris",
                new Point(60, 200), new Size(480, 80),
                new Font("Courier New", 10), Color.White, ContentAlignment.MiddleCenter);

            return panel;
        }

        private void AddLabel(Panel panel, string text, Point location, Size size,
            Font font, Color color, ContentAlignment align)
        {
            var label = new Label
            {
                Text = text,
                Location = location,
                Size = size,
                ForeColor = color,
                Font = font,
                TextAlign = align,
                BackColor = Color.Transparent
            };
            panel.Controls.Add(label);
        }
    }
}
