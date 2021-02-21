using System;
using SadConsole;
using Console = SadConsole.Console;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace SadConsole_Demo
{
    class Program
    {

        public const int Width = 80;
        public const int Height = 25;

        static void Main(string[] args)
        {
            // Setup the engine and create the main window.
            SadConsole.Game.Create(Width, Height);

            // Hook the start event so we can add consoles to the system.
            SadConsole.Game.OnInitialize = Init;

            SadConsole.Game.OnUpdate = Update;

            // Start the game.
            // TODO: Uncomment the Run() method below

            SadConsole.Game.Instance.Run();
            SadConsole.Game.Instance.Dispose();
        }
        private static void Update(GameTime gameTime)
        {
            if (SadConsole.Global.KeyboardState.IsKeyReleased(Microsoft.Xna.Framework.Input.Keys.F5))
            {
                SadConsole.Settings.ToggleFullScreen();
            }
        }

        private static void Init()
        {
            // Any startup code for your game. We will use an example console for now
            var startingConsole = SadConsole.Global.CurrentScreen;

            // TODO: Uncomment the method below to call the FillWithRandomGarbage() method
            startingConsole.Print(10, 0, "Hello SadConsole!", ColorAnsi.CyanBright, ColorAnsi.Black);

            var button1 = new SadConsole.Controls.Button(10,2);
            var cellDecorator1 = new SadConsole.CellDecorator(Color.Blue, 2, SpriteEffects.None);
            startingConsole.SetDecorator(0, 1, cellDecorator1);
            

        }
    }
}
