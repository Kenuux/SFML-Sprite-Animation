using characters;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML_Test.characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SFML_Test
{
    static class Program
    {
        
        static void Main()
        {
            // INITIALIZING WINDOW
            RenderWindow renderWindow = new RenderWindow(new VideoMode(800, 600), "www.github.com/kenuux - SFML Learning Purpose - Sprite Movement/Animation", Styles.Close, new ContextSettings(32, 0, 8));
            renderWindow.SetFramerateLimit(60);
            renderWindow.Closed += RenderWindow_Closed;

            // ASSETS CREDITS
            Text assetsBy = new Text("Assets by lionheart963.itch.io", new Font("fonts/PixelFJVerdana12pt.ttf"));
            assetsBy.Scale = new Vector2f(0.35f, 0.35f);
            assetsBy.Position = new Vector2f(30, renderWindow.Size.Y - 30);

            // FPS COUNT LABEL
            Text fpsStats = new Text("FPS: ?", new Font("fonts/PixelFJVerdana12pt.ttf"));
            fpsStats.Scale = new Vector2f(0.35f, 0.35f);
            fpsStats.Position = new Vector2f(30, 30);

            // CHARACTER
            Character bob = new Character("BOB", new IntRect(0, 0, 24, 32), new Vector2f(renderWindow.Size.X / 2, renderWindow.Size.Y / 2 / 2), new Animation[] {
                new Animation("sprites/idle.png", 3, 60, 0, 60, 60, 350f),
                new Animation("sprites/run.png", 5, 60, 0, 60, 60, 100f)
            });

            // BLACK WINDOW
            Color windowColor = new Color(0, 0, 0);

            // CLOCKS
            Clock fpsClock = new Clock();
            Clock deltaTimeClock = new Clock();
            while (renderWindow.IsOpen)
            {
                renderWindow.DispatchEvents();

                // DELTA TIME
                float dt = deltaTimeClock.Restart().AsMilliseconds();

                // CHARACTER
                bob.HandleInput(renderWindow);
                bob.Update(dt);

                renderWindow.Clear(windowColor);

                // FPS COUNT
                float framerate = 1.0f / fpsClock.Restart().AsSeconds();
                fpsStats.DisplayedString = fpsStats.DisplayedString = "FPS: " + Math.Round(framerate).ToString();

                // DRAWING
                bob.Draw(renderWindow);
                renderWindow.Draw(assetsBy);
                renderWindow.Draw(fpsStats);


                renderWindow.Display();
            }
        }

        private static void RenderWindow_Closed(object sender, EventArgs e)
        {
            RenderWindow window = (RenderWindow) sender;
            window.Close();
        }
    }
}
