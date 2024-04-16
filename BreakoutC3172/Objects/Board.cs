using BreakoutC3172.SystemsCore;

namespace BreakoutC3172.Objects
{
    internal class Board : GameObject
    {
        private float acc = 12;
        private float max_speed = 3.5f;
        private Vector2 velocity;

        public Board(List<Texture2D> textures, Vector2 position, float scale) : base(textures, position, scale)
        {

        }

        public override void Update(List<GameObject> gameObjects, List<int> indicesToRemove)
        {

            UpdateVelocity();

            Position = new(Position.X + velocity.X, Position.Y + velocity.Y);

            //if (InputManager.KeyDown(Keys.Down))
            //{
            //    Position = new(Position.X, Position.Y + speed * Globals.Time);
            //}
            //if (InputManager.KeyDown(Keys.Up))
            //{
            //    Position = new(Position.X, Position.Y - speed * Globals.Time);
            //}

        }

        public override void Draw()
        {
            base.Draw();
        }

        private void UpdateVelocity()
        {
            if (InputManager.KeyDown(Keys.Left))
            {
                var newAcc = acc;
                // If positive speed
                if (velocity.X > 0)
                {
                    //velocity = new(0, velocity.Y);
                    newAcc = acc * 6;
                }
                velocity = new(velocity.X - newAcc * Globals.Time, velocity.Y);
            }
            if (InputManager.KeyDown(Keys.Right))
            {
                var newAcc = acc;
                // If negative speed
                if (velocity.X < 0)
                {
                    //velocity = new(0, velocity.Y);
                    newAcc = acc * 6;
                }
                velocity = new(velocity.X + newAcc * Globals.Time, velocity.Y);
            }

            // Constant friction to make it possible to accelerate for long with diminishing returns.
            //velocity = new(velocity.X * 0.9f, velocity.Y);

            if (!InputManager.KeyDown(Keys.Right) && !InputManager.KeyDown(Keys.Left))
            {
                // Add resistance
                velocity = new(UtilityFunctions.Approach(velocity.X, 0, acc * Globals.Time), velocity.Y);
            }

            // Make Max speed
            if (velocity.X > max_speed)
            {
                velocity.X = max_speed;
            }
            if (velocity.X < -max_speed)
            {
                velocity.X = -max_speed;
            }

        }
    }
}
