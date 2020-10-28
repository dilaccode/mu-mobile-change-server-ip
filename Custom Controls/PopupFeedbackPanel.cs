using System;
using System.Drawing;
using System.Windows.Forms;

namespace CongLibrary.CustomControls
{
    class PopupFeedback
    {
        private Panel Popup;
        private Label Text;
        private Size SoftSize;

        public PopupFeedback(Panel Popup, Label Text, Size SoftSize) {
            this.Popup = Popup;
            this.Popup.Hide();
            this.Text = Text;
            this.SoftSize = SoftSize;
        }

        public void Show(string Text, Point Position) {
            this.Text.Text = Text;
            // check posotion
            int EndPointX = Position.X + Popup.Width;
            int EndPointY = Position.Y + Popup.Height;
            int ScrollBarWidth = 40;
            if (EndPointX > SoftSize.Width)
            {
                // new position X
                Position.X = SoftSize.Width - Popup.Width - ScrollBarWidth;
            }
            Console.WriteLine(SoftSize.Height);
            if (EndPointY > SoftSize.Height)
            {
                // new position Y
                Position.Y = SoftSize.Height - Popup.Height;
            }
            this.Popup.Location = Position;
            this.Popup.Show();

            // hide after X seconds
            var t = new Timer();
            t.Interval = 1000;
            t.Tick += (s, e) =>
            {
                this.Popup.Hide();
                t.Stop();
            };
            t.Start();
        }
    }
}
