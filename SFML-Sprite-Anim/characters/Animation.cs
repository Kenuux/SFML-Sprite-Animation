using SFML.Graphics;
using SFML_Test.characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace characters
{
    class Animation
    {
        public Texture texture { get; set; }
        int nFrames;
        IntRect[] frames;
        int iFrame = 0;
        float holdTime;
        float time = 0.0f;

        public Animation(string textureFileName, int nFrames, int x, int y, int width, int height, float holdTime)
        {
            this.holdTime = holdTime;
            this.nFrames = nFrames;
            frames = new IntRect[this.nFrames];
            texture = new Texture(textureFileName);
            texture.Smooth = false;
            for (int i = 0; i < this.nFrames; i++) {
                frames[i] = new IntRect(x + (i * width), y, width, height);
            }
        }

        public void ApplyToSprite(Sprite sprite)
        {
            sprite.Texture = texture;
            sprite.TextureRect = frames[iFrame];
        }

        public void Update(float dt)
        {
            time += dt;
            while(time >= holdTime)
            {
                time -= holdTime;
                Advance();
            }
        }

        private void Advance()
        {
            if(++iFrame >= nFrames)
            {
                iFrame = 0;
            }
        }
    }
}
