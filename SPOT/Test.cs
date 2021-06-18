using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPOT
{
    public partial class Test : Form
    {
        private static PointF SW = new PointF(-2.561f, -2.967f);
        private static PointF SE = new PointF(4.095f, -2.959f);
        private static PointF NW, NE;
        private static PointF Pitch = new PointF(0.4f, 0.4f);
        private static PointF Num = new PointF(40, 40);
        private static int Step = 25; // Microns
        private static double[,] Data;
        private static int NumX = 100;
        private static int NumY = 100;
        private static double Angle;

        public Test()
        {
            InitializeComponent();
            Angle = Math.Atan2(SE.Y - SW.Y, SE.X - SW.X) * 180 / Math.PI;
            Data = new double[NumX, NumY];

            var model = new PlotModel { Title = "ScatterSeries" };
            scatterPlotView.Model = model;

            NW = new PointF(SW.X, SW.Y + Num.Y * Pitch.Y);
            NE = new PointF(SE.X, SE.Y + Num.Y * Pitch.Y);
            RotatePoint(ref NW);
            RotatePoint(ref NE);

            List<PointF> points = new List<PointF>() { SW, SE, NW, NE };
            RectangleF rectangle = GetBoundingRect(points);

            for (int i = 0; i < rectangle.Width * 1000; i += Step)
            {
                for (int j = 0; j < rectangle.Height * 1000; j += Step)
                { 
                    PointF scanCoord = new PointF(i, j);
                    RotatePoint(ref scanCoord);
                    Random random = new Random((i+1)%(j+1));
                    var scatterSeries = new ScatterSeries()
                    {
                        MarkerSize = 1,
                        MarkerStroke = OxyColor.FromRgb((byte)(random.NextDouble() * 255), (byte)(random.NextDouble() * 255), (byte)(random.NextDouble() * 255))
                    };
                    scatterSeries.Points.Add(new ScatterPoint(scanCoord.X, scanCoord.Y));
                    scatterPlotView.Model.Series.Add(scatterSeries);
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
    }
}
