using Microsoft.Xna.Framework.Content;

namespace BreakoutC3172
{
    public static class Globals
    {
        // Ect
        public static float Time { get; private set; }
        public static ContentManager Content { get; set; }
        public static SpriteBatch SpriteBatch { get; set; }
        public static GraphicsDevice GraphicsDevice { get; set; }
        public static Random RandomGenerator { get; } = new Random();

        // Screen Window Stuff
        public static float AspectRatio = (16f / 9f);
        public static Point WindowSize { get; } = new(640, (int)(640 / AspectRatio));
        public static float _gameScale;
        public static float _gameScaleMax = 1f;
        public static float _gameScalePrefered = 1f;
        public static Vector2 _gameOffset;
        public static DisplayMode screen = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;

        // Global Textures
        public static Texture2D PixelTexture { get; private set; }

        // Options
        public static bool IsDrawingOutline { get; set; } = false;


        public static void Load()
        {
            PixelTexture = new Texture2D(Globals.GraphicsDevice, 1, 1);
            PixelTexture.SetData(new Color[] { Color.White });
        }

        public static void Update(GameTime gt)
        {
            Time = (float)gt.ElapsedGameTime.TotalSeconds;
        }
    }
}
