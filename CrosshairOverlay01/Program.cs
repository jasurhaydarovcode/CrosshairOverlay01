using System;
using System.Drawing;
using System.Windows.Forms;

namespace CrosshairApp
{
    public class MainForm : Form
    {
        private Button controlButton;
        private OverlayForm overlay;

        public MainForm()
        {
            // Oyna sozlamalari
            this.Text = "Crosshair Control";
            this.Size = new Size(300, 150);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Control Button (Turn On/Off)
            controlButton = new Button();
            controlButton.Text = "Turn On";
            controlButton.Size = new Size(100, 40);
            controlButton.Location = new Point(100, 40);
            controlButton.Click += ControlButton_Click;

            this.Controls.Add(controlButton);
        }

        private void ControlButton_Click(object sender, EventArgs e)
        {
            if (overlay == null || overlay.IsDisposed) // Agar overlay ochilmagan bo'lsa
            {
                overlay = new OverlayForm(); // Yangi overlay yaratish
                overlay.Show(); // Overlayni ko'rsatish
                controlButton.Text = "Turn Off"; // Tugma yozuvini o'zgartirish
            }
            else
            {
                overlay.Close(); // Overlayni yopish
                overlay.Dispose(); // Resurslarni bo'shatish
                overlay = null;
                controlButton.Text = "Turn On"; // Tugma yozuvini o'zgartirish
            }
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }

    public class OverlayForm : Form
    {
        public OverlayForm()
        {
            this.FormBorderStyle = FormBorderStyle.None; // Ramkasiz
            this.TopMost = true; // Boshqa oynalardan yuqori
            this.BackColor = Color.Magenta; // Shaffoflik uchun rang
            this.TransparencyKey = Color.Magenta; // Magenta shaffof bo'ladi
            this.StartPosition = FormStartPosition.CenterScreen; // Markazda joylashadi
            this.Size = new Size(5, 5); // Crosshair hajmi
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            Pen pen = new Pen(Color.Red, 1); // Chiziq qalinligi 1px
            g.DrawLine(pen, 2, 0, 2, 5); // Vertikal chiziq
            g.DrawLine(pen, 0, 2, 5, 2); // Gorizontal chiziq
        }
    }
}
