﻿using SadRogue.Primitives;
using SadConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using SadConsole.UI.Themes;
using SadConsole.UI.Controls;

namespace SadConsole.UI.Controls
{
    public class HueBar : SadConsole.UI.Controls.ControlBase
    {
        public event EventHandler ColorChanged;

        public Color SelectedColor
        {
            get { return _selectedColor; }
            set
            {
                SetClosestIndex(value);

                if (_selectedColor != value)
                {
                    _selectedColor = value;

                    ColorChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private Color SelectedColorSafe
        {
            set
            {
                if (_selectedColor != value)
                {
                    _selectedColor = value;

                    ColorChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// The position on the bar currently selected.
        /// </summary>
        public int SelectedPosition { get => _selectedPosition; }

        /// <summary>
        /// Internal use by theme.
        /// </summary>
        public int _positions;

        private int _selectedPosition;

        private Color _selectedColor;

        public HueBar(int width): base(width, 2)
        {
            CanFocus = false;
        }

        private void SetClosestIndex(Color color)
        {
            ColorMine.ColorSpaces.Rgb rgbColorStop = new ColorMine.ColorSpaces.Rgb() { R = color.R, G = color.G, B = color.B };
            Tuple<Color, double, int>[] colorWeights = new Tuple<Color, double, int>[Width];

            // Create a color weight for every cell compared to the color stop
            for (int x = 0; x < Width; x++)
            {
                ColorMine.ColorSpaces.Rgb rgbColor = new ColorMine.ColorSpaces.Rgb() { R = Surface[x, 0].Foreground.R, G = Surface[x, 0].Foreground.G, B = Surface[x, 0].Foreground.B };
                ColorMine.ColorSpaces.Cmy cmyColor = rgbColor.To<ColorMine.ColorSpaces.Cmy>();

                colorWeights[x] = new Tuple<Color, double, int>(Surface[x, 0].Foreground, rgbColorStop.Compare(cmyColor, new ColorMine.ColorSpaces.Comparisons.Cie1976Comparison()), x);

            }

            var foundColor = colorWeights.OrderBy(t => t.Item2).First();
            _selectedPosition = foundColor.Item3;
            this.IsDirty = true;
        }
        
        protected override void OnMouseIn(ControlMouseState info)
        {
            base.OnMouseIn(info);

            if (Parent.Host.CapturedControl == null)
            {
                if (info.OriginalMouseState.Mouse.LeftButtonDown)
                {
                    var location = info.MousePosition;
                    _selectedPosition = location.X;
                    SelectedColorSafe = Surface[_selectedPosition, 0].Foreground;
                    IsDirty = true;

                    Parent.Host.CaptureControl(this);
                }
            }
        }

        public override bool ProcessMouse(SadConsole.Input.MouseScreenObjectState info)
        {
            if (Parent.Host.CapturedControl == this)
            {
                if (info.Mouse.LeftButtonDown == false)
                    Parent.Host.ReleaseControl();
                else
                {
                    var newState = new ControlMouseState(this, info);
                    var location = newState.MousePosition;

                    //if (info.ConsolePosition.X >= Position.X && info.ConsolePosition.X < Position.X + Width)
                    if (location.X >= 0 && location.X <= Width - 1 && location.Y > -4 && location.Y < Height + 3 )
                    {
                        _selectedPosition = location.X;
                        SelectedColorSafe = Surface[_selectedPosition, 0].Foreground;
                    }

                    IsDirty = true;
                }
            }

            return base.ProcessMouse(info);
        }
    }
}
