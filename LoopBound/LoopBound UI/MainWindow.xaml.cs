using System.Windows;
using Engine.ViewModels;
using Engine.Models;
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

        // List to store all restricted zones
        public List<RestrictedZone> _restrictedZones = new List<RestrictedZone>();

        // Map switch trigger class
        public MapSwitchTrigger _mapSwitchTrigger;

        public MainWindow()
        {
            InitializeComponent();

            // Initializing the game session and setting the DataContext
            _gameSession = new GameSession();
            DataContext = _gameSession;

            // Initializing the restricted zones and map switch trigger for the default map
            InitializeRestrictedZones("Assets/Home.png");
        }

        // Event handler for player keyboard movement
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            var margin = Player.Margin;

            // Defining gameplay area boundaries
            const int leftBound = 0;
            const int topBound = 0;
            const int rightBound = 700;
            const int bottomBound = 410;

            // Amount to move the player
            const int moveAmount = 10;

            // Checking if the player is within the gameplay area
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

            // Checking if the new position intersects with any restricted zones
            Rect playerRect = new Rect(newMargin.Left, newMargin.Top, Player.ActualWidth, Player.ActualHeight);

            // If the new position does NOT intersect with any restricted zones, update the player's position
            if (!IsIntersectingRestrictedZones(playerRect))
            {
                Player.Margin = newMargin;

                // Checking if the player is in the map switch trigger area
                if (_mapSwitchTrigger.IsPlayerInTrigger(playerRect))
                {
                    SwitchBackgroundMap(_mapSwitchTrigger.TargetMapPath);
                }
            }
        }

        private void SwitchBackgroundMap(string newMapPath)
        {
            // Updating the background map
            Background_Map.Source = new BitmapImage(new Uri(newMapPath, UriKind.Relative));

            // Reinitializing the restricted zones + map switch trigger for the new map
            InitializeRestrictedZones(newMapPath);
        }

        public void InitializeRestrictedZones(string mapPath)
        {
            _restrictedZones.Clear();

            switch (mapPath)
            {
                // Defining restricted zones and map switch trigger for each map
                case "Assets/Home.png":
                    _restrictedZones.Add(new RestrictedZone(0, 0, 310, 277));
                    _restrictedZones.Add(new RestrictedZone(454, 0, 316, 276));
                    _restrictedZones.Add(new RestrictedZone(310, 0, 144, 204));
                    _restrictedZones.Add(new RestrictedZone(0, 426, 770, 86));
                    _mapSwitchTrigger = new MapSwitchTrigger(344, 171, 99, 122, "Assets/Room.png");
                    break;

                case "Assets/Room.png":
                    _restrictedZones.Add(new RestrictedZone(10, 417, 336, 92));
                    _restrictedZones.Add(new RestrictedZone(463, 417, 307, 92));
                    _restrictedZones.Add(new RestrictedZone(595, 10, 175, 401));
                    _restrictedZones.Add(new RestrictedZone(10, 10, 175, 401));
                    _restrictedZones.Add(new RestrictedZone(203, 10, 390, 171));
                    _mapSwitchTrigger = new MapSwitchTrigger(347, 412, 113, 75, "Assets/Home.png");
                    break;

                default:
                    // Default restricted zones if no specific map is matched
                    _restrictedZones.Add(new RestrictedZone(0, 0, 0, 0));
                    _mapSwitchTrigger = new MapSwitchTrigger(0, 0, 0, 0, string.Empty);
                    break;
            }
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