using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models.Maps
{
    public class VendorRoute : Map
    {
        public VendorRoute() : base("Assets/VendorRoute.png", new Thickness(363, 334, 363, 97))
        {
            AddRestrictedZone(10, 0, 276, 494);
            AddRestrictedZone(300, 80, 26, 86);
            AddRestrictedZone(452, 80, 26, 86);
            AddRestrictedZone(494, 0, 276, 494);
            AddRestrictedZone(291, 327, 37, 170);
            AddRestrictedZone(446, 327, 43, 170);
            AddMapSwitchTrigger(346, 437, 84, 44, "Assets/QuestMap1.gif");
            AddMapSwitchTrigger(347, 80, 84, 86, "Assets/VendorShop.gif");
        }
    }
}
