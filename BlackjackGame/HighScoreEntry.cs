using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BlackjackGame
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
        public int NumberOfGames { get; set; }

        public HighScoreEntry()
        {
            Date = "";
            PlayerName = "";
            PlayerScore = 0;
            ComputerScore = 0;
            PlayerWin = 0;
            ComputerWin = 0;
            NoneWin = 0;
            NumberOfGames = 0;
        }
    }

    public static class HighScoreManager
    {
        private const string SAVE_GAME_FILE = "save_game.txt";

        /// <summary>
        /// Load high scores from the save game file
        /// </summary>
        public static List<HighScoreEntry> LoadHighScores()
        {
            List<HighScoreEntry> scores = new List<HighScoreEntry>();

            if (!File.Exists(SAVE_GAME_FILE))
            {
                return scores;
            }

            try
            {
                string[] lines = File.ReadAllLines(SAVE_GAME_FILE);

                for (int i = 0; i < lines.Length; i += 3)
                {
                    if (i + 2 >= lines.Length)
                    {
                        break;
                    }

                    HighScoreEntry entry = new HighScoreEntry();
                    entry.Date = lines[i];
                    entry.PlayerName = lines[i + 1];

                    // Parse the scores line: playerScore computerScore playerWin computerWin noneWin
                    string[] scoreData = lines[i + 2].Split(' ');
                    if (scoreData.Length >= 5)
                    {
                        if (int.TryParse(scoreData[0], out int ps))
                            entry.PlayerScore = ps;
                        if (int.TryParse(scoreData[1], out int cs))
                            entry.ComputerScore = cs;
                        if (int.TryParse(scoreData[2], out int pw))
                            entry.PlayerWin = pw;
                        if (int.TryParse(scoreData[3], out int cw))
                            entry.ComputerWin = cw;
                        if (int.TryParse(scoreData[4], out int nw))
                            entry.NoneWin = nw;
                    }

                    scores.Add(entry);
                }
            }
            catch (Exception ex)
            {
                // Silently fail - file may be corrupted
                System.Diagnostics.Debug.WriteLine("Error loading high scores: " + ex.Message);
            }

            return scores;
        }

        /// <summary>
        /// Save a high score entry to the file
        /// </summary>
        public static void SaveHighScore(HighScoreEntry entry)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(SAVE_GAME_FILE, true, Encoding.UTF8))
                {
                    writer.WriteLine(entry.Date);
                    writer.WriteLine(entry.PlayerName);
                    writer.WriteLine($"{entry.PlayerScore} {entry.ComputerScore} {entry.PlayerWin} {entry.ComputerWin} {entry.NoneWin}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error saving high score: " + ex.Message);
            }
        }

        /// <summary>
        /// Get formatted high score table as text
        /// </summary>
        public static string GetHighScoreTable(List<HighScoreEntry> scores)
        {
            StringBuilder table = new StringBuilder();

            foreach (var entry in scores)
            {
                table.AppendLine($" {entry.Date} {entry.PlayerName} {entry.PlayerScore} {entry.ComputerScore}\n\n");
            }

            if (table.Length == 0)
            {
                table.AppendLine("No high scores yet.");
            }

            return table.ToString();
        }
    }
}
