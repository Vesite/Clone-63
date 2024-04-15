namespace BreakoutC3172.Objects
{
    internal class Character : GameObject
    {
        private readonly int _speed = 300;

        public Character(List<Texture2D> textures, Vector2 position, float scale, int speed) : base(textures, position, scale)
        {
            _speed = speed;
        }

        public override void Update(List<GameObject> gameObjects, List<int> indicesToRemove)
        {
            if (InputManager.KeyDown(Keys.Left))
            {
                Position = new(Position.X - _speed * Globals.Time, Position.Y);
            }
            if (InputManager.KeyDown(Keys.Right))
            {
                Position = new(Position.X + _speed * Globals.Time, Position.Y);
            }
            if (InputManager.KeyDown(Keys.Down))
            {
                Position = new(Position.X, Position.Y + _speed * Globals.Time);
            }
            if (InputManager.KeyDown(Keys.Up))
            {
                Position = new(Position.X, Position.Y - _speed * Globals.Time);
            }
        }

        public override void Draw()
        {
            base.Draw();
        }

    }
}
