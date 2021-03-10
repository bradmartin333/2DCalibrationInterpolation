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

        private List<double> _X = new List<double>();
        private List<double> _Y = new List<double>();
        private List<double> _Z = new List<double>();
        private List<double> _Zo = new List<double>();
        private alglib.spline2dinterpolant ZSpline = new alglib.spline2dinterpolant();
        private alglib.spline2dinterpolant ZoSpline = new alglib.spline2dinterpolant();
        private bool _Loaded = false;
        

        private void LoadPositons()
        {
            string line;
            int idx = 0;
            StreamReader file = new StreamReader("PositionMemory.txt");
            while ((line = file.ReadLine()) != null)
            {
                string[] data = line.Split(',');
                if (!_X.Contains(double.Parse(data[0])))
                    _X.Add(double.Parse(data[0]));
                if (!_Y.Contains(double.Parse(data[1])))
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

            alglib.spline2dbuildbicubicv(_X.ToArray(), 2, _Y.ToArray(), 2, _Z.ToArray(), 1, out ZSpline);
            alglib.spline2dbuildbicubicv(_X.ToArray(), 2, _Y.ToArray(), 2, _Zo.ToArray(), 1, out ZoSpline);
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
            double vZ = alglib.spline2dcalc(ZSpline, decimal.ToDouble(numX.Value), decimal.ToDouble(numY.Value));
            double vZo = alglib.spline2dcalc(ZoSpline, decimal.ToDouble(numX.Value), decimal.ToDouble(numY.Value));
            vZ = Math.Round(vZ, 3);
            double vZOff = Math.Round((vZ - _Z.Min()) * -1, 3);
            vZo = Math.Round(vZo, 3);
            double vZoOff = Math.Round((vZo - _Zo.Min()) * -1, 3);
            lblZ.Text = vZ.ToString();
            lblZOffset.Text = vZOff.ToString();
            lblZo.Text = vZo.ToString();
            lblZoOffset.Text = vZoOff.ToString();
        }
    }
}
       
