using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolyInterp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadPositons();
        }

        private List<double> _X = new List<double>(), _Y = new List<double>(), _Zo = new List<double>();

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
        }
    }
}
