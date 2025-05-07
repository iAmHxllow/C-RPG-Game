using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models.Maps
{
    public class QuestMap3 : Map
    {
        public QuestMap3() : base("Assets/QuestMap3.png", new Thickness(254, 70, 470, 371))
        {
            AddRestrictedZone(10, 10, 225, 384);
            AddRestrictedZone(334, 13, 436, 144);
            AddRestrictedZone(238, 229, 258, 165);
            AddRestrictedZone(592, 233, 178, 50);
            AddRestrictedZone(717, 322, 9, 16); // Flower
            AddRestrictedZone(717, 452, 9, 16); // Flower
            AddMapSwitchTrigger(245, 13, 75, 34, "Assets/QuestMap2.png");
            AddMapSwitchTrigger(12, 441, 49, 62, "Assets/BossRoom.gif");
        }
    }
}
