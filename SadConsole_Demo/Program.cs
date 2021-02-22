using System;
using SadConsole;
using Console = SadConsole.Console;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SadConsole.Components;

namespace SadConsole_Demo
{
    public class Program
    {

        public const int Width = 80;
        public const int Height = 25;
        private static Player player;

        public static Map GameMap;
        private static int _mapWidth = 100;
        private static int _mapHeight = 100;
        private static int _maxRooms = 500;
        private static int _minRoomSize = 4;
        private static int _maxRoomSize = 15;

        private static ScrollingConsole startingConsole;

        static void Main(string[] args)
        {
            // Setup the engine and create the main window.
            SadConsole.Game.Create(Width, Height);

            // Hook the start event so we can add consoles to the system.
            SadConsole.Game.OnInitialize = Init;

            // Hook the update event that happens each frame so we can trap keys and respond.
            SadConsole.Game.OnUpdate = Update;

            // Start the game.
            SadConsole.Game.Instance.Run();

            //
            // Code here will not run until the game window closes.
            //

            SadConsole.Game.Instance.Dispose();
        }

        private static void Update(GameTime time)
        {
            // Called each logic update.
            CheckKeyboard();
        }

        // Scans the SadConsole's Global KeyboardState and triggers behaviour
        // based on the button pressed.
        private static void CheckKeyboard()
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
                player.MoveBy(new Point(0, -1));
                CenterOnActor(player);
            }

            // Keyboard movement for Player character: Down arrow
            // Increment player's Y coordinate by 1
            if (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Down))
            {
                player.MoveBy(new Point(0, 1));
                CenterOnActor(player);
            }

            // Keyboard movement for Player character: Left arrow
            // Decrement player's X coordinate by 1
            if (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Left))
            {
                player.MoveBy(new Point(-1, 0));
                CenterOnActor(player);
            }

            // Keyboard movement for Player character: Right arrow
            // Increment player's X coordinate by 1
            if (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Right))
            {
                player.MoveBy(new Point(1, 0));
                CenterOnActor(player);
            }
        }

        private static void Init()
        {

            // Initialize an empty map
            GameMap = new Map(_mapWidth, _mapHeight);

            // Instantiate a new map generator and
            // populate the map with rooms and tunnels
            MapGenerator mapGen = new MapGenerator();
            GameMap = mapGen.GenerateMap(_mapWidth, _mapHeight, _maxRooms, _minRoomSize, _maxRoomSize);

            // Create a console using gameMap's tiles
            Console startingConsole = new ScrollingConsole(GameMap.Width, GameMap.Height, Global.FontDefault, new Rectangle(0, 0, Width, Height), GameMap.Tiles);

            // Set our new console as the thing to render and process
            SadConsole.Global.CurrentScreen = startingConsole;

            // create an instance of player
            CreatePlayer();

            // add the EntityViewSyncComponent to the player
            player.Components.Add(new EntityViewSyncComponent());

            // add the player Entity to our only console
            // so it will display on screen
            startingConsole.Children.Add(player);

        }

        // centers the viewport camera on an Actor
        public static void CenterOnActor(Actor actor)
        {
            startingConsole.CenterViewPortOnPoint(actor.Position);
        }

        // Create a player using the Player class
        // and set its starting position
        private static void CreatePlayer()
        {
            player = new Player(Color.Yellow, Color.Transparent);
            player.Position = new Point(5, 5);
        }
    }

}

