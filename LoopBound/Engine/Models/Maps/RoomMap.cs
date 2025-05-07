using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models.Maps
{
    public class RoomMap : Map
    {
        public RoomMap() : base("Assets/Room.png", new Thickness(356, 303, 326, 110))
        {
            AddRestrictedZone(10, 417, 336, 92);
            AddRestrictedZone(463, 417, 307, 92);
            AddRestrictedZone(595, 10, 175, 401);
            AddRestrictedZone(10, 10, 175, 401);
            AddRestrictedZone(203, 10, 390, 171);
            AddMapSwitchTrigger(347, 412, 113, 75, "Assets/Home.png");

        }
    }
}
