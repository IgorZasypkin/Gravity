using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gravity
{
    public class GOBject
    {
        public string Name = string.Empty;
        public double Mass = 0;
        public Color Color;
        public double X = 0;
        public double Y = 0;
        public double VX = 0;
        public double VY = 0;
        public double AX = 0;
        public double AY = 0;
        public float K = 1f;

        public GOBject() { }

        public GOBject(string name, double mass, Color color, double x, double y, double vX, double vY)
        {
            Name = name;
            Mass = mass;
            Color = color;
            X = x;
            Y = y;
            VX = vX;
            VY = vY;

            if (Mass > 0 && Mass < 1)
            {
                K = 2f;
            }
            else if (Mass >= 1 )
            {
                K = 3f;
            }
        }

        public GOBject (string name, double mass, Color color, double x, double y, double vX, double vY, double aX, double aY)
        {
            Name = name;
            Mass = mass;
            Color = color;
            X = x;
            Y = y;
            VX = vX;
            VY = vY;
            AX = aX;
            AY = aY;

            if (Mass > 1)
            {
                K = 2f;
            }
        }
    }
}
