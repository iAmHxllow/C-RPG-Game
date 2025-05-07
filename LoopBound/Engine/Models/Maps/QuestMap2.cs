using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models.Maps
{
    public class QuestMap2 : Map
    {
        public QuestMap2() : base("Assets/QuestMap2.png", new Thickness(30, 47, 700, 381))
        {
            AddRestrictedZone(96, 10, 244, 111);
            AddRestrictedZone(345, 48, 90, 166);
            AddRestrictedZone(440, 10, 330, 121);
            AddRestrictedZone(10, 160, 36, 344);
            AddRestrictedZone(141, 240, 80, 166);
            AddRestrictedZone(267, 203, 20, 108);
            AddRestrictedZone(345, 322, 20, 108);
            AddRestrictedZone(477, 306, 90, 166);
            AddRestrictedZone(655, 203, 69, 175);
            AddRestrictedZone(51, 456, 413, 48);
            AddRestrictedZone(469, 475, 117, 29);
            AddRestrictedZone(661, 456, 109, 48);
            AddMapSwitchTrigger(35, 10, 45, 24, "Assets/QuestMap1.gif");
            AddMapSwitchTrigger(599, 447, 46, 48, "Assets/QuestMap3.png");
        }
    }
}
