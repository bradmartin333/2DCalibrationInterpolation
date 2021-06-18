using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SPOT
{
    public partial class Test : Form
    {
        private static PointF SW = new PointF(-2.561f, -2.967f);
        private static PointF SE = new PointF(4.095f, -2.959f);
        private static PointF NW, NE;
        private static PointF Pitch = new PointF(0.35f, 0.25f);
        private static PointF Num = new PointF(20, 28);
        private static float Step = 0.020f;
        private static double Angle;
        private static ScatterSeries[] ScatterSeries;

        public Test()
        {
            InitializeComponent();
            Angle = Math.Atan2(SE.Y - SW.Y, SE.X - SW.X) * 180 / Math.PI;

            var model = new PlotModel { Title = "ScatterSeries" };
            scatterPlotView.Model = model;

            NW = new PointF(SW.X, SW.Y + Num.Y * Pitch.Y);
            NE = new PointF(SE.X, SE.Y + Num.Y * Pitch.Y);
            RotatePoint(ref NW);
            RotatePoint(ref NE);

            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            List<PointF> points = new List<PointF>() { SW, SE, NW, NE };
            RectangleF rectangle = GetBoundingRect(points);

            ScatterSeries = new ScatterSeries[(int)((rectangle.Width / Step) + 1) * (int)((rectangle.Height / Step) + 1)];
            int seriesIdx = 0;

            for (int i = 0; i < ScatterSeries.Length; i++)
            {
                ScatterSeries[i] = new ScatterSeries();
                ScatterSeries[i].MarkerSize = 1;
                scatterPlotView.Model.Series.Add(ScatterSeries[i]);
            }

            LinearAxis XAxis = new LinearAxis()
            {
                Position = AxisPosition.Bottom,
                Minimum = rectangle.Left,
                Maximum = rectangle.Right
            };

            LinearAxis YAxis = new LinearAxis()
            {
                Position = AxisPosition.Left,
                Minimum = rectangle.Top,
                Maximum = rectangle.Bottom
            };

            scatterPlotView.Invoke(new EventHandler(delegate {
                scatterPlotView.Model.Axes.Add(XAxis);
                scatterPlotView.Model.Axes.Add(YAxis);
                scatterPlotView.Visible = true; 
                Refresh(); 
            }));

            for (float i = rectangle.Left; i < rectangle.Right; i += Step)
            {
                for (float j = rectangle.Bottom; j > rectangle.Top; j -= Step)
                {
                    PointF scanCoord = new PointF(i, j);
                    RotatePoint(ref scanCoord);
                    Random random = new Random((int)((i + 1) * (j + 1)));
                    Color color = GetScoreColor(random.NextDouble() * 3);
                    ScatterSeries[seriesIdx].MarkerStroke = OxyColor.FromRgb(color.R, color.G, color.B);
                    ScatterSeries[seriesIdx].Points.Add(new ScatterPoint(scanCoord.X, scanCoord.Y));
                    System.Threading.Thread.Sleep(1);
                    seriesIdx++;
                    scatterPlotView.Model.InvalidatePlot(true);
                }
            }
        }

        public static void RotatePoint(ref PointF point)
        {
            float x = (float)(point.X * Math.Cos(Math.PI / 180d * Angle) - point.Y * Math.Sin(Math.PI / 180d * Angle));
            float y= (float)(point.X * Math.Sin(Math.PI / 180d * Angle) + point.Y * Math.Cos(Math.PI / 180d * Angle));
            point = new PointF(x, y);
        }

        public RectangleF GetBoundingRect(List<PointF> points)
        {
            float minX = points.Min(p => p.X);
            float minY = points.Min(p => p.Y);
            float maxX = points.Max(p => p.X);
            float maxY = points.Max(p => p.Y);
            return new RectangleF(new PointF(minX, minY), new SizeF(maxX - minX, maxY - minY));
        }

        public struct ColorRGB
        {
            public byte R;
            public byte G;
            public byte B;

            public ColorRGB(Color value)
            {
                this.R = value.R;
                this.G = value.G;
                this.B = value.B;
            }

            public static implicit operator Color(ColorRGB rgb)
            {
                Color c = Color.FromArgb(rgb.R, rgb.G, rgb.B);
                return c;
            }

            public static explicit operator ColorRGB(Color c)
            {
                return new ColorRGB(c);
            }
        }

        public static Color GetScoreColor(double score)
        {
            ColorRGB c = HSL2RGB(1 / score, 0.5, 0.5);
            return Color.FromArgb(100, c.R, c.G, c.B);
        }

        // Given H,S,L in range of 0-1
        // Returns a Color (RGB struct) in range of 0-255
        public static ColorRGB HSL2RGB(double h, double sl, double l)
        {
            double v;
            double r, g, b;

            r = l;   // default to gray
            g = l;
            b = l;
            v = (l <= 0.5) ? (l * (1.0 + sl)) : (l + sl - l * sl);

            if (v > 0)
            {
                double m;
                double sv;
                int sextant;
                double fract, vsf, mid1, mid2;

                m = l + l - v;
                sv = (v - m) / v;
                h *= 1.0; // orig is 6.0
                sextant = (int)h;
                fract = h - sextant;
                vsf = v * sv * fract;
                mid1 = m + vsf;
                mid2 = v - vsf;

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

            ColorRGB rgb;
            rgb.R = Convert.ToByte(r * 255.0f);
            rgb.G = Convert.ToByte(g * 255.0f);
            rgb.B = Convert.ToByte(b * 255.0f);
            return rgb;
        }
    }
}
