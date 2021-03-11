using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
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

        private List<double> _X = new List<double>(), _Y = new List<double>(), _Z = new List<double>(), _Zo = new List<double>();
        private alglib.spline2dinterpolant ZSpline, ZoSpline = new alglib.spline2dinterpolant();
        private bool _Loaded = false;

        private void LoadPositons()
        {
            string line;
            StreamReader file = new StreamReader("PositionMemory.txt");
            while ((line = file.ReadLine()) != null)
            {
                string[] data = line.Split(',');
                if (!_X.Contains(double.Parse(data[0]))) // Easy way to limit _X to grid values
                    _X.Add(double.Parse(data[0]));
                if (!_Y.Contains(double.Parse(data[1]))) // Easy way to limit _Y to grid values
                    _Y.Add(double.Parse(data[1]));
                _Z.Add(double.Parse(data[2]));
                _Zo.Add(double.Parse(data[5]));
            }
            file.Close();

            // Don't let use go into "uncharted" territory
            numX.Minimum = (decimal)_X.Min();
            numX.Maximum = (decimal)_X.Max();
            numX.Value = (decimal)(_X.Min() + _X.Max()) / 2;
            numY.Minimum = (decimal)_Y.Min();
            numY.Maximum = (decimal)_Y.Max();
            numY.Value = (decimal)(_Y.Min() + _Y.Max()) / 2;

            // Length of _Z and _Zo need to equal the length of _X times the length of _Y
            // len(_Z) == len(_Zo) == len(_X) * len(_Y)
            // Define the grid and init the bicubic splines interpolant

            alglib.spline2dbuildbicubicv(_X.ToArray(), _X.Count(), _Y.ToArray(), _Y.Count(), _Z.ToArray(), 1, out ZSpline);
            alglib.spline2dbuildbicubicv(_X.ToArray(), _X.Count(), _Y.ToArray(), _Y.Count(), _Zo.ToArray(), 1, out ZoSpline);

            _Loaded = true;
            MakePlots();
            updateAll();
        }

        private void num_Changed(object sender, EventArgs e)
        {
            if (!_Loaded) 
                return; // Will come here on form init
            updateAll();
        }

        private void updateAll()
        {
            // Get interpolated position
            double vZ = alglib.spline2dcalc(ZSpline, decimal.ToDouble(numX.Value), decimal.ToDouble(numY.Value));
            double vZo = alglib.spline2dcalc(ZoSpline, decimal.ToDouble(numX.Value), decimal.ToDouble(numY.Value));

            // Round and calc offset vals
            vZ = Math.Round(vZ, 3);
            double vZOff = Math.Round((vZ - _Z.Min()) * -1, 3);
            vZo = Math.Round(vZo, 3);
            double vZoOff = Math.Round((vZo - _Zo.Min()) * -1, 3);

            // Set labels
            lblZ.Text = vZ.ToString();
            lblZOffset.Text = vZOff.ToString();
            lblZo.Text = vZo.ToString();
            lblZoOffset.Text = vZoOff.ToString();

            MakeCursor();
        }

        private void MakeCursor()
        {
            Bitmap bmpZ = (Bitmap)pbxZ.BackgroundImage.Clone();
            using (Graphics G = Graphics.FromImage(bmpZ))
            {
                using (SolidBrush brush = new SolidBrush(Color.HotPink))
                    G.FillRectangle(brush, (int)((double)numX.Value - bmpZ.Width/100 - _X.Min()), 
                                           (int)((double)numY.Value - bmpZ.Height/100 - _Y.Min()),
                                           bmpZ.Width / 50, bmpZ.Height / 50);
            }
            pbxZ.Image = bmpZ;

            Bitmap bmpZo = (Bitmap)pbxZo.BackgroundImage.Clone();
            using (Graphics G = Graphics.FromImage(bmpZo))
            {
                using (SolidBrush brush = new SolidBrush(Color.HotPink))
                    G.FillRectangle(brush, (int)((double)numX.Value - bmpZ.Width / 100 - _X.Min()),
                                           (int)((double)numY.Value - bmpZ.Height / 100 - _Y.Min()),
                                           bmpZ.Width / 50, bmpZ.Height / 50);
            }
            pbxZo.Image = bmpZo;
        }

        private void MakePlots()
        {
            int[] xs = Enumerable.Range((int)_X.Min(), (int)(_X.Max() - _X.Min())).ToArray();
            int[] ys = Enumerable.Range((int)_Y.Min(), (int)(_Y.Max() - _Y.Min())).ToArray();

            double[,] Zs = new double[ys.Length, xs.Length];
            double[,] Zos = new double[ys.Length, xs.Length];
            for (int i = 0; i < ys.Length; i++)
            {
                for (int j = 0; j < xs.Length; j++)
                {
                    Zs[i, j] = alglib.spline2dcalc(ZSpline, j, i);
                    Zos[i, j] = alglib.spline2dcalc(ZoSpline, j, i);
                }
            }

            Bitmap bmpZ = new Bitmap(xs.Length, ys.Length);
            using (Graphics G = Graphics.FromImage(bmpZ))
            {
                float w = 1f * xs.Length / Zs.GetLength(0);
                float h = 1f * ys.Length / Zs.GetLength(1);
                for (int x = 0; x < Zs.GetLength(0); x++)
                    for (int y = 0; y < Zs.GetLength(1); y++)
                    {
                        int lux = (int)((Zs[x, y] - _Z.Max()) / (_Z.Max() - _Z.Min()) * 255);
                        if (lux < 0)
                            lux = 0;
                        if (lux > 255)
                            lux = 255;
                        using (SolidBrush brush = new SolidBrush(Color.FromArgb(255, lux, lux, lux)))
                            G.FillRectangle(brush, x * w, y * h, w, h);
                    }
            }
            bmpZ.RotateFlip(RotateFlipType.RotateNoneFlipX);
            pbxZ.BackgroundImage = bmpZ;

            Bitmap bmpZo = new Bitmap(xs.Length, ys.Length);
            using (Graphics G = Graphics.FromImage(bmpZo))
            {
                float w = 1f * xs.Length / Zos.GetLength(0);
                float h = 1f * ys.Length / Zos.GetLength(1);
                for (int x = 0; x < Zos.GetLength(0); x++)
                    for (int y = 0; y < Zos.GetLength(1); y++)
                    {
                        int lux = (int)((Zos[x, y] - _Zo.Max()) / (_Zo.Max() - _Zo.Min()) * 255);
                        if (lux < 0)
                            lux = 0;
                        if (lux > 255)
                            lux = 255;
                        using (SolidBrush brush = new SolidBrush(Color.FromArgb(255, lux, lux, lux)))
                            G.FillRectangle(brush, x * w, y * h, w, h);
                    }
            }
            bmpZo.RotateFlip(RotateFlipType.RotateNoneFlipX);
            pbxZo.BackgroundImage = bmpZo;
        }
    }
}
       
