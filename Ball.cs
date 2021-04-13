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
    class Ball
    {
        public float posX;
        public float posY;
        public Vector2 velocity;
        public float ballHeight;
        public float ballWidth;
        public float ballRotation;
        public bool useRotation;
        public float screenWidth;
        public float screenHeight;
        public bool isVisible;
        public int score;
        public int bricksCleared;

        private Texture2D imgBall;
        private SpriteBatch spriteBatch;
        private GameContent gameContent;

        public Ball(float screenWidth, float screenHeight, SpriteBatch spriteBatch, GameContent gameContent)
        {
            posX = 0;
            posY = 0;
            velocity = Vector2.Zero;
            ballRotation = 0;
            imgBall = gameContent.imgBall;
            ballHeight = imgBall.Height;
            ballWidth = imgBall.Width;
            this.spriteBatch = spriteBatch;
            this.gameContent = gameContent;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            isVisible = false;
            score = 0;
            bricksCleared = 0;
            useRotation = true;
        }

        public void Draw()
        {
            if(isVisible == false) { return; }

            if(useRotation)
            {
                ballRotation += .1f;
                if(ballRotation > 3 * Math.PI) { ballRotation = 0; }
            }
            spriteBatch.Draw(imgBall, new Vector2(posX, posY), null, Color.White, ballRotation, new Vector2(ballWidth / 2, ballHeight / 2), 1f, SpriteEffects.None, 1);
        }

        public void Launch(float posX, float posY, Vector2 velocity)
        {
            if(isVisible == true) { return; }
            PlaySound(gameContent.startSound);
            isVisible = true;
            this.posX = posX;
            this.posY = posY;
            this.velocity = velocity;
        }

        public bool Move(Wall wall, Paddle paddle)
        {
            if(isVisible == false) { return false; }
            posX += velocity.X;
            posY += velocity.Y;

            if(posX < 1)
            {
                posX = 1;
                velocity.X *= -1;
                PlaySound(gameContent.wallBounceSound);
            }

            if(posX > screenWidth - ballWidth + 5)
            {
                posX = screenWidth - ballWidth + 5;
                velocity.X *= -1;
                PlaySound(gameContent.wallBounceSound);
            }

            if (posY < 1)
            {
                posY = 1;
                velocity.Y *= -1;
                PlaySound(gameContent.wallBounceSound);
            }

            if (posY > screenHeight)
            {
                isVisible = false;
                posY = 0;
                PlaySound(gameContent.missSound);
                return false;
            }

            Rectangle paddleRect = new Rectangle((int)paddle.posX, (int)paddle.posY, (int)paddle.paddleWidth, (int)paddle.paddleHeight);
            Rectangle ballRect = new Rectangle((int)posX, (int)posY, (int)ballWidth, (int)ballHeight);
            if(HitTest(paddleRect, ballRect))
            {
                PlaySound(gameContent.paddleBounceSound);
                int offset = Convert.ToInt32(paddle.paddleWidth - (paddle.posX + paddle.paddleWidth - posX + ballWidth / 2));
                offset /= 5;
                if(offset < 0) { offset = 0; }

                switch(offset)
                {
                    case 0:
                        velocity.X = -6;
                        break;
                    case 1:
                        velocity.X = -5;
                        break;
                    case 2:
                        velocity.X = -4;
                        break;
                    case 3:
                        velocity.X = -3;
                        break;
                    case 4:
                        velocity.X = -2;
                        break;
                    case 5:
                        velocity.X = -1;
                        break;
                    case 6:
                        velocity.X = 1;
                        break;
                    case 7:
                        velocity.X = 2;
                        break;
                    case 8:
                        velocity.X = 3;
                        break;
                    case 9:
                        velocity.X = 4;
                        break;
                    case 10:
                        velocity.X = 5;
                        break;
                    default:
                        velocity.X = 6;
                        break;
                }

                velocity.Y *= -1;
                posY = paddle.posY - ballHeight + 1;
                return true;
            }
            bool hitBrick = false;
            for (int i = 0; i < 7; i++)
            {
                if(hitBrick == false)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        Brick brick = wall.brickWall[i, j];
                        if(brick.isVisible)
                        {
                            Rectangle brickRect = new Rectangle((int)brick.posX, (int)brick.posY, (int)brick.brickWidth, (int)brick.brickHeight);
                            if(HitTest(ballRect, brickRect))
                            {
                                PlaySound(gameContent.brickSound);
                                brick.isVisible = false;
                                score += 7 - i;
                                velocity.Y *= -1;
                                bricksCleared++;
                                hitBrick = true;
                                break;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public static bool HitTest(Rectangle r1, Rectangle r2)
        {
            if(Rectangle.Intersect(r1, r2) != Rectangle.Empty) { return true; }
            else { return false; }
        }

        public static void PlaySound(SoundEffect sound)
        {
            float volume = 1;
            float pitch = 0.0f;
            float pan = 0.0f;
            sound.Play(volume, pitch, pan);
        }
    }
}
