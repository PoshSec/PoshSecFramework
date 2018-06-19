using System.Windows.Forms;

namespace PoshSec.Framework.Interface
{
    public partial class SystemForm : Form
    {
        public SystemForm()
        {
            InitializeComponent();
        }

        public string SystemName
        {
            get => txbSystemName.Text;
            set => txbSystemName.Text = value;
        }

        public string IpAddress
        {
            get => txbIpAddress.Text;
            set => txbIpAddress.Text = value;
        }

        public string Description
        {
            get => txbDescription.Text;
            set => txbDescription.Text = value;
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}