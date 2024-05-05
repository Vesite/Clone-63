using BreakoutC3172.SystemsCore;
using BreakoutC3172.UI;
using Microsoft.Xna.Framework.Audio;

namespace BreakoutC3172.ScenesFolder
{
    internal class SceneMenu1 : Scene
    {

        private Texture2D bg;
        private UIManager UIManager;

        private SoundEffect effect;

        public SceneMenu1(GameManager gameManager, SceneManager sceneManager) : base(gameManager, sceneManager)
        {

        }

        protected override void Load()
        {
            base.Load();

            bg = Globals.Content.Load<Texture2D>("Backgrounds/bg_arena_1");
            effect = Globals.Content.Load<SoundEffect>("Audio/SoundEffects/so_sfx_sweep");

        }

        public override void Activate()
        {
            gameObjects.Clear();

            UIManager = new UIManager();

            var h = 35;
            var w = 110;

            var button = UIManager.AddButton(new(Globals.WindowSize.X / 2, Globals.WindowSize.Y * 0.2f), "Stage 1", 1, w, h);
            button.OnClick += Action1;

            button = UIManager.AddButton(new(Globals.WindowSize.X / 2, Globals.WindowSize.Y * 0.4f), "Stage 2", 1, w, h);
            button.OnClick += Action2;

            button = UIManager.AddButton(new(Globals.WindowSize.X / 2, Globals.WindowSize.Y * 0.6f), "Stage 3", 1, w, h);
            button.OnClick += Action3;

            button = UIManager.AddButton(new(Globals.WindowSize.X / 2, Globals.WindowSize.Y * 0.8f), "Exit Game", 1, w, h);
            button.OnClick += ActionExit;

            effect.Play();

        }

        public override void Update()
        {
            base.Update();
            UIManager.Update();
        }

        protected override void Draw()
        {
            Globals.SpriteBatch.Draw(bg, new Vector2(0, 0), null, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            base.Draw();
            UIManager.Draw();
        }


        public void Action1(object sender, EventArgs e)
        {
            sceneManager.SwitchScene(Scenes.SceneRoom1);
        }
        public void Action2(object sender, EventArgs e)
        {
            sceneManager.SwitchScene(Scenes.SceneRoom2);
        }
        public void Action3(object sender, EventArgs e)
        {
            sceneManager.SwitchScene(Scenes.SceneRoom3);
        }
        public void ActionExit(object sender, EventArgs e)
        {
            gameManager.Exit();
        }

    }
}
