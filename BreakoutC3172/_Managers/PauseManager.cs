using BreakoutC3172.SystemsCore;

namespace BreakoutC3172._Managers
{
    public class PauseManager
    {
        private SpriteFont uiFont;

        public PauseManager()
        {
            // Load content for the pause menu
            uiFont = Globals.Content.Load<SpriteFont>("ui_font");
        }

        public void Update()
        {

        }

        public void Draw()
        {
            Globals.SpriteBatch.Draw(Globals.PixelTexture, new Rectangle(0, 0, Globals.WindowSize.X, Globals.WindowSize.Y), Color.Black * 0.5f);

            UtilityFunctions.DrawStringCentered(Globals.SpriteBatch, uiFont, "The Game Paused", new(Globals.WindowSize.X / 2, Globals.WindowSize.Y / 2), Color.White);
        }
    }
}
