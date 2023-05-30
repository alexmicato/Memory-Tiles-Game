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
using System.Windows.Shapes;

namespace PairGameApplication
{
    /// <summary>
    /// Interaction logic for NewUserWindow.xaml
    /// </summary>
    public partial class NewUserWindow : Window
    {
        private List<String> avatarImages;
        private int currentAvatarIndex = 0;
        private readonly LoginPage parentPage;
        public NewUserWindow(LoginPage parentPage)
        {
            InitializeComponent();

            this.parentPage = parentPage;

            avatarImages = new List<String>();

            string defaultPath = "Assets/AvatarImages/avatar";

            for(int i = 0; i < 5; i++)
                avatarImages.Add(defaultPath + i + ".png");

            string imagePath = avatarImages[currentAvatarIndex];
            BitmapImage imageBitmap = new BitmapImage(new Uri(imagePath, UriKind.Relative));
            avatarImageControl.Source = imageBitmap;
        }

        private void ImageLeftButton_Click(object sender, RoutedEventArgs e)
        {

            currentAvatarIndex--;

            if (currentAvatarIndex < 0)
                currentAvatarIndex = avatarImages.Count - 1;
            string imagePath = avatarImages[currentAvatarIndex];
            BitmapImage imageBitmap = new BitmapImage(new Uri(imagePath, UriKind.Relative));
            avatarImageControl.Source = imageBitmap;

        }

        private void ImageRightButton_Click(object sender, RoutedEventArgs e)
        {
            currentAvatarIndex++;

            if (currentAvatarIndex == avatarImages.Count)
                currentAvatarIndex = 0;
            string imagePath = avatarImages[currentAvatarIndex];
            BitmapImage imageBitmap = new BitmapImage(new Uri(imagePath, UriKind.Relative));
            avatarImageControl.Source = imageBitmap;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string username = nameTextBox.Text;
            string avatar = avatarImageControl.Source.ToString();
            User newUser = new User(username, avatar);

            List<User> users = new List<User>();

            users = (List<User>)xmlSerializationUtility.Deserialize<List<User>>(User.usersFilePath);

            users.Add(newUser);

            xmlSerializationUtility.Serialize(users, User.usersFilePath);

            nameTextBox.Text = "";

            parentPage.RefreshUsersList(users);

            MessageBox.Show("Succesfully added user", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
