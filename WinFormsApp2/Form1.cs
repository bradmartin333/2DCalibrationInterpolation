using System.Windows.Forms;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1(double[,] data)
        {
            InitializeComponent();
            formsPlot1.plt.PlotHeatmap(data);
        }
    }
}
