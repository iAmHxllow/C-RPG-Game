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
            // Current player margin
            var margin = Player.Margin;

            // Adjust the margin based on the key pressed
            const int moveAmount = 10; // Number of pixels to move
            switch (e.Key)
            {
                case Key.Up:
                    margin.Top -= moveAmount;
                    margin.Bottom += moveAmount;
                    break;
                case Key.Down:
                    margin.Top += moveAmount;
                    margin.Bottom -= moveAmount;
                    break;
                case Key.Left:
                    margin.Left -= moveAmount;
                    margin.Right += moveAmount;
                    break;
                case Key.Right:
                    margin.Left += moveAmount;
                    margin.Right -= moveAmount;
                    break;
            }

            Player.Margin = margin;
        }
    }
}