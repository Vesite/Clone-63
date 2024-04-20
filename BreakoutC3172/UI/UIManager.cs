using BreakoutC3172.SystemsCore;

namespace BreakoutC3172.UI
{
    internal class UIManager
    {
        private Texture2D button_c4_mouseover;
        private Texture2D button_c4_normal;
        private Texture2D button_c4_pressed;
        private SpriteFont UIFont { get; }

        private readonly List<UIButton> buttons = new();

        public UIManager()
        {
            button_c4_mouseover = Globals.Content.Load<Texture2D>("button_c4_mouseover");
            button_c4_normal = Globals.Content.Load<Texture2D>("button_c4_normal");
            button_c4_pressed = Globals.Content.Load<Texture2D>("button_c4_pressed");
            UIFont = Globals.Content.Load<SpriteFont>("ui_font");
        }

        public UIButton AddButton(Vector2 position, string text, int scale, int width, int height)
        {
            List<Texture2D> textureList = new() { button_c4_normal, button_c4_mouseover, button_c4_pressed };
            UIButton b = new(textureList, position, text, UIFont, scale, width, height);
            buttons.Add(b);

            return b;
        }

        public void Update()
        {
            foreach (var item in buttons)
            {
                item.Update();
            }
        }

        public void Draw()
        {
            foreach (var item in buttons)
            {
                item.Draw();
            }
        }
    }
}
