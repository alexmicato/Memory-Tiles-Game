using PairGameApplication.Data;
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

namespace PairGameApplication
{
    /// <summary>
    /// Interaction logic for MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        public static User currentUser { get; set; }
        public Tuple<int, int> customValues { get; set; }
        public MenuPage()
        {
            InitializeComponent();

            if (currentUser != null)
            {
                usernameLabel.Content = "User: " + currentUser.username;
                BitmapImage imageBitmap = new BitmapImage(new Uri(currentUser.avatarImage, UriKind.Absolute));
                userImage.Source = imageBitmap;
            }
        }

        private void newGameButton_Click(object sender, RoutedEventArgs e)
        {
            if(options.SelectedItem == null)
            {
                MessageBox.Show("You have not selected the game type!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
           
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            GamePage.currentUser = currentUser;
            GamePage.gameSize = customValues;
            mainWindow.mainFrame.Navigate(new GamePage());
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {

            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow.mainFrame.GoBack();
        }

        private void statistic_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            StatisticPage.currentUser = currentUser;
            mainWindow.mainFrame.Navigate(new StatisticPage());
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(options.SelectedIndex == 0)
            {
                dimensionsLabel.Content = "4 X 4";
                customValues = new Tuple<int, int> ( 4, 4 );
            }
            else if(options.SelectedIndex == 1)
            {
                var dialog = new DialogWindow();
                if (dialog.ShowDialog() == true)
                {
                    customValues = dialog.CustomValues;

                    if ((customValues.Item1 * customValues.Item2) % 2 == 0)
                        dimensionsLabel.Content = customValues.Item1.ToString() + " X " + customValues.Item2.ToString();
                    else
                    {
                        MessageBox.Show("Please select n and m so that n x m is even!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        options.SelectedItem = null;
                    }
                }
            }
        }

        private void help_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("\tStudent: Micato Alexandru\n\tGrupa: 10LF312\n\tSpecializare: Informatica Aplicata", "About", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void OpenGame_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            SavedGamesPage.currentUser = currentUser;
            mainWindow.mainFrame.Navigate(new SavedGamesPage());
        }
    }
}
