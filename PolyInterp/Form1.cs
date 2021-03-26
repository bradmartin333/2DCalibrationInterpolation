using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PolyInterp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadPositons();
            IterateGrid();
        }

        private List<PointF> _Poly = new List<PointF>();
        private List<PointF[]> _InterpPoly = new List<PointF[]>();
        private List<float> _Z = new List<float>();

        private void LoadPositons()
        {
            string line;
            StreamReader file = new StreamReader("PositionMemory.txt");
            while ((line = file.ReadLine()) != null)
            {
                string[] data = line.Split(',');
                _Poly.Add(new PointF(float.Parse(data[0]), float.Parse(data[1])));
                _Z.Add(float.Parse(data[2]));
            }
            file.Close();
        }

        private void IterateGrid()
        {
            float MinX = 1e6f;
            float MinY = 1e6f;
            float MaxX = 0f;
            float MaxY = 0f;

            foreach (PointF p in _Poly)
            {
                if (p.X < MinX)
                {
                    MinX = p.X;
                }
                else if (p.X > MaxX)
                {
                    MaxX = p.X;
                }
                if (p.Y < MinY)
                {
                    MinY = p.Y;
                }
                else if (p.Y > MaxY)
                {
                    MaxY = p.Y;
                }
            }

            for (float i = MinX; i < MaxX; i+=10)
            {
                for (float j = MinY; j < MaxY; j+=10)
                {
                    PointF P1 = new PointF(i, j);
                    PointF P2 = new PointF(i+10, j+10);

                    if (InPolygon(P1) && InPolygon(P2))
                        _InterpPoly.Add(new PointF[] { P1, P2 });
                }
            }

            double[,] HeatArr = new double[(int)(MaxY-MinY), (int)(MaxX-MinX)];
            foreach (PointF[] poly in _InterpPoly)
            {  
                var rand = new Random();
                Interpolator interp = new Interpolator(rand.NextDouble(), rand.NextDouble(), rand.NextDouble(), rand.NextDouble(), poly[0].X, poly[1].X, poly[0].Y, poly[1].Y);
                for (double i = interp.X1; i < interp.X2; i ++)
                {
                    for (double j = interp.Y1; j < interp.Y2; j ++)
                    {
                        HeatArr[(int)(j-MinY), (int)(i-MinX)] = interp.Get(i, j);
                    }
                }
            }
            plot.plt.PlotHeatmap(HeatArr);
        }

        private bool InPolygon(PointF testPoint)
        {
            bool result = false;
            int j = _Poly.Count - 1;
            for (int i = 0; i < _Poly.Count; i++)
            {
                if (_Poly[i].Y < testPoint.Y && _Poly[j].Y >= testPoint.Y || _Poly[j].Y < testPoint.Y && _Poly[i].Y >= testPoint.Y)
                {
                    if (_Poly[i].X + (testPoint.Y - _Poly[i].Y) / (_Poly[j].Y - _Poly[i].Y) * (_Poly[j].X - _Poly[i].X) < testPoint.X)
                    {
                        result = !result;
                    }
                }
                j = i;
            }
            return result;
        }
    }
}
