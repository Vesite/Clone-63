using BreakoutC3172.SystemsCore;

namespace BreakoutC3172.Objects
{
    public class Board : GameObject
    {
        public float acc = 20;
        public float maxSpeed = 6f;
        public Vector2 velocity;

        public Board(List<Texture2D> textures, Vector2 position, float scale) : base(textures, position, scale)
        {

        }

        public override void Update(List<GameObject> gameObjects, List<int> indicesToRemove)
        {

            UpdateVelocity();

            Vector2 nextPosition = Position + velocity;
            var w = Rectangle.Width;
            if (nextPosition.X < 0f + w / 2) // Collide with left wall
            {
                // Move us flush to the left wall and then add counce (flip velocity)?
                Position = new(0f + w / 2, Position.Y);
                velocity = new(0, velocity.Y);
            }
            else if (nextPosition.X > Globals.WindowSize.X - 5 * 32 - w / 2) // Collide with right wall
            {
                Position = new(Globals.WindowSize.X - 5 * 32 - w / 2, Position.Y);
                velocity = new(0, velocity.Y);
            }

            Position = Position + velocity;

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
                if (UtilityFunctions.IsPositive(velocity.X))
                {
                    newAcc = acc * 6;
                }
                else if (velocity.X < -(maxSpeed * 0.5f))
                {
                    // Accelerate less when we are going fast
                    newAcc = acc * 0.3f;
                }

                velocity = new(velocity.X - newAcc * Globals.Time, velocity.Y);

            }
            if (InputManager.KeyDown(Keys.Right))
            {
                var newAcc = acc;
                // If negative speed
                if (!UtilityFunctions.IsPositive(velocity.X))
                {
                    newAcc = acc * 6;
                }
                else if (velocity.X > maxSpeed * 0.5f)
                {
                    // Accelerate less when we are going fast
                    newAcc = acc * 0.3f;
                }

                velocity = new(velocity.X + newAcc * Globals.Time, velocity.Y);

            }

            // Stop when not clicking
            if (!InputManager.KeyDown(Keys.Right) && !InputManager.KeyDown(Keys.Left))
            {
                velocity = new(UtilityFunctions.Approach(velocity.X, 0, acc * Globals.Time), velocity.Y);
            }

            // Make Max speed
            if (velocity.X > maxSpeed)
            {
                velocity.X = maxSpeed;
            }
            if (velocity.X < -maxSpeed)
            {
                velocity.X = -maxSpeed;
            }

        }
    }
}
