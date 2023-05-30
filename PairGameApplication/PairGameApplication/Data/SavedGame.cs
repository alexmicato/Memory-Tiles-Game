using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PairGameApplication.Data
{
    [Serializable]
    public class SavedGame
    {
        public User user {  get; set; }
        public PairGame game { get; set; }

        public static string savedGamesFilePath = "../../../Database/savedGames.xml";

        public SavedGame()
        {

        }

        public SavedGame(User user, PairGame game)
        {
            this.user = user;
            this.game = new PairGame(game);
        }
    }
}
