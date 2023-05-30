using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PairGameApplication.Data
{
    [Serializable]
    public class User
    {
        public string username { get; set; }
        public string avatarImage { get; set; }

        public static string usersFilePath = "../../../Database/users.xml";
        public int gamesPlayed { get; set; }
        public int gamesWon { get; set; }

        public User()
        {

        }

        public User(string username, string avatarImage) {
            this.username = username;
            this.avatarImage = avatarImage;
            gamesPlayed = 0;
            gamesWon = 0;
        }

        public void UpdateGamesPlayed()
        {
            gamesPlayed++;

            XDocument doc = XDocument.Load(usersFilePath);
            XElement userElement = doc.Descendants("User")
                                     .FirstOrDefault(e => (string)e.Element("username") == username);
            if (userElement != null)
            {
                userElement.Element("gamesPlayed").Value = gamesPlayed.ToString();
                doc.Save(usersFilePath);
            }
        }

        public void UpdateGamesWon()
        {
            gamesWon++;

            XDocument doc = XDocument.Load(usersFilePath);
            XElement userElement = doc.Descendants("User")
                                     .FirstOrDefault(e => (string)e.Element("username") == username);
            if (userElement != null)
            {
                userElement.Element("gamesWon").Value = gamesWon.ToString();
                doc.Save(usersFilePath);
            }
        }

        public override string ToString()
        {
            return this.username;
        }
    }
}
