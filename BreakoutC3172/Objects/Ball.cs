using BreakoutC3172.Objects.Blocks;
using BreakoutC3172.SystemsCore;
using Microsoft.Xna.Framework.Audio;
using System.Diagnostics;

namespace BreakoutC3172.Objects
{

    // TODO, im trying to add some random change in the direction when colliding
    // But im getting wrong behavior when I think I do it correct, atm it works perfectly but my code don't make sense
    // And I would not expect it to work...

    internal class Ball : GameObject
    {
        public float radius;
        private Vector2 direction;
        private float speed;

        private float rotation;
        private float rotation_speed;
        private Vector2 velocity;

        private SpriteFont UIFont { get; }

        private Random random = new();

        private SoundEffect[] soundsBounce = new SoundEffect[3];

        public Ball(List<Texture2D> textures, Vector2 position, float scale, float radius, Vector2 direction, float speed)
             : base(textures, position, scale)
        {
            this.radius = radius;
            this.direction = direction;
            this.speed = speed;

            rotation = (float)(random.NextDouble() * 2 * Math.PI);
            NewRotationSpeed();

            UIFont = Globals.Content.Load<SpriteFont>("ui_font");
            // Load the sound effects in your LoadContent method or wherever appropriate
            soundsBounce[0] = Globals.Content.Load<SoundEffect>("Audio/SoundEffects/so_sfx_punch_light_1");
            soundsBounce[1] = Globals.Content.Load<SoundEffect>("Audio/SoundEffects/so_sfx_punch_light_2");
            soundsBounce[2] = Globals.Content.Load<SoundEffect>("Audio/SoundEffects/so_sfx_punch_light_3");

        }

        public override void Update(List<GameObject> gameObjects, List<int> indicesToRemove)
        {

            UpdateVelocity();
            UpdatePosition(gameObjects, indicesToRemove);

            // Rotation Stuff
            rotation += rotation_speed * Globals.Time;

            float spriteRadius = textures[0].Width / 2;
            scale = radius / spriteRadius;

        }

        public override void Draw()
        {
            Globals.SpriteBatch.Draw(textures[0], Position, null, Color.White,
                                     rotation, new Vector2(textures[0].Width / 2, textures[0].Height / 2), scale, SpriteEffects.None, 0f);
            if (Globals.IsDrawingOutline)
            {
                UtilityFunctions.DrawOutline(Rectangle);
                // Debug Text
                Globals.SpriteBatch.DrawString(UIFont, UtilityFunctions.VectorToString(direction), Position, Color.White);
            }

        }

        private void NewRotationSpeed()
        {
            rotation_speed = ((float)(random.NextDouble()) - 0.5f) * 1.1f;
        }

        private void UpdateVelocity()
        {
            velocity = direction * speed * Globals.Time;
        }

