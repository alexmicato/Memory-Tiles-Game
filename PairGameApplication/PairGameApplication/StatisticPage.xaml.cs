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
    /// Interaction logic for StatisticPage.xaml
    /// </summary>
    public partial class StatisticPage : Page
    {
        public static User currentUser { get; set; }
        public StatisticPage()
        {
            InitializeComponent();

            if (currentUser != null)
            {
                usernameLabel.Content = currentUser.username + "'s";
                BitmapImage imageBitmap = new BitmapImage(new Uri(currentUser.avatarImage, UriKind.Absolute));
                userImage.Source = imageBitmap;

                gamesPlayedLabel.Content = currentUser.gamesPlayed;
                gamesWonLabel.Content = currentUser.gamesWon;
            }
        }

  
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow.mainFrame.GoBack();
        }
    }
}
