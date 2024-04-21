using BreakoutC3172.Objects;
using BreakoutC3172.SystemsCore;

namespace BreakoutC3172.ScenesFolder
{
    internal class SceneRoom2 : Scene
    {

        private Texture2D bg;
        private Texture2D ball;
        private Texture2D board;
        private Texture2D blockDirt;
        private Texture2D blockStone;
        private Texture2D blockMetal;
        private Texture2D breakTexture;

        private Texture2D ui_overlay;

        public static readonly int[,] tiles =
        {
            {2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
            {2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
            {2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
            {2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
        };

        public SceneRoom2(GameManager gameManager, SceneManager sceneManager) : base(gameManager, sceneManager)
        {

        }

        protected override void Load()
        {
            base.Load();

            bg = Globals.Content.Load<Texture2D>("Backgrounds/bg_beach_1");
            ball = Globals.Content.Load<Texture2D>("ball_2");
            board = Globals.Content.Load<Texture2D>("board");
            blockDirt = Globals.Content.Load<Texture2D>("block_dirt_1");
            blockStone = Globals.Content.Load<Texture2D>("block_stone_1");
            blockMetal = Globals.Content.Load<Texture2D>("block_metal_2");
            breakTexture = Globals.Content.Load<Texture2D>("break");

            ui_overlay = Globals.Content.Load<Texture2D>("ui_left");
        }

        public override void Activate()
        {
            gameObjects.Clear();

            gameObjects.Add(new Board(new() { board }, new Vector2(15 * 32 / 2, 10 * 32), 1));

            float radius = 11f;
            Vector2 direction = UtilityFunctions.ConvertRadiansToHeadingVector((float)(Globals.RandomGenerator.NextDouble() * (Math.PI * 2)));
            float speed = 300f;
            gameObjects.Add(new Ball(new() { ball }, new(400, 450), 1, radius, direction, speed));

            SpawnBlocks(tiles, blockDirt, blockStone, blockMetal, breakTexture);
        }

        public override void Update()
        {
            base.Update();
        }

        protected override void Draw()
        {
            // Draw Background
            Globals.SpriteBatch.Draw(bg, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), 0.75f, SpriteEffects.None, 0f);

            base.Draw();

            // Draw UI Here?
            Globals.SpriteBatch.Draw(ui_overlay, new Vector2(0, 0), null, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
        }
    }
}
