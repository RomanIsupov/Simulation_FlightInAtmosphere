using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Simulation_FlightInAtmosphere.Flight;

namespace Simulation_FlightInAtmosphere
{
    public partial class Form1 : Form
    {
        private Flight.Flight flight = new Flight.Flight();
        private Point current;
        private bool isStarted = false;
        private double t = 0D;
        private const double dt = 0.01D;

        public Form1()
        {
            InitializeComponent();
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            if (isStarted)
            {
                timer1.Start();
                return;
            }

            flight.SetParameters((double)edAngle.Value, (double)edSpeed.Value, (double)edHeight.Value, (double)edWeight.Value, (double)edSquare.Value);

            chart1.Series[0].Points.Clear();
            chart1.Series[0].Points.AddXY(0, (double)edHeight.Value);
            isStarted = true;
            if (!timer1.Enabled)
            {
                timer1.Start();
            }
            t = 0D;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            current = flight.ToNextState();
            chart1.Series[0].Points.AddXY(current.x, current.y);
            t += dt;
            labTime.Text = $"Time: {Math.Round(t, 2)}";
            if (current.y <= 0)
            {
                timer1.Stop();
                isStarted = false;
            }
        }

        private void btStop_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Stop();
            }
        }
    }
}
