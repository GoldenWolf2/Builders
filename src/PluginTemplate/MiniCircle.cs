using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniGamesAPI.Core
{
    public class MiniCircle
    {
        public double x;
        public double y;
        public double radius;
        public MiniCircle(double x,double y,double radius) 
        {
            this.x = x;
            this.y = y;
            this.radius = radius;
        }

        public bool Contain(Point point) 
        {
            double x = point.X * 16f;
            double y = point.Y * 16f;
            bool flag = Math.Abs((this.x-x)* (this.x - x) - (this.y - y) * (this.y - y)) <=radius*radius;
            return flag;
        }
        public bool Contain(float x,float y) 
        {
            bool flag = Math.Abs((this.x - x) * (this.x - x) - (this.y - y) * (this.y - y)) <= radius * radius;
            return flag;
        }
    }
}
