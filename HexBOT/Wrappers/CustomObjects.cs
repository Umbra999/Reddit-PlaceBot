namespace HexBOT.Wrappers
{
    internal class CustomObjects
    {
        public enum PixelColor : int
        {
            DarkGreen = 0,
            DarkBlue = 12,
            DarkPurple = 18,
            Black = 27,
        }

        public class Coordinate
        {
            public int x { get; set; }
            public int y { get; set; }
        }

        public class Input
        {
            public PixelMessageData PixelMessageData { get; set; }
            public string actionName { get; set; }
        }

        public class PixelMessageData
        {
            public int canvasIndex { get; set; }
            public int colorIndex { get; set; }
            public Coordinate coordinate { get; set; }
        }

        public class PixelRequest
        {
            public string operationName { get; set; }
            public string query { get; set; }
            public Variables variables { get; set; }
        }

        public class Variables
        {
            public Input input { get; set; }
        }
    }
}
