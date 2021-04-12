using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Brickbreaker
{
    class GameBorder
    {
        public float screenWidth;
        public float screenHeight;

        private Texture2D imgPixel;
        private SpriteBatch spriteBatch;

        public GameBorder(float screenWidth, float screenHeight, SpriteBatch spriteBatch, GameContent gameContent)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            imgPixel = gameContent.imgPixel;
            this.spriteBatch = spriteBatch;
        }

        public void Draw()
        {
            spriteBatch.Draw(imgPixel, new Rectangle(0, 0, (int)screenWidth - 1, 1), Color.White);
            spriteBatch.Draw(imgPixel, new Rectangle(0, 0, 1, (int)screenHeight - 1), Color.White);
            spriteBatch.Draw(imgPixel, new Rectangle((int)screenWidth - 1, 0, 1, (int)screenHeight - 1), Color.White);
            spriteBatch.Draw(imgPixel, new Rectangle(0, (int)screenHeight - 1, (int)screenWidth - 1, 1), Color.White);
        }
    }
}
