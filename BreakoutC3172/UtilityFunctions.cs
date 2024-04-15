namespace BreakoutC3172
{
    internal class UtilityFunctions
    {

        public static RenderTarget2D GetNewRenderTarget()
        {
            return new RenderTarget2D(Globals.GraphicsDevice, Globals.WindowSize.X, Globals.WindowSize.Y);
        }

        public static void DrawOutline(Rectangle rectangle)
        {
            var col = Color.Yellow;
            // Draw top line
            Globals.SpriteBatch.Draw(Globals.PixelTexture, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, 1), col);
            // Draw bottom line
            Globals.SpriteBatch.Draw(Globals.PixelTexture, new Rectangle(rectangle.X, rectangle.Y + rectangle.Height - 1, rectangle.Width, 1), col);
            // Draw left line
            Globals.SpriteBatch.Draw(Globals.PixelTexture, new Rectangle(rectangle.X, rectangle.Y, 1, rectangle.Height), col);
            // Draw right line
            Globals.SpriteBatch.Draw(Globals.PixelTexture, new Rectangle(rectangle.X + rectangle.Width - 1, rectangle.Y, 1, rectangle.Height), col);
        }

        public static void DrawStringCentered(SpriteBatch spriteBatch, SpriteFont spriteFont, string text, Vector2 position, Color color)
        {
            // Measure the size of the text
            Vector2 textSize = spriteFont.MeasureString(text);

            Vector2 centeredPosition = position - textSize / 2;

            spriteBatch.DrawString(spriteFont, text, centeredPosition, color);
        }
        public static void DrawAtlasImage(Texture2D texture, float scale, int image_width, Vector2 position, int image_index, float rotation)
        {

            Rectangle sourceRectangle = new(0 + image_index * image_width, 0, image_width, texture.Height);

            Rectangle destinationRectangle = new((int)(position.X - image_width * scale / 2), (int)(position.Y - texture.Height * scale / 2),
                                                 (int)(image_width * scale), (int)(texture.Height * scale));

            // Calculate the origin for rotation (center of the sprite)
            //Vector2 origin = new Vector2(destinationRectangle.Width / 2, destinationRectangle.Height / 2);
            Vector2 origin = new(0, 0);
            Globals.SpriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White, rotation, origin, SpriteEffects.None, 0);

        }

        public static Vector2 ConvertRadiansToVector2(float direction)
        {
            return new Vector2((float)Math.Cos(direction), (float)Math.Sin(direction));
        }

        public static void SetFullscreen(bool setFullscreen)
        {

            Game1._graphics.IsFullScreen = setFullscreen;

            if (Game1._graphics.IsFullScreen == true)
            {
                // Fit the game to the screen using AspectRatio, Black Letterbox
                if (Globals.screen.AspectRatio < Globals.AspectRatio)
                {
                    // letterbox vertically
                    // Scale game to screen width
                    Globals._gameScale = (float)Globals.screen.Width / Globals.WindowSize.X;
                    // translate vertically
                    Globals._gameOffset.Y = (Globals.screen.Height - Globals.WindowSize.Y * Globals._gameScale) / 2f;
                    Globals._gameOffset.X = 0;
                }
                else
                {
                    // letterbox horizontally
                    // Scale game to screen height 
                    Globals._gameScale = (float)Globals.screen.Height / Globals.WindowSize.Y;
                    // translate horizontally
                    Globals._gameOffset.X = (Globals.screen.Width - Globals.WindowSize.X * Globals._gameScale) / 2f;
                    Globals._gameOffset.Y = 0;
                }
            }
            else
            {
                Globals._gameScale = Globals._gameScalePrefered;
            }

            SetWindowSize();
        }

        // Will only work with no fullscreen
        public static void ToggleScreenScale()
        {
            if (Game1._graphics.IsFullScreen == false)
            {
                Globals._gameScalePrefered += 1;
                if (Globals._gameScalePrefered > Globals._gameScaleMax)
                {
                    Globals._gameScalePrefered = 1;
                }

                Globals._gameScale = Globals._gameScalePrefered;
            }

            SetWindowSize();
        }

        private static void SetWindowSize()
        {
            // Sets game window size in pixels
            Game1._graphics.PreferredBackBufferWidth = (int)(Globals.WindowSize.X * Globals._gameScale);
            Game1._graphics.PreferredBackBufferHeight = (int)(Globals.WindowSize.Y * Globals._gameScale);
            Game1._graphics.ApplyChanges();
        }

    }
}
