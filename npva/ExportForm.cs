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
    /// <summary>
    /// エクスポートウィンドウ
    /// </summary>
    public partial class ExportForm : Form
    {
        public ExportForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 誰の奴？
        /// </summary>
        DB.Author auth;

        /// <summary>
        /// こいつの奴表示
        /// </summary>
        /// <param name="author"></param>
        public void Arrange(DB.Author author)
        {
            auth = author;
            lstTitles.Items.Clear();
            lstTitles.Items.AddRange(author.Titles.ToArray());
            for(var i=0;i<lstTitles.Items.Count;i++)
            {
                lstTitles.SetSelected(i, true);
            }

        }

        /// <summary>
        /// ロード時初期化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportForm_Load(object sender, EventArgs e)
        {
            cmbExportMode.Items.AddRange(Exporter.Exporter.GetExporters().ToArray());
            cmbExportMode.SelectedIndex = 0;
        }

        /// <summary>
        /// エクスポート
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            var exporter = cmbExportMode.SelectedItem as Exporter.Exporter;

            if (exporter == null) throw new InvalidOperationException();

            try
            {
                var titles = lstTitles.SelectedItems.Cast<DB.Title>();
                exporter.StartExport(auth, titles);
                if (chkAutoClose.Checked)
                {
                    Close();
                }
                else
                {
                    MessageBox.Show("エクスポートが終了しました。");
                }
            }
            catch (Exporter.ExportTitleNotSelectedException)
            {
                MessageBox.Show("エクスポートする作品が選ばれていません。");
            }
            catch (Exporter.ExporterNotAcceptMultiTitleException)
            {
                MessageBox.Show("この方法では複数の作品を同時にエクスポートできません。");
            }
        }
    }
}
