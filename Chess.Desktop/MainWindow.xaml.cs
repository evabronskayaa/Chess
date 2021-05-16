using Chess.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chess.Desktop
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Drawer.MainWindow = this;
        }

        private void btnStartGame(object sender, RoutedEventArgs e)
        {
            Board.FillBoard();
            Drawer.CreatePreviousGameWindow();
        }

        private void btnContinue(object sender, RoutedEventArgs e)
        {
            Board.DataWorker.LoadBoard();
            Drawer.CreateGameWindow();
        }

        private void btnSettings(object sender, RoutedEventArgs e)
        {
            Drawer.CreateSettingsWindow();
        }

        private void btnRating(object sender, RoutedEventArgs e)
        {
            Drawer.CreateRatingWindow();
        }

        private void btnExit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
