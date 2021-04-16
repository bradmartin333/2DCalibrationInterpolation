static class Program
{
    private static string _Path = @"C:\Users\delta\Desktop\";
    private static Point _Range = new Point(100, 160);
    private static double[][,] _ChuckData = new double[][,] { new double[_Range.X, _Range.Y], new double[_Range.X, _Range.Y] };
    private static double[] _Averages = new double[_ChuckData.Length];
    private static double _OutlierThreshold = 0.025;
    private static double[,] _OutputData = new double[_Range.X, _Range.Y];

    static void Main()
    {
        GetData();
        CleanData();
        LinearFit();
        MakeOutputData();
        Application.Run(new Form1(_OutputData));
    }

    private static void GetData()
    {
        string[] files = new string[] { _Path + "TARGET", _Path + "SOURCE" };
        for (int i = 0; i < files.Length; i++)
        {
            string[] readText = File.ReadAllLines(files[i] + ".txt");
            double sum = 0.0;
            int count = 0;
            foreach (string line in readText)
            {
                string[] values = line.Split("\t");
                int x = Math.Abs(_Range.Y - (int.Parse(values[0]) / 5));
                int y = int.Parse(values[1]) / 5;
                double z = double.Parse(values[2]);
                sum += z;
                count++;
                _ChuckData[i][y, x] = z;
            }
            _Averages[i] = sum / count;
        }
    }

    private static void CleanData()
    {
        for (int i = 0; i < _ChuckData.Length; i++)
        {
            for (int j = 0; j < _Range.X; j++)
            {
                for (int k = 0; k < _Range.Y; k++)
                {
                    CheckGlobalOutlier(ref _ChuckData[i][j, k], i);
                    RemoveAverage(ref _ChuckData[i][j, k], i);
                }
            }
        }
    }

    private static void CheckGlobalOutlier(ref double x, int i)
    {
        if (x != 0 && (Math.Abs(x - _Averages[i]) / ((x + _Averages[i]) / 2)) > _OutlierThreshold)
        {
            x = _Averages[i];
        }
    }

    private static void RemoveAverage(ref double x, int i)
    {
        if (x != 0)
        {
            x -= _Averages[i];
        }
    }

    private static void LinearFit()
    {
        for (int i = 0; i < _ChuckData.Length; i++)
        {
            for (int j = 0; j < _Range.X; j++)
            {
                List<double> lineY = new List<double>();
                List<double> lineData = new List<double>();
                for (int k = 0; k < _Range.Y; k++)
                {
                    double data = _ChuckData[i][j, k];
                    if (data != 0)
                    {
                        lineY.Add(k);
                        lineData.Add(_ChuckData[i][j, k]);
                    }
                }

                if (lineData.Count == 0)
                {
                    continue;
                }

                var line = MathNet.Numerics.Fit.Line(lineY.ToArray(), lineData.ToArray());
                List<double> perfectData = new List<double>();
                for (int k = 0; k < lineData.Count; k++)
                {
                    _ChuckData[i][j, (int)lineY[k]] = line.Item2 * lineY[k] + line.Item1;
                }
            }
        }
    }

    private static void MakeOutputData()
    {
        for (int i = 0; i < _ChuckData.Length; i++)
        {
            for (int j = 0; j < _Range.X; j++)
            {
                for (int k = 0; k < _Range.Y; k++)
                {
                    if(_ChuckData[i][j, k] != 0)
                    {
                        _OutputData[j, k] = _ChuckData[i][j, k];
                    }  
                }
            }
        }
    }
}
