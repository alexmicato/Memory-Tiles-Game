using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;
using PairGameApplication.Data;

namespace PairGameApplication
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page, INotifyPropertyChanged
    {
        private PairGame pairGame;
        private int currentLevel = 1;
        bool start = false;
        private DispatcherTimer timer;
        public static Tuple<int, int> gameSize;
        public Tuple<int, int> standardSize = new Tuple<int, int>(4, 4);
        public static User currentUser { get; set; }
        public GamePage()
        {
            InitializeComponent();
            pairGame = null;
            pairGame = new PairGame();
            if(gameSize != null)
                pairGame.SetGameSettings(gameSize.Item1, gameSize.Item2);
            DataContext = pairGame;
            currentUser.UpdateGamesPlayed();

            if (currentUser != null)
            {
                usernameLabel.Content = "User: " + currentUser.username;
                BitmapImage imageBitmap = new BitmapImage(new Uri(currentUser.avatarImage, UriKind.Absolute));
                userImage.Source = imageBitmap;
            }

            levelLabel.Content = currentLevel;
            elapsedTimeLabel.Content = pairGame.elapsedTime.ToString();
        }

        public GamePage(PairGame pairGame)
        {
            InitializeComponent();
            this.pairGame = pairGame;
            DataContext = pairGame;

            if (currentUser != null)
            {
                usernameLabel.Content = "User: " + currentUser.username;
                BitmapImage imageBitmap = new BitmapImage(new Uri(currentUser.avatarImage, UriKind.Absolute));
                userImage.Source = imageBitmap;
            }

            gameSize = new Tuple<int, int>(pairGame.width, pairGame.height);
            gameBoard.ItemsSource = pairGame.Cards;
            levelLabel.Content = currentLevel;
            elapsedTimeLabel.Content = pairGame.elapsedTime.ToString();
        }
        public void Card_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var card = (Card)button.DataContext;
            if (card.IsMatched || card.IsFlipped || pairGame.GetSecondCardClicked() != null || !start)
            {
                return;
            }

            if (pairGame.IsMatch(card))
            {
                if (pairGame.IsWin())
                {
                    if(currentLevel != 3)
                    { 
                    timer.Stop();
                    MessageBox.Show("Congratulations! You advanced to a new level");
                    currentLevel++;

                    startButton.Content = "Start";

                    levelLabel.Content = currentLevel;
                    
                    pairGame.ReshuffleCards();
                    pairGame.AssignGameLevel(currentLevel);
                 
                    gameBoard.ItemsSource = pairGame.Cards;
                    elapsedTimeLabel.Content = pairGame.elapsedTime.ToString();

                    start = false;
                    }
                    else
                    {
                        timer.Stop();
                        MessageBoxResult result = MessageBox.Show("Congratulations! You have won! Want to try a new game?", "Game Over", MessageBoxButton.YesNo, MessageBoxImage.Information);
                        currentUser.UpdateGamesWon();

                        if(result == MessageBoxResult.Yes)
                        {
                            StartNewGame();
                        }
                        else
                        {
                            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
                            mainWindow.mainFrame.GoBack();
                        }
                    }

                }
            }

        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if(startButton.Content.ToString() == "Start")
            {

                start = true;
                StartTimer();
                startButton.Content = "Stop";

            }
            else
            {
                start = false;
                startButton.Content = "Start";
                if(timer != null)
                    timer.Stop();
            }
        }

        public void StartNewGame()
        {
            currentLevel = 1;
            levelLabel.Content = currentLevel;
            startButton.Content = "Start";

            pairGame = new PairGame();
            if (gameSize != null)
                pairGame.SetGameSettings(gameSize.Item1, gameSize.Item2);
            pairGame.AssignGameLevel(currentLevel);
            gameBoard.ItemsSource = pairGame.Cards;
            elapsedTimeLabel.Content = pairGame.elapsedTime.ToString();

            currentUser.UpdateGamesPlayed();

            start = false;
        }
        private void StartTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            pairGame.elapsedTime--;
            elapsedTimeLabel.Content = pairGame.elapsedTime.ToString();

            if(pairGame.RanOutOfTime())
            {
                timer.Stop();
                MessageBoxResult result = MessageBox.Show("You have run out of time! Start again?", "Warning", MessageBoxButton.YesNo);

                if(result == MessageBoxResult.Yes)
                {
                    StartNewGame();
                }
                else 
                {
                    MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
                    mainWindow.mainFrame.GoBack();
                }
                
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow.mainFrame.GoBack();
        }

        private void newGameButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow.mainFrame.GoBack();
        }

        private void help_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("\tStudent: Micato Alexandru\n\tGrupa: 10LF312\n\tSpecializare: Informatica Aplicata", "About", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void saveGame_Click(object sender, RoutedEventArgs e)
        {
            if (timer != null)
                timer.Stop();

            startButton.Content = "Start";

            SavedGame savedGame = new SavedGame(currentUser, pairGame);

            List<SavedGame> savedGames = new List<SavedGame>();

            savedGames = (List<SavedGame>)xmlSerializationUtility.Deserialize<List<SavedGame>>(SavedGame.savedGamesFilePath);

            savedGames.Add(savedGame);

            xmlSerializationUtility.Serialize(savedGames, SavedGame.savedGamesFilePath);

            MessageBox.Show("Succesfully saved game", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void openGame_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            SavedGamesPage.currentUser = currentUser;
            mainWindow.mainFrame.Navigate(new SavedGamesPage());
        }
    }
}
