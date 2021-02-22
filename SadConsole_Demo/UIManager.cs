using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using SadConsole;
using SadConsole.Controls;

namespace SadConsole_Demo
{
    // Creates/Holds/Destroys all consoles used in the game
    // and makes consoles easily addressable from a central place.
    public class UIManager : ContainerConsole
    {
        public SadConsole.ScrollingConsole MapConsole;
        public Window MapWindow;

        public UIManager()
        {
            // must be set to true
            // or will not call each child's Draw method
            IsVisible = true;
            IsFocused = true;

            // The UIManager becomes the only
            // screen that SadConsole processes
            Parent = SadConsole.Global.CurrentScreen;
        }

        // Initializes all windows and consoles
        public void Init()
        {
            CreateConsoles();
            CreateMapWindow(Program.GameWidth / 2, Program.GameHeight / 2, "Game Map");
        }

        // Creates all child consoles to be managed
        public void CreateConsoles()
        {
            MapConsole = new SadConsole.ScrollingConsole(Program.World.CurrentMap.Width, Program.World.CurrentMap.Height, Global.FontDefault, new Rectangle(0, 0, Program.GameWidth, Program.GameHeight), Program.World.CurrentMap.Tiles);
        }

        // centers the viewport camera on an Actor
        public void CenterOnActor(Actor actor)
        {
            MapConsole.CenterViewPortOnPoint(actor.Position);
        }

        public override void Update(TimeSpan timeElapsed)
        {
            CheckKeyboard();
            base.Update(timeElapsed);
        }

        // Scans the SadConsole's Global KeyboardState and triggers behaviour
        // based on the button pressed.
        private void CheckKeyboard()
        {
            // As an example, we'll use the F5 key to make the game full screen
            if (SadConsole.Global.KeyboardState.IsKeyReleased(Microsoft.Xna.Framework.Input.Keys.F5))
            {
                SadConsole.Settings.ToggleFullScreen();
            }

            // Keyboard movement for Player character: Up arrow
            // Decrement player's Y coordinate by 1
            if (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Up))
            {
                Program.World.Player.MoveBy(new Point(0, -1));
                CenterOnActor(Program.World.Player);
            }

            // Keyboard movement for Player character: Down arrow
            // Increment player's Y coordinate by 1
            if (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Down))
            {
                Program.World.Player.MoveBy(new Point(0, 1));
                CenterOnActor(Program.World.Player);
            }

            // Keyboard movement for Player character: Left arrow
            // Decrement player's X coordinate by 1
            if (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Left))
            {
                Program.World.Player.MoveBy(new Point(-1, 0));
                CenterOnActor(Program.World.Player);
            }

            // Keyboard movement for Player character: Right arrow
            // Increment player's X coordinate by 1
            if (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Right))
            {
                Program.World.Player.MoveBy(new Point(1, 0));
                CenterOnActor(Program.World.Player);
            }
        }

        // Creates a window that encloses a map console
        // of a specified height and width
        // and displays a centered window title
        // make sure it is added as a child of the UIManager
        // so it is updated and drawn
        public void CreateMapWindow(int width, int height, string title)
        {
            MapWindow = new Window(width, height);
            MapWindow.CanDrag = true;

            //make console short enough to show the window title
            //and borders, and position it away from borders
            int mapConsoleWidth = width - 2;
            int mapConsoleHeight = height - 2;

            // Resize the Map Console's ViewPort to fit inside of the window's borders snugly
            MapConsole.ViewPort = new Rectangle(0, 0, mapConsoleWidth, mapConsoleHeight);

            //reposition the MapConsole so it doesnt overlap with the left/top window edges
            MapConsole.Position = new Point(1, 1);

            //close window button
            Button closeButton = new Button(3, 1);
            closeButton.Position = new Point(0, 0);
            closeButton.Text = "[X]";

            //Add the close button to the Window's list of UI elements
            MapWindow.Add(closeButton);

            // Centre the title text at the top of the window
            MapWindow.Title = title.Align(HorizontalAlignment.Center, mapConsoleWidth);

            //add the map viewer to the window
            MapWindow.Children.Add(MapConsole);

            // The MapWindow becomes a child console of the UIManager
            Children.Add(MapWindow);

            // Without this, the window will never be visible on screen
            MapWindow.Show();
        }
    }
}
