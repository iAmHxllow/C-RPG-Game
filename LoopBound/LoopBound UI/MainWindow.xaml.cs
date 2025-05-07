using System.Windows;
using Engine.ViewModels;
using Engine.Models;
using Engine.Models.Maps;
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

        // Game session class
        private GameSession _gameSession;

        // Map background updater class
        private MapBackgroundUpdater _mapBackgroundUpdater;

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

            var homeMap = new HomeMap();
            var roomMap = new RoomMap();
            var questMap1 = new QuestMap1();
            var questMap2 = new QuestMap2();
            var questMap3 = new QuestMap3();
            var vendorRoute = new VendorRoute();
            var vendorShop = new VendorShop();
            var bossRoom = new BossRoom();

            _backgroundMaps.Add(homeMap.MapPath, homeMap);
            _backgroundMaps.Add(roomMap.MapPath, roomMap);
            _backgroundMaps.Add(questMap1.MapPath, questMap1);
            _backgroundMaps.Add(questMap2.MapPath, questMap2);
            _backgroundMaps.Add(questMap3.MapPath, questMap3);
            _backgroundMaps.Add(vendorRoute.MapPath, vendorRoute);
            _backgroundMaps.Add(vendorShop.MapPath, vendorShop);
            _backgroundMaps.Add(bossRoom.MapPath, bossRoom);

            // Initialize the MapSwitcher
            _mapBackgroundUpdater = new MapBackgroundUpdater(Background_Map, Player, SavePoint, _backgroundMaps);

            // Set the initial map
            _mapBackgroundUpdater.SwitchToMap("Assets/Room.png", UpdateMapData);

            //<-- Assets for each map -->

            // Save Point Animated GIF
            var gifImage = new BitmapImage(new Uri("pack://application:,,,/Assets/SavePoint.gif"));
            WpfAnimatedGif.ImageBehavior.SetAnimatedSource(SavePoint, gifImage);

            //<-- Assets for each map -->
        }

        // Method to update data each time the player switches maps
        private void UpdateMapData(List<RestrictedZone> restrictedZones, List<MapSwitchTrigger> mapSwitchTriggers, Thickness playerPosition)
        {
            _restrictedZones = restrictedZones;
            _mapSwitchTriggers = mapSwitchTriggers;
            Player.Margin = playerPosition;
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

            // Creating a rectangle around the player's position
            Rect playerRect = new Rect(newMargin.Left, newMargin.Top, Player.ActualWidth, Player.ActualHeight);

            // Check if the rectangle intersects with any restricted zones
            if (!IsIntersectingRestrictedZones(playerRect))
            {
                Player.Margin = newMargin;

                // Check if recantle interacts with map switch triggers
                foreach (var trigger in _mapSwitchTriggers)
                {
                    if (trigger.IsPlayerInTrigger(playerRect))
                    {
                        _mapBackgroundUpdater.SwitchToMap(trigger.TargetMapPath, UpdateMapData);
                        return; 
                    }
                }
            }
        }

        // Method to check if the player 's rectangle intersects with any restricted zones (AKA Boundaries / Walls)
        public bool IsIntersectingRestrictedZones(Rect playerRect)
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