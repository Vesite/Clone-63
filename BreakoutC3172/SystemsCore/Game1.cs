namespace BreakoutC3172.SystemsCore
{
    public class Game1 : Game
    {
        public static GraphicsDeviceManager _graphics;
        private GameManager _gameManager;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            // When the game starts i set "_gameScalePrefered" to be as big whole number as possible
            // without being the same size or bigger than the monitor
            int maxWidthScale = 1;
            while (maxWidthScale * Globals.WindowSize.X < Globals.screen.Width)
            {
                maxWidthScale += 1;
            }
            maxWidthScale -= 1;
            int maxHeightScale = 1;
            while (maxHeightScale * Globals.WindowSize.Y < Globals.screen.Height)
            {
                maxHeightScale += 1;
            }
            maxHeightScale -= 1;
            Globals._gameScaleMax = Math.Max(maxHeightScale, maxWidthScale);
            Globals._gameScalePrefered = Globals._gameScaleMax;

            UtilityFunctions.SetFullscreen(false);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Globals.Content = Content;
            Globals.GraphicsDevice = GraphicsDevice;
            Globals.SpriteBatch = new SpriteBatch(GraphicsDevice);

            Globals.Load();

            _gameManager = new();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Globals.Update(gameTime);
            InputManager.Update();
            _gameManager.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Globals.GraphicsDevice.Clear(Color.Black);

            // Determine the necessary transform to scale and position game on-screen
            Matrix transform = Matrix.CreateScale(Globals._gameScale) * // Scale the game to screen size 
                               Matrix.CreateTranslation(Globals._gameOffset.X, Globals._gameOffset.Y, 0); // Translate game to letterbox position

            _gameManager.Draw(transform);

            base.Draw(gameTime);
        }
    }
}
