using System.Windows;
using Engine.ViewModels;
using Engine.Models;
using WpfAnimatedGif;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LoopBound_UI
{
    public partial class MainWindow : Window
    {

        // Game session instance class
        private GameSession _gameSession;

        // Map switch trigger class
        public MapSwitchTrigger _mapSwitchTrigger;

        // List to store all restricted zones
        public List<RestrictedZone> _restrictedZones = new List<RestrictedZone>();

        // List to store all map switch triggers
        public List<MapSwitchTrigger> _mapSwitchTriggers = new List<MapSwitchTrigger>();
        private bool _canSwitchMap = true; // Flag to control map switching

        // Dictionary to store background maps
        private Dictionary<string, Map> _backgroundMaps = new Dictionary<string, Map>();
        private Map _currentMap;

        public MainWindow()
        {
            InitializeComponent();

            _gameSession = new GameSession();
            DataContext = _gameSession;

            // Define maps
            var homeMap = new Map("Assets/Home.png", new Thickness(18,312,664,101));
            homeMap.AddRestrictedZone(0, 0, 310, 277);
            homeMap.AddRestrictedZone(454, 0, 316, 276);
            homeMap.AddRestrictedZone(310, 0, 144, 204);
            homeMap.AddRestrictedZone(0, 426, 770, 86);
            homeMap.AddMapSwitchTrigger(344, 171, 99, 122, "Assets/Room.png");
            homeMap.AddMapSwitchTrigger(749, 286, 21, 135, "Assets/QuestMap1.gif");

            var roomMap = new Map("Assets/Room.png", new Thickness(356, 303, 326, 110));
            roomMap.AddRestrictedZone(10, 417, 336, 92);
            roomMap.AddRestrictedZone(463, 417, 307, 92);
            roomMap.AddRestrictedZone(595, 10, 175, 401);
            roomMap.AddRestrictedZone(10, 10, 175, 401);
            roomMap.AddRestrictedZone(203, 10, 390, 171);
            roomMap.AddMapSwitchTrigger(347, 412, 113, 75, "Assets/Home.png");

            var questMap = new Map("Assets/QuestMap1.gif", new Thickness(40, 269, 686, 175));
            questMap.AddRestrictedZone(10, 10, 25, 190);
            questMap.AddRestrictedZone(77, 10, 67, 216);
            questMap.AddRestrictedZone(150, 10, 620, 75);
            questMap.AddRestrictedZone(250, 90, 520, 167);
            questMap.AddRestrictedZone(10, 393, 135, 111);
            questMap.AddRestrictedZone(150, 426, 434, 78);
            questMap.AddRestrictedZone(589,393, 37, 111);
            questMap.AddRestrictedZone(542, 262, 228, 65);
            questMap.AddRestrictedZone(631, 487, 94, 17);
            questMap.AddRestrictedZone(365, 272, 44, 51);
            questMap.AddMapSwitchTrigger(10, 269, 7, 72, "Assets/Home.png");
            questMap.AddMapSwitchTrigger(728, 425, 22,54, "Assets/QuestMap2.png");
            questMap.AddMapSwitchTrigger(40, 174, 32, 58, "Assets/VendorRoute.png");

            var questMap2 = new Map("Assets/QuestMap2.png", new Thickness(30, 47, 700, 381));
            questMap2.AddRestrictedZone(96, 10, 244, 111);
            questMap2.AddRestrictedZone(345, 48, 90, 166);
            questMap2.AddRestrictedZone(440, 10, 330, 121);
            questMap2.AddRestrictedZone(10, 160, 36, 344);
            questMap2.AddRestrictedZone(141, 240, 80, 166);
            questMap2.AddRestrictedZone(267, 203, 20, 108);
            questMap2.AddRestrictedZone(345, 322, 20, 108);
            questMap2.AddRestrictedZone(477, 306, 90, 166);
            questMap2.AddRestrictedZone(655, 203, 69, 175);
            questMap2.AddRestrictedZone(51, 456, 413, 48);
            questMap2.AddRestrictedZone(469, 475, 117, 29);
            questMap2.AddRestrictedZone(661, 456, 109, 48);
            questMap2.AddMapSwitchTrigger(35, 10, 45, 24, "Assets/QuestMap1.gif");
            questMap2.AddMapSwitchTrigger(599, 447, 46, 48, "Assets/QuestMap3.png");

            var questMap3 = new Map("Assets/QuestMap3.png", new Thickness(254, 70, 470, 371));
            questMap3.AddRestrictedZone(10, 10, 225, 427);
            questMap3.AddRestrictedZone(334, 13, 436, 144);
            questMap3.AddRestrictedZone(236, 224, 260, 213);
            questMap3.AddRestrictedZone(592, 233, 178, 50);
            questMap3.AddRestrictedZone(11, 442, 67, 62);
            questMap3.AddRestrictedZone(717, 322, 9, 16); // Flower
            questMap3.AddRestrictedZone(717, 452, 9, 16); // Flower
            questMap3.AddMapSwitchTrigger(245, 13, 75, 34, "Assets/QuestMap2.png");

            var VendorRoute = new Map("Assets/VendorRoute.png", new Thickness(363, 334, 363, 97));
            VendorRoute.AddRestrictedZone(10, 0, 276, 494);
            VendorRoute.AddRestrictedZone(300, 80, 26, 86);
            VendorRoute.AddRestrictedZone(452, 80, 26, 86);
            VendorRoute.AddRestrictedZone(494, 0, 276, 494);
            VendorRoute.AddRestrictedZone(291, 327, 37, 170);
            VendorRoute.AddRestrictedZone(446, 327, 43, 170);
            VendorRoute.AddMapSwitchTrigger(346, 437, 84, 44, "Assets/QuestMap1.gif");
            VendorRoute.AddMapSwitchTrigger(347, 80, 84, 86, "Assets/Room.png"); // Change to Vendor


            // Add maps to the dictionary
            _backgroundMaps.Add(homeMap.MapPath, homeMap);
            _backgroundMaps.Add(roomMap.MapPath, roomMap);
            _backgroundMaps.Add(questMap.MapPath, questMap);
            _backgroundMaps.Add(questMap2.MapPath, questMap2);
            _backgroundMaps.Add(questMap3.MapPath, questMap3);
            _backgroundMaps.Add(VendorRoute.MapPath, VendorRoute);


            // Set the initial map
            SwitchBackgroundMap("Assets/Home.png");
        }

        // Event handler for player keyboard movement
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            var margin = Player.Margin;

            // Define boundaries
            const int leftBound = 0;
            const int topBound = 0;
            const int rightBound = 700;
            const int bottomBound = 410;

            const int moveAmount = 10;

            Thickness newMargin = margin;
            switch (e.Key)
            {
                case Key.Up:
                    if (margin.Top - moveAmount >= topBound)
                    {
                        newMargin.Top -= moveAmount;
                        newMargin.Bottom += moveAmount;
                    }
                    break;
                case Key.Down:
                    if (margin.Top + moveAmount <= bottomBound)
                    {
                        newMargin.Top += moveAmount;
                        newMargin.Bottom -= moveAmount;
                    }
                    break;
                case Key.Left:
                    if (margin.Left - moveAmount >= leftBound)
                    {
                        newMargin.Left -= moveAmount;
                        newMargin.Right += moveAmount;
                    }
                    break;
                case Key.Right:
                    if (margin.Left + moveAmount <= rightBound)
                    {
                        newMargin.Left += moveAmount;
                        newMargin.Right -= moveAmount;
                    }
                    break;
            }

            Rect playerRect = new Rect(newMargin.Left, newMargin.Top, Player.ActualWidth, Player.ActualHeight);

            if (!IsIntersectingRestrictedZones(playerRect))
            {
                Player.Margin = newMargin;

                // Check all map switch triggers
                foreach (var trigger in _mapSwitchTriggers)
                {
                    if (trigger.IsPlayerInTrigger(playerRect))
                    {
                        SwitchBackgroundMap(trigger.TargetMapPath);
                        return; // Exit after switching maps
                    }
                }
            }
        }


        // Method to update the background map
        private async void SwitchBackgroundMap(string newMapPath)
        {
            if (!_canSwitchMap) return;

            _canSwitchMap = false;

            if (!_backgroundMaps.TryGetValue(newMapPath, out var newMap)) return;

            _currentMap = newMap;

            // Clean up previous map source
            ImageBehavior.SetAnimatedSource(Background_Map, null);
            Background_Map.Source = null;

            // Load new image
            if (newMapPath.EndsWith(".gif"))
            {
                var gifImage = new BitmapImage(new Uri($"pack://application:,,,/{newMapPath}"));
                ImageBehavior.SetAnimatedSource(Background_Map, gifImage);
            }
            else
            {
                var image = new BitmapImage(new Uri(newMapPath, UriKind.Relative));
                Background_Map.Source = image;
            }

            // Temporary fix for stretching on the VendorRoute map.
            if (newMapPath.EndsWith("VendorRoute.png"))
            {
                Background_Map.Stretch = Stretch.Uniform;
            }
            else
            {
                Background_Map.Stretch = Stretch.Fill;
            }

            // Update zones and triggers
            _restrictedZones = newMap.RestrictedZones;
            _mapSwitchTriggers = newMap.MapSwitchTriggers;

            // Reposition player
            Player.Margin = newMap.DefaultPlayerPosition;

            // Delay to allow for map transition
            await Task.Delay(500);
            _canSwitchMap = true;
        }


        public bool IsIntersectingRestrictedZones(Rect playerRect)
        {
            // Check if the player's rectangle intersects with any restricted zone
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