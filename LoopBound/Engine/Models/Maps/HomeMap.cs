using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models.Maps
{
    public class HomeMap : Map
    {
        public HomeMap() : base("Assets/Home.png", new Thickness(18, 312, 664, 101))
        {
            AddRestrictedZone(0, 0, 310, 277);
            AddRestrictedZone(454, 0, 316, 276);
            AddRestrictedZone(310, 0, 144, 204);
            AddRestrictedZone(0, 426, 770, 86);
            AddMapSwitchTrigger(344, 171, 99, 122, "Assets/Room.png");
            AddMapSwitchTrigger(749, 286, 21, 135, "Assets/QuestMap1.gif");
        }
    }
}
