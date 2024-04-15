namespace BreakoutC3172.ScenesFolder
{
    internal class SceneMenu1 : Scene
    {

        private Texture2D hero;

        public SceneMenu1(GameManager gameManager) : base(gameManager)
        {

        }

        protected override void Load()
        {
            hero = Globals.Content.Load<Texture2D>("hero");

            base.Load();
        }


        public override void Activate()
        {
            gameObjects.Clear();

            gameObjects.Add(new Objects.Character(new() { hero }, new(10, 10), 1, 300));

        }

        public override void Update()
        {
            base.Update();
        }

        protected override void Draw()
        {
            base.Draw();
        }
    }
}
