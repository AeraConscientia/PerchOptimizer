using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIS
{
    public class Perch
    {
        public Vector coords;
        public double fitness = 0;
        
        public Perch()
        { 
            coords = new Vector(0, 0); 
            fitness = 0; 
        }
        
        public Perch(double x, double y, double fitness)
        {
            coords = new Vector(x, y);
            this.fitness = fitness;
        }
    }
}
