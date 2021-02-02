using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace npva
{
    public partial class ScoreViewDialog : Form
    {
        public ScoreViewDialog()
        {
            InitializeComponent();
        }

        public string EventString { get { return txtEvent.Text; } }

        public void Set(DB.Author author, DB.Title title, DB.DailyScore daily)
        {
            var s = new StringBuilder();
            if (author != null)
            {
                s.AppendLine($"{author.Name}[{author.ID}]");
            }
            if (title != null)
            {
                s.AppendLine($"{title.Name}");
                s.AppendLine($"{title.ID} since {title.FirstUp:yyyy/MM/dd}");
            }
            if (daily!=null)
            {
                Text = daily.Date.ToString("yyyy/MM/dd");
                s.AppendLine($"{daily.PageView} pv.");
                if (daily.HasScoreInfo)
                {
                }
                if (daily.PartPvChecked)
                {
                    s.AppendLine("[部位別PV取得済]");
                }
                txtEvent.Text = daily.Event;
            }
            lblInfo.Text = s.ToString();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
