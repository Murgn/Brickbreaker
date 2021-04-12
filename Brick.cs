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
    class Brick
    {
        public float posX;
        public float posY;
        public float brickWidth;
        public float brickHeight;
        public bool isVisible;
        private Color color;

        private Texture2D imgBrick;
        private SpriteBatch spriteBatch;

        public Brick(float posX, float posY, Color color, SpriteBatch spriteBatch, GameContent gameContent)
        {
            this.posX = posX;
            this.posY = posY;
            imgBrick = gameContent.imgBrick;
            brickWidth = imgBrick.Width;
            brickHeight = imgBrick.Height;
            this.spriteBatch = spriteBatch;
            isVisible = true;
            this.color = color;
        }

        public void Draw()
        {
            if(isVisible)
            {
                spriteBatch.Draw(imgBrick, new Vector2(posX, posY), null, color, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            }
        }

    }
}
