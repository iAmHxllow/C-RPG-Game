using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class PlayerMovementHandler
    {
        private UIElement _player;
        private List<RestrictedZone> _restrictedZones;
        private List<MapSwitchTrigger> _mapSwitchTriggers;
        private Action<string> _switchMapCallback;


        // Constructor
        public PlayerMovementHandler(UIElement player, List<RestrictedZone> restrictedZones, List<MapSwitchTrigger> mapSwitchTriggers, Action<string> switchMapCallback)
        {
            _player = player;
            _restrictedZones = restrictedZones;
            _mapSwitchTriggers = mapSwitchTriggers;
            _switchMapCallback = switchMapCallback;
        }


        // Method to handle player movement based on key input
        public void HandleMovement(Key key, Thickness boundaries, int moveAmount)
        {
            var margin = ((FrameworkElement)_player).Margin;
            Thickness newMargin = margin;

            // Adjust the player's position based on the key pressed
            switch (key)
            {
                case Key.Up:
                    if (margin.Top - moveAmount >= boundaries.Top)
                    {
                        newMargin.Top -= moveAmount;
                        newMargin.Bottom += moveAmount;
                    }
                    break;
                case Key.Down:
                    if (margin.Top + moveAmount <= boundaries.Bottom)
                    {
                        newMargin.Top += moveAmount;
                        newMargin.Bottom -= moveAmount;
                    }
                    break;
                case Key.Left:
                    if (margin.Left - moveAmount >= boundaries.Left)
                    {
                        newMargin.Left -= moveAmount;
                        newMargin.Right += moveAmount;
                    }
                    break;
                case Key.Right:
                    if (margin.Left + moveAmount <= boundaries.Right)
                    {
                        newMargin.Left += moveAmount;
                        newMargin.Right -= moveAmount;
                    }
                    break;
            }

            // Check if the new position intersects with any restricted zones
            Rect playerRect = new Rect(newMargin.Left, newMargin.Top, ((FrameworkElement)_player).ActualWidth, ((FrameworkElement)_player).ActualHeight);
            if (!IsIntersectingRestrictedZones(playerRect))
            {
                ((FrameworkElement)_player).Margin = newMargin;

                // Check if the player is in a map switch trigger
                foreach (var trigger in _mapSwitchTriggers)
                {
                    if (trigger.IsPlayerInTrigger(playerRect))
                    {
                        _switchMapCallback(trigger.TargetMapPath);
                        return;
                    }
                }
            }
        }

        private bool IsIntersectingRestrictedZones(Rect playerRect)
        {
            foreach (var zone in _restrictedZones)
            {
                if (zone.Intersects(playerRect))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
