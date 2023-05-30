using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using PairGameApplication.Data;

namespace PairGameApplication
{
    /// <summary>
    /// Interaction logic for SavedGamesPage.xaml
    /// </summary>
    public partial class SavedGamesPage : Page
    {
        public static User currentUser;
        public SavedGamesPage()
        {
            InitializeComponent();

            if (currentUser != null)
            {
                usernameLabel.Content = currentUser.username + "'s";
                BitmapImage imageBitmap = new BitmapImage(new Uri(currentUser.avatarImage, UriKind.Absolute));
                userImage.Source = imageBitmap;


                List<SavedGame> savedGames = new List<SavedGame>();

                savedGames = (List<SavedGame>)xmlSerializationUtility.Deserialize<List<SavedGame>>(SavedGame.savedGamesFilePath);

                savedGamesList.ItemsSource = null;

                savedGamesList.ItemsSource = savedGames.Where(s => s.user.username == currentUser.username).ToList();
            }
        }

        public void RefreshSavedGamesList(List<SavedGame> savedGames)
        {
            List<SavedGame> userSavedGames = new List<SavedGame>();

            foreach (var savedGame in savedGames.Where(s => s.user.username == currentUser.username))
            {
                userSavedGames.Add(savedGame);
            } 
            savedGamesList.ItemsSource = null;
            savedGamesList.ItemsSource = userSavedGames;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow.mainFrame.GoBack();
        }

        private void savedGamesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = savedGamesList.SelectedItem;

            if (selectedItem != null)
            {
                openButton.IsEnabled = true;
            }
            else
            {
                openButton.IsEnabled = false;
            }
        }

        private void openButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = savedGamesList.SelectedItem as SavedGame;

            if (selectedItem != null)
            {
                int x = selectedItem.game.cards.Count;
                Debug.WriteLine(selectedItem.game.cards.Count);
                MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
                GamePage.currentUser = currentUser;
                mainWindow.mainFrame.Navigate(new GamePage(selectedItem.game));
            }
        }

    }
}
