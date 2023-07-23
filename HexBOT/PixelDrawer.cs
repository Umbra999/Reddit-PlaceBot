using HexBOT.Wrappers;
using System.Numerics;

namespace HexBOT
{
    internal class PixelDrawer
    {
        public static async Task DrawWatermark(Vector2 Position, CustomObjects.PixelColor color)
        {
            if (Boot.RedditClients.Count < 47)
            {
                Logger.LogError("47 Bots are needed for this drawing");
                return;
            }

            List<Vector2> positions = new()
            {
                // H
                new Vector2(Position.X, Position.Y),
                new Vector2(Position.X, Position.Y - 1),
                new Vector2(Position.X, Position.Y - 2),
                new Vector2(Position.X, Position.Y - 3),
                new Vector2(Position.X, Position.Y - 4),

                new Vector2(Position.X + 1, Position.Y - 2),

                new Vector2(Position.X + 2, Position.Y),
                new Vector2(Position.X + 2, Position.Y - 1),
                new Vector2(Position.X + 2, Position.Y - 2),
                new Vector2(Position.X + 2, Position.Y - 3),
                new Vector2(Position.X + 2, Position.Y - 4),

                // E
                new Vector2(Position.X + 3, Position.Y),
                new Vector2(Position.X + 4, Position.Y),
                new Vector2(Position.X + 3, Position.Y - 1),
                new Vector2(Position.X + 3, Position.Y - 2),
                new Vector2(Position.X + 3, Position.Y - 3),
                new Vector2(Position.X + 4, Position.Y - 3),
                new Vector2(Position.X + 3, Position.Y - 4),

                // X
                new Vector2(Position.X + 6, Position.Y),
                new Vector2(Position.X + 7, Position.Y - 1),
                new Vector2(Position.X + 8, Position.Y),
                new Vector2(Position.X + 6, Position.Y - 1),
                new Vector2(Position.X + 7, Position.Y),
                new Vector2(Position.X + 8, Position.Y - 1),

                // E
                new Vector2(Position.X + 10, Position.Y),
                new Vector2(Position.X + 11, Position.Y),
                new Vector2(Position.X + 10, Position.Y - 1),
                new Vector2(Position.X + 10, Position.Y - 2),
                new Vector2(Position.X + 10, Position.Y - 3),
                new Vector2(Position.X + 11, Position.Y - 3),
                new Vector2(Position.X + 10, Position.Y - 4),

                // D
                new Vector2(Position.X + 13, Position.Y),
                new Vector2(Position.X + 14, Position.Y),
                new Vector2(Position.X + 13, Position.Y - 1),
                new Vector2(Position.X + 13, Position.Y - 2),
                new Vector2(Position.X + 13, Position.Y - 3),
                new Vector2(Position.X + 13, Position.Y - 4),
                new Vector2(Position.X + 14, Position.Y - 1),
                new Vector2(Position.X + 15, Position.Y - 2),
                new Vector2(Position.X + 16, Position.Y - 3)
            };

            int Current = 0;
            foreach (Vector2 pos in positions)
            {
                await Boot.RedditClients[Current++].PlacePixel(pos, (int)color);
            }

            Logger.LogSuccess("HEXED watermark has been drawn");
        }
    }
}
