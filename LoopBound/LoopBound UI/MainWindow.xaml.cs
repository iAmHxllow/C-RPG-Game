using Engine.ViewModels;
using System.Text;
using System.Windows;
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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameSession _gameSession;
                                                       // X, Y, Width, Height of the restricted area
        private readonly Rect _restrictedZone1 = new Rect(0, 0, 310, 277); // restricted area
        private readonly Rect _restrictedZone2 = new Rect(454, 0, 316, 276); // restricted area
        private readonly Rect _restrictedZone3 = new Rect(310, 0, 144, 204); // restricted area
        private readonly Rect _restrictedZone4 = new Rect(0, 426, 770, 86); // restricted area



        public MainWindow()
        {
            InitializeComponent();

            InitializeComponent();
            _gameSession = new GameSession();
            DataContext = _gameSession;
        }

        /// Event handler for the Keyboard movement
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            // Get the current margin of the Player image
            var margin = Player.Margin;

            // Define boundaries
            const int leftBound = 0;
            const int topBound = 0;
            const int rightBound = 700; // Width of the map
            const int bottomBound = 410; // Height of the map

            // Movement amount
            const int moveAmount = 10;

            // Calculate the player's new position
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

            // Check if the new position intersects with any restricted zone
            Rect playerRect = new Rect(newMargin.Left, newMargin.Top, Player.ActualWidth, Player.ActualHeight);
            if (!IsIntersectingRestrictedZones(playerRect))
            {
                // Apply the updated margin back to the Player image
                Player.Margin = newMargin;
            }
        }

        private bool IsIntersectingRestrictedZones(Rect playerRect)
        {
            // Check if the player's rectangle intersects with any restricted zone
            return playerRect.IntersectsWith(_restrictedZone1) || playerRect.IntersectsWith(_restrictedZone2) || playerRect.IntersectsWith(_restrictedZone3) || playerRect.IntersectsWith(_restrictedZone4);
        }
    }
}