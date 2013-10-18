using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace poshsecframework.Controls
{
    class RichTextBoxCaret : RichTextBox
    {
        [DllImport("user32.dll")]
        static extern bool CreateCaret(IntPtr hWnd, IntPtr hBitmap, int nWidth, int nHeight);
        [DllImport("user32.dll")]
        static extern bool ShowCaret(IntPtr hWnd);
        int tbidx = 0;
        bool filter = true;
        List<String> cmds = null;
        List<String> acmds = null;
        int cmdstart = 0;
        int cmdstop = 0;
        const int WM_USER = 0x0400;
        const int WM_NOTIFY = 0x004E;
        const int WM_REFLECT = WM_USER + 0x1C00;
        const int WM_PAINT = 0xF;

        public RichTextBoxCaret()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.KeyDown += this_KeyDown;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            DrawCaret();
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if ((m.Msg == (WM_REFLECT + WM_NOTIFY)) || (m.Msg == WM_PAINT))
            {
                DrawCaret();
            }
        }

        private void this_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                if (cmds != null)
                {
                    AutoComplete();
                }
            }
            else if (!e.Control && !e.Shift && !e.Alt)
            {
                tbidx = 0;
                filter = true;
            }
        }

        private void AutoComplete()
        {           
            String cmdtxt = "";
            if (filter)
            {
                if (acmds != null && acmds.Count > 0)
                {
                    acmds.Clear();
                }                
                cmdstart = this.Text.LastIndexOf(' ', this.SelectionStart - 1) + 1;
                cmdstop = this.SelectionStart;
                cmdtxt = this.Text.Substring(cmdstart, cmdstop - cmdstart);                
                if (cmdtxt != "")
                {
                    acmds = cmds.Where(cmd => cmd.StartsWith(cmdtxt, StringComparison.OrdinalIgnoreCase)).ToList();
                    filter = false;
                }
            }
            if (acmds != null && acmds.Count > 0)
            {
                this.Text = this.Text.Substring(0, cmdstart);
                this.Text += acmds[tbidx];
                this.SelectionStart = this.Text.Length;
                tbidx++;
                if (tbidx >= acmds.Count)
                {
                    tbidx = 0;
                }
            }
        }

        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            { 
                case Keys.Tab:
                    return true;
                default:
                    return base.IsInputKey(keyData);
            }
        }        

        public void DrawCaret()
        {
            Bitmap bmp = Properties.Resources.caret_underline;
            Size sz = new Size(0, 0);
            //This size matches the Lucidia Console 9.75pt font.
            //Adjust as necessary.
            sz.Width = 8;
            sz.Height = 13;
            try
            {
                CreateCaret(this.Handle, bmp.GetHbitmap(Color.White), sz.Width, sz.Height);
                ShowCaret(this.Handle);
            }
            catch (Exception)
            { 
                //fail silently
            }
        }

        public List<String> AutoCompleteCommands
        {
            set { cmds = value; }
        }
    }
}
