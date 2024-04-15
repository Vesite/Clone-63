using BreakoutC3172.ScenesFolder;

namespace BreakoutC3172
{
    public class GameManager
    {

        private readonly SceneManager _sceneManager;

        public GameManager()
        {
            _sceneManager = new(this);
        }

        public void Update()
        {
            // Swap Between Scenes
            if (InputManager.KeyClicked(Keys.F1)) { _sceneManager.SwitchScene(Scenes.SceneMenu1); }
            if (InputManager.KeyClicked(Keys.F2)) { _sceneManager.SwitchScene(Scenes.SceneRoom1); }
            if (InputManager.KeyClicked(Keys.F3)) { _sceneManager.SwitchScene(Scenes.SceneRoom2); }

            // Screen Settings
            if (InputManager.KeyClicked(Keys.F5)) { UtilityFunctions.SetFullscreen(!Game1._graphics.IsFullScreen); }
            if (InputManager.KeyClicked(Keys.F6)) { UtilityFunctions.ToggleScreenScale(); }

            if (InputManager.KeyClicked(Keys.H)) { Globals.IsDrawingOutline = !Globals.IsDrawingOutline; }

            // Update Current Scene 
            _sceneManager.Update();
        }

        public void Draw(Matrix transform)
        {
            // So while getting the frame for each scene it uses "begin" and "end"
            var frame = _sceneManager.GetFrame();

            Globals.SpriteBatch.Begin(transformMatrix: transform, samplerState: SamplerState.PointClamp);

            Globals.SpriteBatch.Draw(frame, Vector2.Zero, Color.White);

            Globals.SpriteBatch.End();
        }
    }
}
