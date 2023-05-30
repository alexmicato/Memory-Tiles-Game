using Microsoft.VisualBasic;
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
using System.Xml.Serialization;
using PairGameApplication.Data;
using System.IO;

namespace PairGameApplication
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();

            List<User> users = new List<User>();

            users = (List<User>)xmlSerializationUtility.Deserialize<List<User>>(User.usersFilePath);

            userListBox.ItemsSource = users;

        }

        private void PlayGame_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = userListBox.SelectedItem as User;

            if (selectedItem != null)
            {
                MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
                MenuPage.currentUser = selectedItem;
                mainWindow.mainFrame.Navigate(new MenuPage());
            }
        }

        private void NewUserButton_Click(object sender, RoutedEventArgs e)
        {
            var newUserWindow = new NewUserWindow(this);

            var result = newUserWindow.ShowDialog();

        }

         public void RefreshUsersList(List<User> userList)
        {
            userListBox.ItemsSource = null;
            userListBox.ItemsSource = userList;
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = userListBox.SelectedItem as User;

            if(selectedItem != null)
            {
                List<User> users = new List<User>();

                users = (List<User>)xmlSerializationUtility.Deserialize<List<User>>(User.usersFilePath);

                users.RemoveAll(u => u.username == selectedItem.username && u.avatarImage == selectedItem.avatarImage);

                xmlSerializationUtility.Serialize(users, User.usersFilePath);

                MessageBox.Show("User successfully deleted", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                RefreshUsersList(users);

            }

        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Close();
        }

        private void userListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = userListBox.SelectedItem as User;

            if (selectedItem != null)
            {
                
                var imagePath = selectedItem.avatarImage; 
                var imageSource = new BitmapImage(new Uri(imagePath));
                userImage.Source = imageSource;

                playButton.IsEnabled = true;
                deleteUserButton.IsEnabled = true;
            }
            else
            {
                playButton.IsEnabled = false;
                deleteUserButton.IsEnabled = false;
            }
        }
    }
}
