using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Brickbreaker
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private GameContent gameContent;

        private Paddle paddle;
        private Wall wall;
        private GameBorder gameBorder;
        private Ball ball;
        private Ball staticBall;
        private int screenWidth = 502;
        private int screenHeight = 700;
        private bool readyToServeBall = true;
        private int ballsRemaining = 3;

        Input inp;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.ApplyChanges();

            inp = new Input(screenWidth, screenHeight);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            gameContent = new GameContent(Content);

            int paddleX = (screenWidth - gameContent.imgPaddle.Width) / 2;
            int paddleY = screenHeight - 100;
            paddle = new Paddle(paddleX, paddleY, screenWidth, spriteBatch, gameContent);
            wall = new Wall(1, 50, spriteBatch, gameContent);
            gameBorder = new GameBorder(screenWidth, screenHeight, spriteBatch, gameContent);
            ball = new Ball(screenWidth, screenHeight, spriteBatch, gameContent);
            staticBall = new Ball(screenWidth, screenHeight, spriteBatch, gameContent);
            staticBall.posX = 25;
            staticBall.posY = 25;
            staticBall.isVisible = true;
            staticBall.useRotation = false;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //if (IsActive) { return; }

            // TODO: Add your update logic here

            inp.Update();
            if (inp.KeyDown(Keys.Left)) { paddle.MoveLeft(5f); }
            if (inp.KeyDown(Keys.Right)) { paddle.MoveRight(5f); }
            if(inp.KeyDown(Keys.Up)) { ServeBall(); }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            // -----------------------------------------
            paddle.Draw();
            wall.Draw();
            gameBorder.Draw();
            if (ball.isVisible)
            {
                bool inPlay = ball.Move(wall, paddle);
                if (inPlay)
                {
                    ball.Draw();
                }
                else
                {
                    ballsRemaining--;
                    readyToServeBall = true;
                }
            }

            staticBall.Draw();

            string scoreMsg = "Score: " + ball.score.ToString("00000");
            Vector2 space = gameContent.labelFont.MeasureString(scoreMsg);
            spriteBatch.DrawString(gameContent.labelFont, scoreMsg, new Vector2((screenWidth - space.X) / 2, screenHeight - 40), Color.White);
            if(ball.bricksCleared >= 70)
            {
                ball.isVisible = false;
                ball.bricksCleared = 0;
                wall = new Wall(1, 50, spriteBatch, gameContent);
                readyToServeBall = true;
            }
            if(readyToServeBall)
            {
                if(ballsRemaining > 0)
                {
                    string startMsg = "Press the Up arrow to Start";
                    Vector2 startSpace = gameContent.labelFont.MeasureString(startMsg);
                    spriteBatch.DrawString(gameContent.labelFont, startMsg, new Vector2((screenWidth - startSpace.X) / 2, screenHeight / 2), Color.White);
                }
                else
                {
                    string endMsg = "Game Over";
                    Vector2 endSpace = gameContent.labelFont.MeasureString(endMsg);
                    spriteBatch.DrawString(gameContent.labelFont, endMsg, new Vector2((screenWidth - endSpace.X) / 2, screenHeight / 2), Color.White);
                }
            }
            spriteBatch.DrawString(gameContent.labelFont, ballsRemaining.ToString(), new Vector2(40, 10), Color.White);
            // -----------------------------------------
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void ServeBall()
        {
            if (ballsRemaining < 1)
            {
                ballsRemaining = 3;
                ball.score = 0;
                wall = new Wall(1, 50, spriteBatch, gameContent);
            }
            readyToServeBall = false;
            float ballX = paddle.posX + (paddle.paddleWidth) / 2;
            float ballY = paddle.posY - ball.ballHeight;
            ball.Launch(ballX, ballY, new Vector2(-3, -3));
        }
    }
}
