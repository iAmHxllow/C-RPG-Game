using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Engine.Models.Maps
{
    public class Map
    {
        public string MapPath { get; set; } // Path to the map file
        public List<RestrictedZone> RestrictedZones { get; set; } = new List<RestrictedZone>(); // List of restricted zones
        public List<MapSwitchTrigger> MapSwitchTriggers { get; set; } = new List<MapSwitchTrigger>(); // List of map switch triggers
        public Thickness DefaultPlayerPosition { get; set; } // Default player position

        // Constructor
        public Map(string mapPath, Thickness defaultPlayerPosition)
        {
            MapPath = mapPath;
            DefaultPlayerPosition = defaultPlayerPosition;
        }

        public void AddRestrictedZone(double x, double y, double width, double height)
        {
            RestrictedZones.Add(new RestrictedZone(x, y, width, height));
        }

        public void AddMapSwitchTrigger(double x, double y, double width, double height, string targetMapPath)
        {
            MapSwitchTriggers.Add(new MapSwitchTrigger(x, y, width, height, targetMapPath));
        }
    }
}
