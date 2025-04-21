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
            // Current margin of the Player image
            var margin = Player.Margin;

            // Define boundaries
            const int leftBound = 0;
            const int topBound = 0;
            const int rightBound = 700; // Width of the map
            const int bottomBound = 410; // Height of the map

            // Movement amount
            const int moveAmount = 10;

            // Adjust the margin based on the key pressed
            switch (e.Key)
            {
                case Key.Up:
                    if (margin.Top - moveAmount >= topBound)
                    {
                        margin.Top -= moveAmount;
                        margin.Bottom += moveAmount;
                    }
                    break;
                case Key.Down:
                    if (margin.Top + moveAmount <= bottomBound)
                    {
                        margin.Top += moveAmount;
                        margin.Bottom -= moveAmount;
                    }
                    break;
                case Key.Left:
                    if (margin.Left - moveAmount >= leftBound)
                    {
                        margin.Left -= moveAmount;
                        margin.Right += moveAmount;
                    }
                    break;
                case Key.Right:
                    if (margin.Left + moveAmount <= rightBound)
                    {
                        margin.Left += moveAmount;
                        margin.Right -= moveAmount;
                    }
                    break;
            }

            // Apply the updated margin back to the Player image
            Player.Margin = margin;
        }
    }
}