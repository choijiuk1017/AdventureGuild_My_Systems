using System;

namespace Core.Guild.Building
{
    public enum TileType
    {
        None, Floor, Entity, Obstacle, Last
    }

    [Serializable]
    public class Tile
    {
        private int x, y;
        public int X { get { return x; } }
        public int Y { get { return y; } }
        public bool isEmpty = true;

        public Tile(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}