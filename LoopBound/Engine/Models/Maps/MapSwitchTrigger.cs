using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models.Maps
{
    public class MapSwitchTrigger
    {
        public Rect TriggerArea { get; set; } // The area that triggers the map switch
        public string TargetMapPath { get; set; } // The path to the target map

        // Constructor
        public MapSwitchTrigger(double x, double y, double width, double height, string targetMapPath)
        {
            TriggerArea = new Rect(x, y, width, height);
            TargetMapPath = targetMapPath;
        }

        // Method to update the trigger area and target map path
        public void UpdateTrigger(double x, double y, double width, double height, string targetMapPath)
        {
            TriggerArea = new Rect(x, y, width, height);
            TargetMapPath = targetMapPath;
        }

        // Method to check if the player is within the trigger area
        public bool IsPlayerInTrigger(Rect playerRect)
        {
            return TriggerArea.IntersectsWith(playerRect);
        }
    }
}
