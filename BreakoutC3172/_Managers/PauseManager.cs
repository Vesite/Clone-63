using BreakoutC3172.ScenesFolder;
using BreakoutC3172.SystemsCore;
using BreakoutC3172.UI;

namespace BreakoutC3172._Managers
{
    internal class PauseManager
    {
        private SpriteFont uiFont;
        private UIManager UIManager;

        // Input Argument
        private SceneManager SceneManager;

        public PauseManager(SceneManager sceneManager)
        {
            // Load content for the pause menu
            uiFont = Globals.Content.Load<SpriteFont>("ui_font");
            UIManager = new UIManager();
            SceneManager = sceneManager;

            var h = 35;
            var w = 110;

            var button = UIManager.AddButton(new(Globals.WindowSize.X / 2, Globals.WindowSize.Y * 0.5f), "Continue", 1, w, h);
            button.OnClick += ActionContinue;

            button = UIManager.AddButton(new(Globals.WindowSize.X / 2, Globals.WindowSize.Y * 0.7f), "Main Menu", 1, w, h);
            button.OnClick += ActionMain;
        }

        public void Update()
        {
            UIManager.Update();
        }

        public void Draw()
        {
            Globals.SpriteBatch.Draw(Globals.PixelTexture, new Rectangle(0, 0, Globals.WindowSize.X, Globals.WindowSize.Y), Color.Black * 0.8f);

            UtilityFunctions.DrawStringCentered(Globals.SpriteBatch, uiFont, "The Game Paused", new(Globals.WindowSize.X * 0.5f, Globals.WindowSize.Y * 0.25f), Color.White);

            UIManager.Draw();
        }

        public void ActionContinue(object sender, EventArgs e)
        {
            Globals.Paused = false;
        }
        public void ActionMain(object sender, EventArgs e)
        {
            SceneManager.SwitchScene(Scenes.SceneMenu1);
            Globals.Paused = false;
        }


    }
}
