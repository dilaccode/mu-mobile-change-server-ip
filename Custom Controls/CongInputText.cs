using System.Windows.Forms;

namespace CongLibrary.CustomControls
{
    class CongInputText : TextBox
    {
        public void MoveCursorToEnd()
        {
            this.Focus();
            this.SelectionStart = this.Text.Length;
        }
    }
}
