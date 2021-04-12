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
    class Wall
    {
        public Brick[,] brickWall;
        
        public Wall(float posX, float posY, SpriteBatch spriteBatch, GameContent gameContent)
        {
            brickWall = new Brick[7, 10];
            float brickX = posX;
            float brickY = posY;
            Color color = Color.White;
            for (int i = 0; i < 7; i++)
            {
                switch(i)
                {
                    case 0:
                        color = Color.Red;
                        break;
                    case 1:
                        color = Color.Orange;
                        break;
                    case 2:
                        color = Color.Yellow;
                        break;
                    case 3:
                        color = Color.Green;
                        break;
                    case 4:
                        color = Color.Blue;
                        break;
                    case 5:
                        color = Color.Indigo;
                        break;
                    case 6:
                        color = Color.Violet;
                        break;
                }
                brickY = posY + i * (gameContent.imgBrick.Height + 1);

                for (int j = 0; j < 10; j++)
                {
                    brickX = posX + j * (gameContent.imgBrick.Width);
                    Brick brick = new Brick(brickX, brickY, color, spriteBatch, gameContent);
                    brickWall[i, j] = brick;
                }
            }
        }

        public void Draw()
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    brickWall[i, j].Draw();
                }
            }
        }
    }
}
