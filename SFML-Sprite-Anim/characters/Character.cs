using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using characters;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SFML_Test.characters
{
    enum WalkingIndex
    {
        IDLE, WALK
    }

    class Character : IDisposable
    {
        public Sprite sprite { get; }
        private Vector2f pos;
        private Vector2f vel = new Vector2f(0.0f, 0.0f);
        private float speed = 0.5f;
        private Animation[] animations = new Animation[Enum.GetNames(typeof(WalkingIndex)).Length];
        private WalkingIndex walkingIndex = WalkingIndex.IDLE;
        private Font font;
        private Text text;

        public Character(string label, IntRect textureRect, Vector2f position, Animation[] animations)
        {
            this.pos = position;
            font = new Font("fonts/ARCADECLASSIC.TTF");
            text = new Text(label, font);
            for(int i = 0; i < animations.Length; i++)
            {
                this.animations[i] = animations[i];
            }

            sprite = new Sprite();
            sprite.Position = position;
            sprite.Scale = new Vector2f(5, 5);
        }

        public void Draw(RenderWindow renderWindow)
        {
            renderWindow.Draw(sprite);
            renderWindow.Draw(text);
        }

        public void Update(float dt)
        {
            // MOVE CHAR
            pos += vel * dt;
            animations[(int)walkingIndex].Update(dt);
            animations[(int)walkingIndex].ApplyToSprite(sprite);
            sprite.Position = pos;

            // SET ORIGIN TO CENTER
            sprite.Origin = new Vector2f(sprite.GetLocalBounds().Width / 2, sprite.GetLocalBounds().Height / 2);

            // CHAR LABEL
            text.Origin = new Vector2f(text.GetLocalBounds().Width / 2, sprite.GetLocalBounds().Height / 2);
            text.Position = new Vector2f((int)sprite.Position.X, (int)sprite.Position.Y - 100);
            text.Scale = new Vector2f(0.9f, 0.9f);
        }

        private void SetDirection(Vector2f dir)
        {
            vel = dir * speed;
        }
             
        public void HandleInput(RenderWindow renderWindow)
        {
            Vector2f dir = new Vector2f(0.0f, 0.0f);
            if(Keyboard.IsKeyPressed(Keyboard.Key.W) || Keyboard.IsKeyPressed(Keyboard.Key.Up)) {
                dir.Y -= 1.0f;
                walkingIndex = WalkingIndex.WALK;
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.A) || Keyboard.IsKeyPressed(Keyboard.Key.Left))
            {
                dir.X -= 1.0f;
                walkingIndex = WalkingIndex.WALK;
                sprite.Origin = new Vector2f(sprite.GetLocalBounds().Width, 0);
                sprite.Scale = new Vector2f(-5, 5);
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.S) || Keyboard.IsKeyPressed(Keyboard.Key.Down))
            {
                dir.Y += 1.0f;
                walkingIndex = WalkingIndex.WALK;
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.D) || Keyboard.IsKeyPressed(Keyboard.Key.Right))
            {
                dir.X += 1.0f;
                walkingIndex = WalkingIndex.WALK;
                sprite.Origin = new Vector2f(0, 0);
                sprite.Scale = new Vector2f(5, sprite.Scale.Y);
            }
            else
            {
                dir = new Vector2f(0.0f, 0.0f);
                walkingIndex = WalkingIndex.IDLE;
            }
            SetDirection(dir);
        }

        public void Dispose()
        {
            font.Dispose();
            text.Dispose();
            sprite.Dispose();
        }
    }
}

