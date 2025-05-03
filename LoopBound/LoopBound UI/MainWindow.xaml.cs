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
            questMap.AddRestrictedZone(10, 10, 135, 212);
            questMap.AddRestrictedZone(150, 10, 620, 75);
            questMap.AddRestrictedZone(250, 90, 520, 167);
            questMap.AddRestrictedZone(10, 393, 135, 111);
            questMap.AddRestrictedZone(150, 426, 434, 78);
            questMap.AddRestrictedZone(589,393, 37, 111);
            questMap.AddRestrictedZone(542, 262, 228, 75);
            questMap.AddRestrictedZone(726, 342, 44, 78);
            questMap.AddRestrictedZone(631, 487, 94, 17);
            questMap.AddMapSwitchTrigger(10, 269, 22, 73, "Assets/Home.png");

            // Add maps to the dictionary
            _backgroundMaps.Add(homeMap.MapPath, homeMap);
            _backgroundMaps.Add(roomMap.MapPath, roomMap);
            _backgroundMaps.Add(questMap.MapPath, questMap);

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