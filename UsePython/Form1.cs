using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace UsePython
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            StartInterpolationEngine();
            InitializeComponent();
            CreateTable();

            streamWriter = interpolator.StandardInput;
            while (!initialized)
            {
                streamWriter.WriteLine(string.Format("{0} {1} {2} {3} {4}", 0, 0, XString, YString, ZString));
                double val = double.NaN;
                double.TryParse(lblZ.Text, out val);
                if (!val.Equals(double.NaN))
                    initialized = true;
            }
        }

        Process interpolator = new Process();
        StreamWriter streamWriter;
        string XString, YString, ZString;
        bool initialized;

        private void CreateTable()
        {
            XString = StringifyData(new double[] { 660.481, 558.429, 467.500, 705.935, 416.154, 672.047, 558.429, 466.123 });
            YString = StringifyData(new double[] { 204.098, 156.438, 188.269, 292.337, 292.337, 385.686, 440.898, 404.919 });
            ZString = StringifyData(new double[] { -26.029, -26.021, -26.015, -26.029, -26.019, -26.027, -26.020, -26.022 });
        }

        private string StringifyData(double[] data)
        {
            string output = "";
            for (int i = 0; i < data.Length; i++)
            {
                output += data[i].ToString();
                if (i != data.Length - 1)
                    output += ',';
            }
            return output;
        }

        private void num_ValueChanged(object sender, EventArgs e)
        {
            streamWriter.WriteLine(string.Format("{0} {1} {2} {3} {4}", numX.Value, numY.Value, XString, YString, ZString));
        }

        private void StartInterpolationEngine()
        {
            string strExeFilePath = System.Reflection.Assembly.GetEntryAssembly().Location;
            string exeDir = Path.GetDirectoryName(strExeFilePath);
            interpolator.StartInfo.FileName = exeDir + "\\Properties\\interp2d.exe";
            interpolator.StartInfo.UseShellExecute = false;
            interpolator.StartInfo.CreateNoWindow = true;
            interpolator.StartInfo.RedirectStandardInput = true;
            interpolator.StartInfo.RedirectStandardOutput = true;
            interpolator.OutputDataReceived += UpdatePosition;
            interpolator.Start();
            interpolator.StandardInput.AutoFlush = true;
            interpolator.BeginOutputReadLine();
        }

        private void UpdatePosition(object sender, DataReceivedEventArgs e)
        {
            string data;
            if (e.Data == "nan")
                data = "0.000000";
            else
                data = Math.Round(decimal.Parse(e.Data), 6).ToString();
            lblZ.Invoke(new MethodInvoker(delegate { lblZ.Text = data; }));
        }
    }
}
