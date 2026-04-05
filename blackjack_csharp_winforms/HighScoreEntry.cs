using System;

namespace blackjack
{
    public class HighScoreEntry
    {
        public string Date { get; set; }
        public string PlayerName { get; set; }
        public int PlayerScore { get; set; }
        public int ComputerScore { get; set; }
        public int PlayerWin { get; set; }
        public int ComputerWin { get; set; }
        public int NoneWin { get; set; }

        public HighScoreEntry()
        {
            Date = "";
            PlayerName = "";
            PlayerScore = 0;
            ComputerScore = 0;
            PlayerWin = 0;
            ComputerWin = 0;
            NoneWin = 0;
        }
    }
}
