using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation_FlightInAtmosphere.Flight
{
    public class Flight
    {
        private const double dT = 0.1;
        private const double G = 9.81;
        private const double C = 0.15;
        private const double RHO = 1.29;

        private double k;
        private double vx;
        private double vy;

        private Point point = new Point
        {
            x = 0,
            y = 0
        };

        public delegate double TrigonometricFunction(double angle);

        public void SetParameters(double angle, double v0, double y0, double weight, double area)
        {
            k = 0.5 * C * area * RHO / weight;
            
            vx = GetSpeed(v0, new TrigonometricFunction(Math.Cos), angle);
            vy = GetSpeed(v0, new TrigonometricFunction(Math.Sin), angle);
            point.x = 0;
            point.y = y0;
        }

        public Point ToNextState()
        {
            vx -= k * vx * Math.Sqrt(vx * vx + vy * vy) * dT;
            vy -= (G + k * vy * Math.Sqrt(vx * vx + vy * vy)) * dT;

            point.x += vx * dT;
            point.y += vy * dT;
            return point;
        }

        private double DegreesToRadians(double angle)
        {
            return angle * Math.PI / 180;
        }

        private double GetSpeed(double v0, TrigonometricFunction function, double angle)
        {
            return v0 * function(DegreesToRadians(angle));
        }
    }
}
