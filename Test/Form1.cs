using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadPositons();
        }

        private Point _Max = new Point(500, 500);
        private List<double> _X = new List<double>();
        private List<double> _Y = new List<double>();
        private List<double> _Z = new List<double>();
        private List<double> _Zo = new List<double>();
        private bool _Loaded = false;

        private void LoadPositons()
        {
            string line;
            int idx = 0;
            System.IO.StreamReader file = new System.IO.StreamReader("PositionMemory.txt");
            while ((line = file.ReadLine()) != null)
            {
                string[] data = line.Split(',');
                _X.Add(double.Parse(data[0]));
                _Y.Add(double.Parse(data[1]));
                _Z.Add(double.Parse(data[2]));
                _Zo.Add(double.Parse(data[5]));
                idx++;
            }
            file.Close();

            numX.Minimum = (decimal)_X.Min();
            numX.Maximum = (decimal)_X.Max();
            numY.Minimum = (decimal)_Y.Min();
            numY.Maximum = (decimal)_Y.Max();

            _Loaded = true;
            updateAll();
        }

        private void num_Changed(object sender, EventArgs e)
        {
            if (!_Loaded)
                return;
            updateAll();
        }

        private void updateAll()
        {
            lblZ.Text = getZ().ToString();
            lblZOffset.Text = getZOffset().ToString();
            lblZo.Text = getZo().ToString();
            lblZoOffset.Text = getZoOffset().ToString();
        }

        private double getZ()
        {
            string val = interpx(string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8}",
                                                _Max.X, _Max.Y, numX.Value, numY.Value,
                                                _Z[0], _Z[1], _Z[2], _Z[3], 0));
            return double.Parse(val);
        }

        private double getZOffset()
        {
            string val = interpx(string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8}",
                                               _Max.X, _Max.Y, numX.Value, numY.Value,
                                               _Z[0], _Z[1], _Z[2], _Z[3], 1));
            return double.Parse(val);
        }

        private double getZo()
        {
            string val = interpx(string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8}",
                                               _Max.X, _Max.Y, numX.Value, numY.Value,
                                               _Zo[0], _Zo[1], _Zo[2], _Zo[3], 0));
            return double.Parse(val);
        }

        private double getZoOffset()
        {
            string val = interpx(string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8}",
                                               _Max.X, _Max.Y, numX.Value, numY.Value,
                                               _Zo[0], _Zo[1], _Zo[2], _Zo[3], 1));
            return double.Parse(val);
        }

        private string interpx(string args)
        {
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "Properties/interpx/interpx.exe",
                    Arguments = args,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            proc.Start();
            string line = proc.StandardOutput.ReadLine();
            return line;
        }
    }
}
       
