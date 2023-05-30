using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PairGameApplication
{
    [Serializable]
    public class Card : INotifyPropertyChanged
    {
        private string image;
        private string defaultImage;
        private bool isFlipped;
        private bool isMatched;

        public Card() { }

        public Card(string image, string defaultImage, bool isFlipped, bool isMatched)
        {
            this.image = image;
            this.defaultImage = defaultImage;
            this.isFlipped = isFlipped;
            this.isMatched = isMatched;
        }

        public string GetActualImage()
        {
            return image;
        }

        public string Image
        {
            get { return isFlipped || isMatched ? image : defaultImage; }
            set
            {
                if (image != value)
                {
                    image = value;
                    OnPropertyChanged("Image");
                }
            }
        }

        public string DefaultImage
        {
            get { return defaultImage; }
            set { defaultImage = value; }
        }

        public bool IsFlipped
        {
            get { return isFlipped; }
            set
            {
                if (isFlipped != value)
                {
                    isFlipped = value;
                    OnPropertyChanged("Image");
                }
            }
        }
        public bool IsMatched
        {
            get { return isMatched; }
            set { 
                isMatched = value;
                if (isMatched)
                {
                    IsFlipped = true; 
                }
                OnPropertyChanged("IsMatched");

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
