using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace poshsecframework.Interface
{
    public class ProxyPreferenceGroupBox : GroupBox
    {
        public event EventHandler SelectedChanged = delegate { };

        ProxyPreference _selected;

        public ProxyPreference Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                ProxyPreference val = ProxyPreference.System;
                var radioButton = Controls.OfType<RadioButton>()
                    .FirstOrDefault(radio =>
                        radio.Tag != null
                       && Enum.TryParse(radio.Tag.ToString(), out val) && val == value);

                if (radioButton != null)
                {
                    radioButton.Checked = true;
                    _selected = val;
                }
            }
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            if (e.Control is RadioButton radioButton)
                radioButton.CheckedChanged += RadioButton_CheckedChanged;
        }

        void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            var radio = (RadioButton)sender;
            if (radio.Checked && radio.Tag != null
                 && Enum.TryParse(radio.Tag.ToString(), out ProxyPreference val))
            {
                _selected = val;
                SelectedChanged(this, new EventArgs());
            }
        }
    }
}
