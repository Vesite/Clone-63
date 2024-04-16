using BreakoutC3172.SystemsCore;

namespace BreakoutC3172.ScenesFolder
{
    internal class SceneMenu1 : Scene
    {

        private Texture2D bg;

        public SceneMenu1(GameManager gameManager) : base(gameManager)
        {

        }

        protected override void Load()
        {
            base.Load();

            bg = Globals.Content.Load<Texture2D>("Backgrounds/bg_arena_1");

        }


        public override void Activate()
        {
            gameObjects.Clear();
        }

        public override void Update()
        {
            base.Update();
        }

        protected override void Draw()
        {
            Globals.SpriteBatch.Draw(bg, new Vector2(0, 0), null, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            base.Draw();
        }
    }
}
