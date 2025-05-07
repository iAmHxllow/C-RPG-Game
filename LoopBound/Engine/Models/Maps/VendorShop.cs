using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models.Maps
{
    public class VendorShop : Map
    {
        public VendorShop() : base("Assets/VendorShop.gif", new Thickness(0))
        {
            // No restricted zones or map switch triggers defined for this map
        }
    }
}
