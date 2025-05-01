using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Threading.Tasks;



namespace Engine.Models
{
    public class RestrictedZone
    {
        public Rect Zone { get; private set; }

        // Constructor
        public RestrictedZone(double x, double y, double width, double height)
        {
            Zone = new Rect(x, y, width, height);
        }

        // Method to update the restricted zone's position and size
        public void UpdateZone(double x, double y, double width, double height)
        {
            Zone = new Rect(x, y, width, height);
        }

        // Method to check if the restricted zone intersects with another rectangle
        public bool Intersects(Rect other)
        {
            return Zone.IntersectsWith(other);
        }
    }
}
