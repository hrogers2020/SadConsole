using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace SadConsole_Demo
{
    public abstract class Actor : SadConsole.Entities.Entity
    {
        private int _health; //current health
        private int _maxHealth; //maximum possible health

        public int Health { get { return (_health / _maxHealth) * 100; } set { _health = value; } } // public getter/setter for current health ( returns health as % )
        public int MaxHealth { get { return _maxHealth; } set { _maxHealth = value; } } // public getter/setter for max health

        protected Actor(Color foreground, Color background, int glyph, int width = 1, int height = 1) : base(width, height)
        {
            Animation.CurrentFrame[0].Foreground = foreground;
            Animation.CurrentFrame[0].Background = background;
            Animation.CurrentFrame[0].Glyph = glyph;
        }

        // Moves the Actor BY positionChange tiles in any X/Y direction
        // returns true if actor was able to move, false if failed to move
        public bool MoveBy(Point positionChange)
        {
            // Check the map if we can move to this new position
            if (SadConsole_Demo.Map.IsTileWalkable(Position + positionChange))
            {
                Position += positionChange;
                return true;
            }
            else
                return false;
        }

        // Moves the Actor TO newPosition location
        // returns true if actor was able to move, false if failed to move
        public bool MoveTo(Point newPosition)
        {
            Position = newPosition;
            return true;
        }
    }
}
