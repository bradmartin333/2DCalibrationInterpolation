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
            CalculateDeltas();
            Interpolate();
        }

        private Point _Min = new Point(0, 0);
        private Point _Max = new Point(500, 500);
        private List<double> _X = new List<double>();
        private List<double> _Y = new List<double>();
        private List<double> _Z = new List<double>();
        private List<double> _Zo = new List<double>();

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
        }

        private void CalculateDeltas()
        {
            double minZ = _Z.Min();
            double minZo = _Zo.Min();
            for (int i = 0; i<_X.Count; i++)
            {
                _Z[i] -= minZ;
                _Zo[i] -= minZo;
            }
        }

        private void Interpolate()
        {
            MathNet.Numerics.Interpolation.IInterpolation Xinterpolation = MathNet.Numerics.Interpolate.Linear(_X, _Zo);
            MathNet.Numerics.Interpolation.IInterpolation Yinterpolation = MathNet.Numerics.Interpolate.Linear(_Y, _Zo);
            using (StreamWriter sw = File.CreateText("CSoutput.txt"))
            {
                for (int i = _Min.X; i <= _Max.X; i += 5)
                {
                    for (int j = _Min.Y; j < _Max.Y; j += 5)
                    {
                        double Xvalue = Xinterpolation.Interpolate(i);
                        if (Xvalue == double.NegativeInfinity)
                            Xvalue = 0;
                        double Yvalue = Yinterpolation.Interpolate(j);
                        if (Yvalue == double.NegativeInfinity)
                            Yvalue = 0;
                        sw.WriteLine(string.Format("{0}{1}{2}{3}{4}", i, '\t', j, '\t', (Xvalue + Yvalue) / 2));
                    }
                }
            }
        }
    }
}
       
