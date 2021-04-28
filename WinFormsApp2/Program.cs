using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp2
{
    static class Program
    {
        private static int Increment = 5;
        private static string _Path = @"C:\Users\delta\Desktop\";
        private static Point _Range = new Point(800, 500);
        private static double[,] _ChuckData;
        private static double _Average;
        private static double _OutlierThreshold = 0.025;
        private static Point _Steps;

        static void Main()
        {
            _Steps = new Point((int)_Range.X / Increment, (int)_Range.Y / Increment);
            _ChuckData = new double[_Steps.Y, _Steps.X];
            FilterData();
            Application.Run(new Form1(_ChuckData));
        }

        private static void FilterData()
        {
            // Check for data
            string chuckPath = _Path + "TARGET.txt";
            if (!File.Exists(chuckPath))
            {
                return;
            }

            // Get data
            string[] readText = File.ReadAllLines(chuckPath);
            double sum = 0.0;
            int count = 0;
            foreach (string line in readText)
            {
                string[] values = line.Split('\t');
                int x = int.Parse(values[0]) / Increment;
                int y = int.Parse(values[1]) / Increment;
                double z = double.Parse(values[2]);
                sum += z;
                count++;
                _ChuckData[y, x] = z;
            }
            _Average = sum / count;

            // Clean data
            for (int j = 0; j < _Steps.Y; j++)
            {
                for (int i = 0; i < _Steps.X; i++)
                {
                    CheckGlobalOutlier(ref _ChuckData[j, i]);
                    RemoveAverage(ref _ChuckData[j, i]);
                }
            }

            // Linear fit
            for (int j = 0; j < _Steps.Y; j++)
            {
                List<double> lineX = new List<double>();
                List<double> lineData = new List<double>();
                for (int i = 0; i < _Steps.X; i++)
                {
                    double data = _ChuckData[j, i];
                    if (data != 0)
                    {
                        lineX.Add(i);
                        lineData.Add(_ChuckData[j, i]);
                    }
                }

                if (lineData.Count < 2) // Not enough data for line fitting
                {
                    continue;
                }

                var line = MathNet.Numerics.Fit.Line(lineX.ToArray(), lineData.ToArray());
                for (int i = 0; i < lineData.Count; i++)
                {
                    _ChuckData[j, (int)lineX[i]] = line.Item2 * lineX[i] + line.Item1;
                }
            }

            // Export
            for (int j = 0; j < _Steps.Y; j++)
            {
                for (int i = 0; i < _Steps.X; i++)
                {
                    if (_ChuckData[j, i] != 0)
                    {
                        File.AppendAllText(_Path + "CLEANED.txt", string.Format("{0},{1},{2}\n", i * Increment, j * Increment, Math.Round(_ChuckData[j, i], 3)));
                    }
                }
            }
        }
        private static void CheckGlobalOutlier(ref double x)
        {
            if (x != 0 && (Math.Abs(x - _Average) / ((x + _Average) / 2)) > _OutlierThreshold)
                x = _Average;
        }

        private static void RemoveAverage(ref double x)
        {
            if (x != 0)
                x -= _Average;
        }
    }
}
