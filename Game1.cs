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
        private int screenWidth = 502;
        private int screenHeight = 700;

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
            if(inp.left_down) { /*SERVE BALL*/ }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            paddle.Draw();
            wall.Draw();
            gameBorder.Draw();
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
