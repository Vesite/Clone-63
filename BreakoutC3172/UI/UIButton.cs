using BreakoutC3172.SystemsCore;

namespace BreakoutC3172.UI
{
    internal class UIButton
    {
        protected List<Texture2D> textures;
        public Vector2 Position { get; set; }
        private SpriteFont font { get; }

        private int imageIndex = 0;
        private int scale;
        private int width;
        private int height;

        private string text;
        private bool buttonPressedDown = false;
        private int textYOffset = 0;

        private Rectangle[] sourcePatches;
        private Rectangle[] destinationPatches;

        public Rectangle Rectangle => new((int)(Position.X - width * scale / 2), (int)(Position.Y - height * scale / 2), (int)(width * scale), (int)(height * scale));


        public UIButton(List<Texture2D> texture, Vector2 position, string text, SpriteFont font, int scale, int width, int height)
        {
            this.textures = texture;
            Position = position;
            this.text = text;
            this.font = font;
            this.scale = scale;
            this.width = width;
            this.height = height;

            // This is the parts of the sprite we are "taking" to draw 9slice
            Rectangle spriteRectangle = new Rectangle(0, 0, textures[imageIndex].Width, textures[imageIndex].Height);
            sourcePatches = CreatePatches(spriteRectangle, 9, 9, 6, 8, 1);

            // This are the areas in the game where we are drawing
            destinationPatches = CreatePatches(Rectangle, 9, 9, 6, 8, scale);

        }

        public void Update()
        {
            if (InputManager.MouseRectangle.Intersects(Rectangle))
            {
                imageIndex = 1;
                if (InputManager.LeftClicked)
                {
                    buttonPressedDown = true;
                    Click();
                }

                if (!InputManager.LeftDown)
                {
                    buttonPressedDown = false;
                }
            }
            else
            {
                buttonPressedDown = false;
                imageIndex = 0;
            }

            if (buttonPressedDown)
            {
                imageIndex = 2;
                textYOffset = 1 * scale;
            }
            else
            {
                textYOffset = 0;
            }
        }

        public event EventHandler OnClick;

        private void Click()
        {
            OnClick?.Invoke(this, EventArgs.Empty);
        }

        public void Draw()
        {

            for (var i = 0; i < 9; i++)
            {
                Globals.SpriteBatch.Draw(textures[imageIndex], destinationPatches[i],
                    sourcePatches[i], Color.White);
            }

            UtilityFunctions.DrawStringCentered(Globals.SpriteBatch, font, text, new(Position.X, Position.Y + textYOffset), Color.White);

            if (Globals.IsDrawingOutline)
            {
                UtilityFunctions.DrawOutline(Rectangle);
            }

        }

        private Rectangle[] CreatePatches(Rectangle rectangle, int leftPadding, int rightPadding, int topPadding, int bottomPadding, int scale)
        {

            leftPadding = leftPadding * scale;
            rightPadding = rightPadding * scale;
            topPadding = topPadding * scale;
            bottomPadding = bottomPadding * scale;

            var x = rectangle.X;
            var y = rectangle.Y;
            var w = rectangle.Width;
            var h = rectangle.Height;
            var middleWidth = w - leftPadding - rightPadding;
            var middleHeight = h - topPadding - bottomPadding;
            var bottomY = y + h - bottomPadding;
            var rightX = x + w - rightPadding;
            var leftX = x + leftPadding;
            var topY = y + topPadding;
            var patches = new[]
            {
                new Rectangle(x,      y,        leftPadding,  topPadding),      // top left
                new Rectangle(leftX,  y,        middleWidth,  topPadding),      // top middle
                new Rectangle(rightX, y,        rightPadding, topPadding),      // top right
                new Rectangle(x,      topY,     leftPadding,  middleHeight),    // left middle
                new Rectangle(leftX,  topY,     middleWidth,  middleHeight),    // middle
                new Rectangle(rightX, topY,     rightPadding, middleHeight),    // right middle
                new Rectangle(x,      bottomY,  leftPadding,  bottomPadding),   // bottom left
                new Rectangle(leftX,  bottomY,  middleWidth,  bottomPadding),   // bottom middle
                new Rectangle(rightX, bottomY,  rightPadding, bottomPadding)    // bottom right
            };
            return patches;
        }

    }
}
