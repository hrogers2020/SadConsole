using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace SadConsole_Demo
{
    public class Map
    {
        TileBase[] _tiles; // contain all tile objects
        private int _width;
        private int _height;

        public TileBase[] Tiles { get { return _tiles; } set { _tiles = value; } }
        public int Width { get { return _width; } set { _width = value; } }
        public int Height { get { return _height; } set { _height = value; } }

        //Build a new map with a specified width and height
        public Map(int width, int height)
        {
            _width = width;
            _height = height;
            Tiles = new TileBase[width * height];
        }

        // Overload to allow a Point parameter
        public Map(Point location)
        {
            _width = location.X;
            _height = location.Y;
            Tiles = new TileBase[location.X * location.Y];
        }

        // IsTileWalkable checks
        // to see if the actor has tried
        // to walk off the map or into a non-walkable tile
        // Returns true if the tile location is walkable
        // false if tile location is not walkable or is off-map
        public static bool IsTileWalkable(Point location)
        {
            // I added this because Width needed an instance to be accessed
            var map = new Map(location);

            // first make sure that actor isn't trying to move
            // off the limits of the map
            if (location.X < 0 || location.Y < 0 || location.X >= map.Width || location.Y >= map.Height)
                return false;
            // then return whether the tile is walkable
            return !map._tiles[location.Y * map.Width + location.X].IsBlockingMove;
        }
    }
}
