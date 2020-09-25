using System.Windows.Forms;

namespace npva
{
    public class DoubleBufferedListView : ListView
    {
        public DoubleBufferedListView()
        {
            DoubleBuffered = true;
        }
    }
}
