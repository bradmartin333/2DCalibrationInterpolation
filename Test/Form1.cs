using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static interpx.Bilinear;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadPositons();
        }

        private List<double> _X = new List<double>(), _Y = new List<double>(), _Zo = new List<double>();
        private Grid ZoGrid;
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
            ZoGrid = new Grid(_Zo[2], _Zo[0], _Zo[1], _Zo[3], _X.Max(), _X.Min(), _Y.Max(), _Y.Min());

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
            double vZo = Interp2D(ZoGrid, decimal.ToDouble(numX.Value), decimal.ToDouble(numY.Value));

            // Round and calc offset val
            vZo = Math.Round(vZo, 3);
            double vZoOff = Math.Round((vZo - _Zo.Min()) * -1, 3);

            // Set labels
            lblZo.Text = vZo.ToString();
            lblZoOffset.Text = vZoOff.ToString();

            MakeCursor();
        }

        //
        // Everything beyond here is for the heatmap
        //

        private void pbxZo_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.None)
                return;

            PointF click = ZoomMousePos(new Point(e.X, e.Y), pbxZo);
            PointF clickRatio = new PointF(click.X / pbxZo.BackgroundImage.Width, click.Y / pbxZo.BackgroundImage.Height);

            decimal thisX = (decimal)(_X.Max() - (clickRatio.X * (_X.Max() - _X.Min())));
            decimal thisY = (decimal)(_Y.Min() + (clickRatio.Y * (_Y.Max() - _Y.Min())));

            if (thisX < numX.Minimum)
                thisX = numX.Minimum;
            if (thisX > numX.Maximum)
                thisX = numX.Maximum;
            if (thisY < numY.Minimum)
                thisY = numY.Minimum;
            if (thisY > numY.Maximum)
                thisY = numY.Maximum;

            numX.Value = thisX;
            numY.Value = thisY;

            MakeCursor();
        }

        private void MakeCursor()
        {
            Bitmap bmpZo = new Bitmap(pbxZo.BackgroundImage.Width, pbxZo.BackgroundImage.Height);
            using (Graphics G = Graphics.FromImage(bmpZo))
            {
                using (SolidBrush brush = new SolidBrush(Color.White))
                    G.FillRectangle(brush, (int)((double)numX.Value - bmpZo.Width / 70 - _X.Min()),
                                           (int)((double)numY.Value - bmpZo.Height / 70 - _Y.Min()),
                                           bmpZo.Width / 35, bmpZo.Height / 35);
                using (SolidBrush brush = new SolidBrush(Color.Black))
                    G.FillRectangle(brush, (int)((double)numX.Value - bmpZo.Width / 100 - _X.Min()),
                                           (int)((double)numY.Value - bmpZo.Height / 100 - _Y.Min()),
                                           bmpZo.Width / 50, bmpZo.Height / 50);
            }
            bmpZo.RotateFlip(RotateFlipType.RotateNoneFlipX);
            pbxZo.Image = bmpZo;
        }

        private void MakePlots()
        {
            int[] xs = Enumerable.Range((int)_X.Min(), (int)(_X.Max() - _X.Min())).ToArray();
            int[] ys = Enumerable.Range((int)_Y.Min(), (int)(_Y.Max() - _Y.Min())).ToArray();

            double[,] Zos = new double[ys.Length, xs.Length];
            for (int i = 0; i < ys.Length; i++)
            {
                for (int j = 0; j < xs.Length; j++)
                {
                    Zos[i, j] = Interp2D(ZoGrid, j, i);
                }
            }

            Bitmap bmpZo = new Bitmap(xs.Length, ys.Length);
            using (Graphics G = Graphics.FromImage(bmpZo))
            {
                float w = 1f * xs.Length / Zos.GetLength(0);
                float h = 1f * ys.Length / Zos.GetLength(1);
                for (int x = 0; x < Zos.GetLength(0); x++)
                    for (int y = 0; y < Zos.GetLength(1); y++)
                    {
                        double lux = ((Zos[x, y] - _Zo.Max()) / (_Zo.Max() - _Zo.Min()));
                        if (lux < 0)
                            lux = 0;
                        if (lux > 255)
                            lux = 255;
                        using (SolidBrush brush = new SolidBrush(Lux2Color(lux)))
                            G.FillRectangle(brush, x * w, y * h, w, h);
                    }
            }

            pbxZo.BackgroundImage = bmpZo;
        }

        public PointF ZoomMousePos(Point click, PictureBox pbx)
        {
            double imageAspect = pbx.BackgroundImage.Width / (double)pbx.BackgroundImage.Height;
            double controlAspect = pbx.Width / (double)pbx.Height;
            PointF pos = click;
            if (imageAspect > controlAspect)
            {
                double ratioWidth = pbx.BackgroundImage.Width / (double)pbx.Width;
                pos.X *= (float)ratioWidth;
                double scale = pbx.Width / (double)pbx.BackgroundImage.Width;
                double displayHeight = scale * pbx.BackgroundImage.Height;
                double diffHeight = pbx.Height - displayHeight;
                diffHeight /= 2;
                pos.Y -= (float)diffHeight;
                pos.Y /= (float)scale;
            }
            else
            {
                double ratioHeight = pbx.BackgroundImage.Height / (double)pbx.Height;
                pos.Y *= (int)ratioHeight;
                double scale = pbx.Height / (double)pbx.BackgroundImage.Height;
                double displayWidth = scale * pbx.BackgroundImage.Width;
                double diffWidth = pbx.Width - displayWidth;
                diffWidth /= 2;
                pos.X -= (float)diffWidth;
                pos.X /= (float)scale;
            }
            return pos;
        }

        public static Color Lux2Color(double lux)
        {
            double r = 0.5;
            double g = 0.5;
            double b = 0.5;
            double v = 0.75;
            if (v > 0)
            {
                double m = 1 - v;
                double sv = (v - m) / v;
                lux *= 6.0;
                int sextant = (int)lux;
                double fract = lux - sextant;
                double vsf = v * sv * fract;
                double mid1 = m + vsf;
                double mid2 = v - vsf;
                switch (sextant)
                {
                    case 0:
                        r = v;
                        g = mid1;
                        b = m;
                        break;
                    case 1:
                        r = mid2;
                        g = v;
                        b = m;
                        break;
                    case 2:
                        r = m;
                        g = v;
                        b = mid1;
                        break;
                    case 3:
                        r = m;
                        g = mid2;
                        b = v;
                        break;
                    case 4:
                        r = mid1;
                        g = m;
                        b = v;
                        break;
                    case 5:
                        r = v;
                        g = m;
                        b = mid2;
                        break;
                }
            }
            return Color.FromArgb(255, Convert.ToByte(r * 255.0f), Convert.ToByte(g * 255.0f), Convert.ToByte(b * 255.0f));
        }
    }
}
       
