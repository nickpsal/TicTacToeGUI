using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeGUI
{
    public class Player
    {
        public string? PlayerName { get; set; }
        public char PlayerSymbol { get; set; }
        public int PlayerScore { get; set; }

        public Player(string playerName, char playerSymbol)
        {
            this.PlayerName = playerName;
            this.PlayerSymbol = playerSymbol;
            this.PlayerScore = 0;
        }
    }
}