        private void UpdatePosition(List<GameObject> gameObjects, List<int> indicesToRemove)
        {

            int width = Globals.WindowSize.X;
            int height = Globals.WindowSize.Y;

            Vector2 nextPosition = Position + velocity;
            float clampDelta = 0.2f;
            var colRandomMax = 0.3f;

            if (nextPosition.X < 0f + radius) // Collide with left wall
            {
                UtilityFunctions.PlaySoundArray(soundsBounce, 1);
                nextPosition.X = 0f + radius + 1;

                // Flip the direction and change it a little randomly
                direction.X = -direction.X;
                var radians = UtilityFunctions.ConvertHeadingVectorToRadians(direction);
                if (UtilityFunctions.IsPositive(direction.Y))
                {
                    radians += (float)((Globals.RandomGenerator.NextDouble() * colRandomMax) - colRandomMax * 0.5f);
                }
                else
                {
                    radians += (float)((Globals.RandomGenerator.NextDouble() * colRandomMax) - colRandomMax * 0.5f);
                }
                // Clamp
                radians = Math.Clamp(radians, (float)(-Math.PI / 2) + clampDelta, (float)(Math.PI / 2) - clampDelta);

                direction = UtilityFunctions.ConvertRadiansToHeadingVector(radians);

                NewRotationSpeed();
            }
            else if (nextPosition.X > width - 5 * 32 - radius) // Collide with right wall
            {
                UtilityFunctions.PlaySoundArray(soundsBounce, 1);
                nextPosition.X = width - 5 * 32 - radius - 1;

                // Flip the direction and change it a little randomly
                direction.X = -direction.X;
                var radians = UtilityFunctions.ConvertHeadingVectorToRadians(direction);

                if (UtilityFunctions.IsPositive(direction.Y))
                {
                    radians += (float)((Globals.RandomGenerator.NextDouble() * colRandomMax) - colRandomMax * 0.5f);
                }
                else
                {
                    radians += (float)((Globals.RandomGenerator.NextDouble() * colRandomMax) - colRandomMax * 0.5f);
                }
                // Move Radians to PI to -PI Range
                MathHelper.WrapAngle(radians);

                // Clamp
                var max = (Math.PI / 2) + clampDelta;
                var min = (-Math.PI / 2) - clampDelta;
                if (radians < max && radians > 0) { radians = (float)max; }
                if (radians > min && radians < 0) { radians = (float)min; }

                direction = UtilityFunctions.ConvertRadiansToHeadingVector(radians);

                NewRotationSpeed();
            }

            if (nextPosition.Y < 0f + radius) // Collide with top wall
            {
                UtilityFunctions.PlaySoundArray(soundsBounce, 1);
                nextPosition.Y = 0f + radius + 1;

                // Flip the direction and change it a little randomly
                direction.Y = -direction.Y;
                var radians = UtilityFunctions.ConvertHeadingVectorToRadians(direction);
                if (UtilityFunctions.IsPositive(direction.X))
                {
                    radians += (float)((Globals.RandomGenerator.NextDouble() * colRandomMax) - colRandomMax * 0.5f);
                }
                else
                {
                    radians += (float)((Globals.RandomGenerator.NextDouble() * colRandomMax) - colRandomMax * 0.5f);
                }
                // Clamp
                radians = Math.Clamp(radians, (float)(-Math.PI) + clampDelta, (float)(0) - clampDelta);

                direction = UtilityFunctions.ConvertRadiansToHeadingVector(radians);

                NewRotationSpeed();
            }
            else if (nextPosition.Y > height - radius) // Collide with bottom wall
            {
                UtilityFunctions.PlaySoundArray(soundsBounce, 1);
                nextPosition.Y = height - radius - 1;

                // Flip the direction and change it a little randomly
                direction.Y = -direction.Y;
                var radians = UtilityFunctions.ConvertHeadingVectorToRadians(direction);
                if (UtilityFunctions.IsPositive(direction.X))
                {
                    radians += (float)(Globals.RandomGenerator.NextDouble() * colRandomMax);
                }
                else
                {
                    radians -= (float)(Globals.RandomGenerator.NextDouble() * colRandomMax);
                }
                radians = Math.Clamp(radians, (float)(0) + clampDelta, (float)(Math.PI) - clampDelta);

                direction = UtilityFunctions.ConvertRadiansToHeadingVector(radians);

                NewRotationSpeed();
            }

            // I also want these balls to bouce off any Rectangle of some of the other objects in the game "gameObjects"
            for (int i = 0; i < gameObjects.Count; i++)
            {
                if (gameObjects[i] is Board || gameObjects[i] is BlockObject)
                {

                    var colObjRectangle = gameObjects[i].Rectangle;
                    var currentRectangle = Rectangle;

                    int d = 6; // I added a delta here to make the lines smaller, so no collision should be with both lines at the same time
                    Rectangle colObjLineTop = new(colObjRectangle.X + d, colObjRectangle.Y, colObjRectangle.Width - d * 2, 0);
                    Rectangle colObjLineBot = new(colObjRectangle.X + d, colObjRectangle.Bottom, colObjRectangle.Width - d * 2, 0);
                    Rectangle colObjLineLeft = new(colObjRectangle.X, colObjRectangle.Y + d, 0, colObjRectangle.Height - d * 2);
                    Rectangle colObjLineRight = new(colObjRectangle.Right, colObjRectangle.Y + d, 0, colObjRectangle.Height - d * 2);

                    var intersectLineLeft = currentRectangle.Intersects(colObjLineLeft);
                    var intersectLineRight = currentRectangle.Intersects(colObjLineRight);
                    var intersectLineTop = currentRectangle.Intersects(colObjLineTop);
                    var intersectLineBot = currentRectangle.Intersects(colObjLineBot);

                    if (intersectLineLeft || intersectLineRight || intersectLineTop || intersectLineBot)
                    {
                        UtilityFunctions.PlaySoundArray(soundsBounce, 1);

                        if (gameObjects[i] is Board)
                        {
                            Board board = (Board)gameObjects[i];

                            if (currentRectangle.Intersects(colObjLineLeft))
                            {
                                direction.X = -direction.X;
                                nextPosition.X = colObjRectangle.X - radius - 1;
                                nextPosition.X += board.velocity.X; // Push the ball in the direction the board is moving this frame
                            }
                            else if (currentRectangle.Intersects(colObjLineRight))
                            {
                                direction.X = -direction.X;
                                nextPosition.X = colObjRectangle.X + colObjRectangle.Width + radius + 1;
                                nextPosition.X += board.velocity.X; // Push the ball in the direction the board is moving this frame
                            }
                            else if (currentRectangle.Intersects(colObjLineTop))
                            {
                                // Basically i want to move the direction vector left the more left the board is going
                                // and right the more right the board is going
                                // clamp to 170-10 degrees
                                // if no movement in board just bounce normal
                                // max board speed should move the direction 90 degrees = PI/2?

                                direction.Y = -direction.Y;

                                var radiansToShift = (float)((board.velocity.X / board.maxSpeed) * (Math.PI / 2) * -0.5f);
                                var oldDirRadians = UtilityFunctions.ConvertHeadingVectorToRadians(direction);
                                Debug.WriteLine("radiansToShift: " + radiansToShift);
                                Debug.WriteLine("oldDirRadians: " + oldDirRadians);
                                var finalRadians = MathHelper.Clamp(oldDirRadians + radiansToShift, 0.2f, (float)(Math.PI) - 0.2f);
                                Debug.WriteLine("finalRadians: " + finalRadians);
                                var newDirection = UtilityFunctions.ConvertRadiansToHeadingVector(finalRadians);
                                Debug.WriteLine("newDirectionX: " + newDirection.X);
                                Debug.WriteLine("newDirectionY: " + newDirection.Y);

                                direction = newDirection;

                                // Set position to top of board
                                nextPosition.Y = colObjRectangle.Y - radius - 1;

                            }
                            else if (currentRectangle.Intersects(colObjLineBot))
                            {
                                direction.Y = -direction.Y;
                                nextPosition.Y = colObjRectangle.Y + colObjRectangle.Height + radius + 1;
                            }

                        }
                        else
                        {
                            if (currentRectangle.Intersects(colObjLineLeft))
                            {
                                direction.X = -direction.X;
                                nextPosition.X = colObjRectangle.X - radius - 1;
                            }
                            else if (currentRectangle.Intersects(colObjLineRight))
                            {
                                direction.X = -direction.X;
                                nextPosition.X = colObjRectangle.X + colObjRectangle.Width + radius + 1;
                            }
                            else if (currentRectangle.Intersects(colObjLineTop))
                            {
                                direction.Y = -direction.Y;
                                nextPosition.Y = colObjRectangle.Y - radius - 1;
                            }
                            else if (currentRectangle.Intersects(colObjLineBot))
                            {
                                direction.Y = -direction.Y;
                                nextPosition.Y = colObjRectangle.Y + colObjRectangle.Height + radius + 1;
                            }

                            if (gameObjects[i] is BlockObject)
                            {
                                if (gameObjects[i].UpdateHP(-1))
                                {
                                    indicesToRemove.Add(i);
                                }
                            }
                        }

                        // Leave and stop the loop here becuase we had a collision
                        break;
                    }
                }
            }

            Position = nextPosition;

        }

        // Function to clamp a directional vector to be between 170 and 10 degrees
        public static Vector2 ClampDirection(Vector2 direction)
        {
            // Convert direction to angle in radians
            float angle = (float)Math.Atan2(direction.Y, direction.X);

            // Clamp angle between 170 and 10 degrees
            float clampedAngle = MathHelper.Clamp(angle, MathHelper.ToRadians(170), MathHelper.ToRadians(10));

            // Convert clamped angle back to vector
            return new Vector2((float)Math.Cos(clampedAngle), (float)Math.Sin(clampedAngle));
        }

    }
}
