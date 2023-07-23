using HexBOT.Wrappers;
using System.Numerics;

namespace HexBOT
{
    internal class Boot
    {
        public static List<APIClient> RedditClients = new();

        public static void Main()
        {
            Console.Title = "Reddit BOT";

            if (!File.Exists("Accounts.txt")) return;

            foreach (string Token in File.ReadLines("Accounts.txt"))
            {
                APIClient client = APIClient.Login(Token);
                RedditClients.Add(client);
            }

            Console.Title = $"Reddit BOT | {RedditClients.Count} Bots";

            RunGUI();
        }

        private static void RunGUI()
        {
            for (; ; )
            {
                Logger.LogImportant("-----------------");
                Logger.LogImportant("1 - PLACE");
                Logger.LogImportant("-----------------");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Logger.LogImportant("-----------------");
                        Logger.LogImportant("W [X] [Y] - Draw the Watermark");
                        Logger.LogImportant("P [X] [Y] - Draw a Pixel");
                        Logger.LogImportant("-----------------");
                        HandlePlaceInput(Console.ReadLine());
                        break;
                }
            }
        }

        private static void HandlePlaceInput(string input)
        {
            string[] split = input.Split(' ');

            switch (split[0].ToLower())
            {
                case "w":
                    {
                        int x = int.Parse(split[1]);
                        int y = int.Parse(split[2]);

                        Vector2 position = new(x, y);
                        Task.Run(() => PixelDrawer.DrawWatermark(position, CustomObjects.PixelColor.DarkPurple));
                    }              
                    break;

                case "p":
                    {
                        int x = int.Parse(split[1]);
                        int y = int.Parse(split[2]);

                        Vector2 position = new(x, y);
                        Task.Run(() => RedditClients[0].PlacePixel(position, (int)CustomObjects.PixelColor.DarkPurple));
                    }
                    break;

            }
        }
    }
}
