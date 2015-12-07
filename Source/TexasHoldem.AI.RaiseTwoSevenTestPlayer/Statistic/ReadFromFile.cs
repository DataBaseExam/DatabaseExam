namespace TexasHoldem.AI.RaiseTwoSevenTestPlayer.Statistics
{
    using System.IO;
    public static class ReadFromFile
    {
        private static string filePath;

        private static bool fileExist;

        private static string[] lines;

        public static bool ReadFile(string filePath)
        {
            if (!File.Exists("TextFile1.txt"))
            {
                return false;
            }
            else
            {
                lines = File.ReadAllLines(filePath);
                return true;
            }

        }

        public static string[] Lines
        {
            get
            {
                return lines;
            }

            private set
            {
                lines = value;
            }
        }
    }
}