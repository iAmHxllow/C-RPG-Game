using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models.Maps
{
    public class BossRoom : Map
    {
        public BossRoom() : base("Assets/BossRoom.gif", new Thickness(0))
        {
            // No restricted zones or map switch triggers defined for this map
        }
    }
}
