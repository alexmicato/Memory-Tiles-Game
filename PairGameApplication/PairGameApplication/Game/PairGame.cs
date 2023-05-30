
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Threading;

namespace PairGameApplication
{
    [Serializable]
    public class PairGame : INotifyPropertyChanged
    {
        public List<Card> cards;
        public int width { get; set; }
        public int height { get; set; } 
        private Card firstCardClicked;
        private Card secondCardClicked;
        private int gameLevel;
        public int elapsedTime { get; set; }

        public PairGame()
        {

            //string defaultPath = "Assets/BoardImages/default";
            //string imagePath = "Assets/BoardImages/picture";
            //gameLevel = 1;
            //width = 4; height = 4;
            //AssignTimePerLevel();
            //cards = new List<Card>();

            //for (int i = 0; i < 8; i++)
            //{
            //    cards.Add(new Card(imagePath + i + ".png", defaultPath, false, false));
            //    cards.Add(new Card(imagePath + i + ".png", defaultPath, false, false));
            //}

            //Utility.ShuffleList(cards);
        }


        public PairGame(PairGame game)
        {
            this.gameLevel = game.gameLevel;
            this.elapsedTime = game.elapsedTime;
            this.cards = game.cards;
            this.Cards = game.Cards;
            this.width = game.width;
            this.height = game.height;  

        }

        public void SetGameSettings(int width, int height)
        {
            gameLevel = 1;
            AssignTimePerLevel();

            this.width = width;
            this.height = height;

            cards = null;
            cards = new List<Card>();

            string defaultPath = "Assets/BoardImages/default.png";
            string imagePath = "Assets/BoardImages/picture";

            for (int i = 0; i < (width*height)/2; i++)
            {
                cards.Add(new Card(imagePath + i + ".png", defaultPath, false, false));
                cards.Add(new Card(imagePath + i + ".png", defaultPath, false, false));
            }

            Utility.ShuffleList(cards);

            OnPropertyChanged(nameof(Cards));
        }
        public int Width
        {
            get { return width; }
            set
            {
                if (width != value)
                {
                    width = value;
                    OnPropertyChanged(nameof(Width));
                }
            }
        }

        public int Height
        {
            get { return height; }
            set
            {
                if (height != value)
                {
                    height = value;
                    OnPropertyChanged(nameof(Width));
                }
            }
        }

        public List<Card> Cards
        {
            get { return cards; }
            set
            {
                cards = value;
                OnPropertyChanged(nameof(Cards));
            }
        }

        public void ReshuffleCards()
        {
            string defaultPath = "Assets/BoardImages/default.png";
            string imagePath = "Assets/BoardImages/picture";
            cards = new List<Card>();

            for (int i = 0; i < (height*width)/2; i++)
            {
                cards.Add(new Card(imagePath + i + ".png", defaultPath, false, false));
                cards.Add(new Card(imagePath + i + ".png", defaultPath, false, false));
            }

            Utility.ShuffleList(cards);

            OnPropertyChanged(nameof(Cards));
        }

        public int GetGameLevel()
        {
            return gameLevel;
        }

        public void AssignGameLevel(int gameLevel) {

            this.gameLevel = gameLevel;
            AssignTimePerLevel();
        
        }

        public void AssignTimePerLevel()
        {
            if (gameLevel == 1)
                elapsedTime = 600;

            else if (gameLevel == 2)
                elapsedTime = 360;

            else if (gameLevel == 3)
                elapsedTime = 120;

        }

        public bool RanOutOfTime()
        {
            if (elapsedTime == 0)
                return true;

            return false;
        }

        public Card GetSecondCardClicked()
        {
            return secondCardClicked;
        }

        public bool IsWin()
        {
            for(int i=0; i<Cards.Count; i++) 
            {
                if (!Cards[i].IsMatched)
                    return false;
            }
            return true;
        }

        public bool IsMatch(Card card)
        {
            if (firstCardClicked == null)
            {
                firstCardClicked = card;
                firstCardClicked.IsFlipped = true;

                return false;
            }
            else
            {
                secondCardClicked = card;
                secondCardClicked.IsFlipped = true;

                if (firstCardClicked.Image == secondCardClicked.Image)
                {
      
                    firstCardClicked.IsMatched = true;
                    secondCardClicked.IsMatched = true;

                    firstCardClicked.IsFlipped = true;
                    secondCardClicked.IsFlipped = true;

                    for (int i = 0; i < cards.Count; i++)
                    {
                        if (cards[i].GetActualImage() == firstCardClicked.Image)
                        {
                            cards[i].IsMatched = true;
                            cards[i].IsFlipped = true;
                        }   
                    }

                    firstCardClicked = null;
                    secondCardClicked = null;

                    return true;
                }
                else
                {
                    DispatcherTimer timer = new DispatcherTimer();
                    timer.Interval = TimeSpan.FromSeconds(1);
                    timer.Tick += Timer_Tick;
                    timer.Start();

                    return false;
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {

            firstCardClicked.IsFlipped = false;
            secondCardClicked.IsFlipped = false;

            firstCardClicked = null;
            secondCardClicked = null;

            var timer = sender as DispatcherTimer;
            timer.Stop();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }

}


