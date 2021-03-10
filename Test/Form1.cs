using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RDotNet;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            _Steps = new Point((_Max.X - _Min.X) / _Increment, (_Max.Y - _Min.Y) / _Increment);
            LoadPositons();
            CalculateDeltas();
            Interpolate();
        }

        private static int _Increment = 5;
        private Point _Min = new Point(0, 0);
        private Point _Max = new Point(500, 500);
        private Point _Steps;
        private double[,] _Positions = new double[5, 4];

        private void LoadPositons()
        {
            string line;
            int idx = 0;
            System.IO.StreamReader file = new System.IO.StreamReader("PositionMemory.txt");
            while ((line = file.ReadLine()) != null)
            {
                string[] data = line.Split(',');
                _Positions[idx, 0] = double.Parse(data[0]);
                _Positions[idx, 1] = double.Parse(data[1]);
                _Positions[idx, 2] = double.Parse(data[2]);
                _Positions[idx, 3] = double.Parse(data[5]);
                idx++;
            }
            file.Close();
        }

        private void CalculateDeltas()
        {
            double MinZ = 1e10;
            double MinZo = 0;
            for (int i = 0; i < 5; i++)
            {
                if (_Positions[i, 2] < MinZ)
                    MinZ = _Positions[i, 2];
                if (_Positions[i, 3] < MinZo)
                    MinZo = _Positions[i, 3];
            }
            for (int i = 0; i < 5; i++)
            {
                _Positions[i, 2] -= MinZ;
                _Positions[i, 3] -= MinZo;
            }
        }

        private void Interpolate()
        {
           
        }
    }
}
       
