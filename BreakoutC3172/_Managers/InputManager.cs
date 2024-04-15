namespace BreakoutC3172
{
    public static class InputManager
    {
        private static KeyboardState _lastKeyboard;
        private static KeyboardState _currentKeyboard;
        private static MouseState _oldMouse;
        public static bool LeftClicked { get; private set; }
        public static bool RightClicked { get; private set; }
        public static bool LeftDown { get; private set; }
        public static bool RightDown { get; private set; }
        public static Point MousePosition => Mouse.GetState().Position;

        public static bool KeyClicked(Keys key)
        {
            return _currentKeyboard.IsKeyDown(key) && _lastKeyboard.IsKeyUp(key);
        }

        public static bool KeyDown(Keys key)
        {
            return _currentKeyboard.IsKeyDown(key);
        }

        public static void Update()
        {
            _lastKeyboard = _currentKeyboard;
            _currentKeyboard = Keyboard.GetState();

            var mouseState = Mouse.GetState();
            LeftClicked = mouseState.LeftButton == ButtonState.Pressed && _oldMouse.LeftButton == ButtonState.Released;
            RightClicked = mouseState.RightButton == ButtonState.Pressed && _oldMouse.RightButton == ButtonState.Released;

            LeftDown = mouseState.LeftButton == ButtonState.Pressed;
            RightDown = mouseState.RightButton == ButtonState.Pressed;

            _oldMouse = mouseState;
        }
    }
}
