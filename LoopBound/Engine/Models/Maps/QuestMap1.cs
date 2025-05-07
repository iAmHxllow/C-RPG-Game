using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models.Maps
{
    public class QuestMap1 : Map
    {
        public QuestMap1() : base("Assets/QuestMap1.gif", new Thickness(40, 269, 686, 175))
        {
            AddRestrictedZone(10, 10, 25, 190);
            AddRestrictedZone(77, 10, 67, 216);
            AddRestrictedZone(150, 10, 620, 75);
            AddRestrictedZone(250, 90, 520, 167);
            AddRestrictedZone(10, 393, 135, 111);
            AddRestrictedZone(150, 426, 434, 78);
            AddRestrictedZone(589, 393, 37, 111);
            AddRestrictedZone(542, 262, 228, 65);
            AddRestrictedZone(631, 487, 94, 17);
            AddRestrictedZone(365, 272, 44, 51);
            AddMapSwitchTrigger(10, 269, 7, 72, "Assets/Home.png");
            AddMapSwitchTrigger(728, 425, 22, 54, "Assets/QuestMap2.png");
            AddMapSwitchTrigger(40, 174, 32, 58, "Assets/VendorRoute.png");
        }
    }
}
