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
    class Paddle
    {
        public float posX;
        public float posY;
        public float paddleWidth;
        public float paddleHeight;
        public float screenWidth;

        private Texture2D _imgPaddle;
        private SpriteBatch spriteBatch;

        public Paddle(float posX, float posY, float screenWidth, SpriteBatch spriteBatch, GameContent gameContent)
        {
            this.posX = posX;
            this.posY = posY;
            _imgPaddle = gameContent.imgPaddle;
            paddleWidth = _imgPaddle.Width;
            paddleHeight = _imgPaddle.Height;
            this.spriteBatch = spriteBatch;
            this.screenWidth = screenWidth;
        }

        public void Draw()
        {
            spriteBatch.Draw(_imgPaddle, new Vector2(posX, posY), null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
        }

        public void MoveLeft(float x)
        {
            posX -= x;
            if (posX < 1) { posX = 1; }
        }

        public void MoveRight(float x)
        {
            posX += x;
            if((posX + paddleWidth) > screenWidth)
            {
                posX = screenWidth - paddleWidth;
            }
        }

        public void MoveTo(float x)
        {
            if(x>=0)
            {
                if(x < screenWidth - paddleWidth) { posX = x; }
                else { posX = screenWidth - paddleWidth; }
            }
            else
            {
                if(x < 0) { posX = 0; }
            }
            
        }
    }
}
