namespace BreakoutC3172.ScenesFolder
{
    internal class SceneRoom2 : Scene
    {

        private Texture2D bg;

        public SceneRoom2(GameManager gameManager) : base(gameManager)
        {

        }

        protected override void Load()
        {
            bg = Globals.Content.Load<Texture2D>("Backgrounds/bg_beach_1");
            base.Load();
        }

        public override void Activate()
        {
            gameObjects.Clear();

            var hero = Globals.Content.Load<Texture2D>("hero");
            gameObjects.Add(new Objects.Character(new() { hero }, new(10, 10), 1, 300));
            gameObjects.Add(new Objects.Character(new() { hero }, new(640 - 20, 360 - 20), 1, 300));
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
