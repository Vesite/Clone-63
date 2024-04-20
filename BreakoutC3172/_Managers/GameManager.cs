using BreakoutC3172._Managers;
using BreakoutC3172.ScenesFolder;
using BreakoutC3172.SystemsCore;
using System.Diagnostics;

namespace BreakoutC3172
{
    public class GameManager
    {

        private readonly SceneManager _sceneManager;
        private PauseManager _pauseManager;
        private readonly Game _game;

        public GameManager(Game game)
        {
            _game = game;
            _sceneManager = new(this);
            _pauseManager = new(_sceneManager);

            #region DEBUG TESTING

            var radians = (float)Math.PI;
            var unitVec = UtilityFunctions.ConvertRadiansToUnitVector(radians);
            var radians2 = UtilityFunctions.ConvertUnitVectorToRadians(unitVec);
            Debug.WriteLine(radians);
            Debug.WriteLine(unitVec);
            Debug.WriteLine(radians2);
            //Debug.WriteLine(UtilityFunctions.IsPositive(2));
            //Debug.WriteLine(UtilityFunctions.IsPositive(-2));

            #endregion 
        }

        public void Update()
        {

            // Pause
            if (InputManager.KeyClicked(Keys.Escape)) { Globals.Paused = !Globals.Paused; }

            _pauseManager.Update();

            // Fullscreen
            if (InputManager.KeyClicked(Keys.F5))
            {
                if (!Game1._graphics.IsFullScreen) { UtilityFunctions.SetWindowState(_game, UtilityFunctions.WindowState.FullScreen); }
                else { UtilityFunctions.SetWindowState(_game, UtilityFunctions.WindowState.Windowed); }
            }
            // Borderless
            //if (InputManager.KeyClicked(Keys.F6))
            //{
            //    if (!_game.Window.IsBorderless) { UtilityFunctions.SetWindowState(_game, UtilityFunctions.WindowState.Borderless); }
            //    else { UtilityFunctions.SetWindowState(_game, UtilityFunctions.WindowState.Windowed); }
            //}

            // Toggle Size
            if (InputManager.KeyClicked(Keys.F8)) { UtilityFunctions.ToggleScreenScale(_game); }

            if (InputManager.KeyClicked(Keys.H)) { Globals.IsDrawingOutline = !Globals.IsDrawingOutline; }


            if (!Globals.Paused)
            {
                // Swap Between Scenes
                if (InputManager.KeyClicked(Keys.F1)) { _sceneManager.SwitchScene(Scenes.SceneMenu1); }
                if (InputManager.KeyClicked(Keys.F2)) { _sceneManager.SwitchScene(Scenes.SceneRoom1); }
                if (InputManager.KeyClicked(Keys.F3)) { _sceneManager.SwitchScene(Scenes.SceneRoom2); }

                // Update Current Scene
                _sceneManager.Update();
            }

        }

        public void Draw(Matrix transform)
        {
            // So while getting the frame for each scene it uses "begin" and "end"
            var frame = _sceneManager.GetFrame();

            Globals.SpriteBatch.Begin(transformMatrix: transform, samplerState: SamplerState.PointClamp);

            // Draw In-Game
            Globals.SpriteBatch.Draw(frame, Vector2.Zero, Color.White);

            if (Globals.Paused)
            {
                _pauseManager.Draw();
            }

            Globals.SpriteBatch.End();
        }

        public void Exit()
        {
            _game.Exit();
        }
    }
}
