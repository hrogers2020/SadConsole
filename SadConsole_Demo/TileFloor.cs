using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace SadConsole_Demo
{
    public class TileFloor : TileBase
    {
        // Parameterized constructor
        // Floors are set to allow movement and line of sight by default
        // and have a dark gray foreground and a transparent background
        // represented by the . symbol
        public TileFloor(bool blocksMovement = false, bool blocksLOS = false) : base(Color.DarkGray, Color.Transparent, '.', blocksMovement, blocksLOS)
        {
            Name = "Floor";
        }
    }
}
