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
        private Game Game;

        public PauseManager(SceneManager sceneManager, Game game)
        {
            // Load content for the pause menu
            uiFont = Globals.Content.Load<SpriteFont>("ui_font");
            UIManager = new UIManager();
            SceneManager = sceneManager;
            Game = game;

            var h = 35;
            var w = 110;

            var button = UIManager.AddButton(new(Globals.WindowSize.X / 2, Globals.WindowSize.Y * 0.3f), "Continue", 1, w, h);
            button.OnClick += ActionContinue;

            button = UIManager.AddButton(new(Globals.WindowSize.X / 2, Globals.WindowSize.Y * 0.45f), "Fullscreen", 1, w, h);
            button.OnClick += ActionF;

            button = UIManager.AddButton(new(Globals.WindowSize.X / 2, Globals.WindowSize.Y * 0.6f), "Toggle Window Size", 1, w + 60, h);
            button.OnClick += ActionW;

            button = UIManager.AddButton(new(Globals.WindowSize.X / 2, Globals.WindowSize.Y * 0.75f), "Main Menu", 1, w, h);
            button.OnClick += ActionMain;
        }

        public void Update()
        {
            UIManager.Update();
        }

        public void Draw()
        {
            Globals.SpriteBatch.Draw(Globals.PixelTexture, new Rectangle(0, 0, Globals.WindowSize.X, Globals.WindowSize.Y), Color.Black * 0.92f);

            UtilityFunctions.DrawStringCentered(Globals.SpriteBatch, uiFont, " ~ The Game Paused ~ ", new(Globals.WindowSize.X * 0.5f, Globals.WindowSize.Y * 0.15f), Color.White);

            UIManager.Draw();
        }





        public void ActionContinue(object sender, EventArgs e)
        {
            Globals.Paused = false;
        }

        public void ActionF(object sender, EventArgs e)
        {
            if (!Game1._graphics.IsFullScreen) { UtilityFunctions.SetWindowState(Game, UtilityFunctions.WindowState.FullScreen); }
            else { UtilityFunctions.SetWindowState(Game, UtilityFunctions.WindowState.Windowed); }
        }

        public void ActionW(object sender, EventArgs e)
        {
            UtilityFunctions.ToggleScreenScale(Game);
        }

        public void ActionMain(object sender, EventArgs e)
        {
            SceneManager.SwitchScene(Scenes.SceneMenu1);
            Globals.Paused = false;
        }


    }
}
