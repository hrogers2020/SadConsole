using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace SadConsole_Demo
{
    // TileWall is based on TileBase
    // Walls are to be used in maps.
    public class TileWall : TileBase
    {
        // Parameterized constructor
        // Walls are set to block movement and line of sight by default
        // and have a dark gray foreground and a transparent background
        // represented by the # symbol
        public TileWall(bool blocksMovement = true, bool blocksLOS = true) : base(Color.LightGray, Color.Transparent, '#', blocksMovement, blocksLOS)
        {
            Name = "Wall";
        }
    }
}
