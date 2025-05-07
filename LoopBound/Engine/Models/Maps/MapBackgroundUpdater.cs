using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Engine.Models.Maps;
using WpfAnimatedGif;

namespace Engine.Models
{
    public class MapBackgroundUpdater
    {
        private Image _backgroundMap; // The image control for the background map
        private Image _player; // The image control for the player
        private UIElement _savePoint; // The UI element for the save point
        private Dictionary<string, Map> _backgroundMaps; // Dictionary to hold all background maps
        private bool _canSwitchMap = true; // Flag to control map switching

        // Constructor 
        public MapBackgroundUpdater(Image backgroundMap, Image player, UIElement savePoint, Dictionary<string, Map> backgroundMaps)
        {
            _backgroundMap = backgroundMap;
            _player = player;
            _savePoint = savePoint;
            _backgroundMaps = backgroundMaps;
        }

        // Method to switch to a new map
        public async Task SwitchToMap(string newMapPath, Action<List<RestrictedZone>, List<MapSwitchTrigger>, Thickness> updateMapData)
        {
            if (!_canSwitchMap) return;

            _canSwitchMap = false;

            // Check if the new map path is valid
            if (!_backgroundMaps.TryGetValue(newMapPath, out var newMap)) return;

            // Clean up previous map source
            ImageBehavior.SetAnimatedSource(_backgroundMap, null);
            _backgroundMap.Source = null;

            // if the new map is a GIF, use WpfAnimatedGif to set the source
            if (newMapPath.EndsWith(".gif"))
            {
                var gifImage = new BitmapImage(new Uri($"pack://application:,,,/{newMapPath}"));
                ImageBehavior.SetAnimatedSource(_backgroundMap, gifImage);
            }
            // if the new map is a static image, set the source directly
            else
            {
                var image = new BitmapImage(new Uri(newMapPath, UriKind.Relative));
                _backgroundMap.Source = image;
            }

            // Adjust stretching for specific maps
            _backgroundMap.Stretch = newMapPath.EndsWith("VendorRoute.png") ? Stretch.Uniform : Stretch.Fill;

            // Managing save point visibility
            _savePoint.Visibility = newMapPath.EndsWith("Room.png") ? Visibility.Visible : Visibility.Hidden;

            // Managing player visibility for specific maps
            _player.Visibility = (newMapPath.EndsWith("VendorShop.gif") || newMapPath.EndsWith("BossRoom.gif"))
                ? Visibility.Hidden
                : Visibility.Visible;

            // Update zones, triggers, and player position
            updateMapData(newMap.RestrictedZones, newMap.MapSwitchTriggers, newMap.DefaultPlayerPosition);

            // Delay to allow for map transition
            await Task.Delay(500);
            _canSwitchMap = true;
        }
    }
}
