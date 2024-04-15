using BreakoutC3172.Objects;
using BreakoutC3172.Objects.Blocks;

namespace BreakoutC3172.ScenesFolder
{
    internal class SceneRoom1 : Scene
    {

        private Texture2D bg;
        private Texture2D ball;
        private Texture2D board;
        private Texture2D blockDirt1;
        private Texture2D blockStone1;
        private Texture2D blockMetal2;
        private Texture2D breakTexture;

        public static readonly int[,] tiles =
        {
            {1, 1, 2, 3, 3, 2, 1, 1, 0, 0, 1, 1, 0, 0, 1},
            {1, 1, 2, 2, 2, 2, 1, 1, 0, 0, 1, 1, 0, 0, 1},
            {0, 0, 1, 2, 2, 1, 0, 0, 0, 0, 1, 1, 0, 0, 1},
            {1, 0, 0, 1, 1, 0, 0, 1, 0, 0, 1, 1, 0, 0, 0},
            {0, 0, 3, 3, 3, 0, 0, 0, 0, 0, 0, 3, 0, 1, 3},
            {0, 0, 1, 0, 3, 0, 0, 0, 0, 2, 0, 3, 0, 0, 2}
        };

        public SceneRoom1(GameManager gameManager) : base(gameManager)
        {

        }

        protected override void Load()
        {

            base.Load();

            bg = Globals.Content.Load<Texture2D>("Backgrounds/bg_arena_1");

            ball = Globals.Content.Load<Texture2D>("ball_2");
            board = Globals.Content.Load<Texture2D>("board");
            blockDirt1 = Globals.Content.Load<Texture2D>("block_dirt_1");
            blockStone1 = Globals.Content.Load<Texture2D>("block_stone_1");
            blockMetal2 = Globals.Content.Load<Texture2D>("block_metal_2");
            breakTexture = Globals.Content.Load<Texture2D>("break");
        }


        public override void Activate()
        {
            gameObjects.Clear();

            gameObjects.Add(new Board(new() { board }, new Vector2(15 * 32 / 2, 10 * 32), 1, 400));

            float radius = 11f;
            Vector2 direction = UtilityFunctions.ConvertRadiansToVector2((float)(Globals.RandomGenerator.NextDouble() * (Math.PI * 2)));
            float speed = 120f;
            gameObjects.Add(new Ball(new() { ball }, new(400, 450), 1, radius, direction, speed));
            gameObjects.Add(new Ball(new() { ball }, new(200, 450), 1, radius, direction, speed));

            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    if (tiles[x, y] == 0) continue;
                    var posX = y * 32 + 16;
                    var posY = x * 32 + 16;

                    if (tiles[x, y] == 1)
                    {
                        var texture = blockDirt1;
                        var hp = 1;
                        gameObjects.Add(new BlockBasic(new() { texture, breakTexture }, new(posX, posY), 1, hp));
                    }
                    else if (tiles[x, y] == 2)
                    {
                        var texture = blockStone1;
                        var hp = 2;
                        gameObjects.Add(new BlockBasic(new() { texture, breakTexture }, new(posX, posY), 1, hp));
                    }
                    else if (tiles[x, y] == 3)
                    {
                        var texture = blockMetal2;
                        var hp = 3;
                        gameObjects.Add(new BlockBasic(new() { texture, breakTexture }, new(posX, posY), 1, hp));
                    }
                }
            }
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
        }
    }
}
