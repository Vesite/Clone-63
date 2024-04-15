using BreakoutC3172.Objects.Blocks;

namespace BreakoutC3172.Objects
{
    internal class Ball : GameObject
    {
        public float radius;
        private Vector2 direction;
        private float speed;

        private float rotation;
        private float rotation_speed;
        private Vector2 velocity;

        private Random random = new();

        public Ball(List<Texture2D> textures, Vector2 position, float scale, float radius, Vector2 direction, float speed)
             : base(textures, position, scale)
        {
            this.radius = radius;
            this.direction = direction;
            this.speed = speed;

            rotation = (float)(random.NextDouble() * 2 * Math.PI);
            NewRotationSpeed();

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
                UtilityFunctions.DrawOutline(Rectangle);
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

            if (nextPosition.X < 0f + radius) // Collide with left wall
            {
                nextPosition.X = 0f + radius;
                direction.X = -direction.X;
                NewRotationSpeed();
            }
            else if (nextPosition.X > width - 5 * 32 - radius) // Collide with right wall
            {
                nextPosition.X = width - 5 * 32 - radius;
                direction.X = -direction.X;
                NewRotationSpeed();
            }

            if (nextPosition.Y < 0f + radius) // Collide with top wall
            {
                nextPosition.Y = 0f + radius;
                direction.Y = -direction.Y;
                NewRotationSpeed();
            }
            else if (nextPosition.Y > height - radius) // Collide with bottom wall
            {
                nextPosition.Y = height - radius;
                direction.Y = -direction.Y;
                NewRotationSpeed();
            }

            // I also want these balls to bouce off any Rectangle of some of the other objects in the game "gameObjects"
            for (int i = 0; i < gameObjects.Count; i++)
            {
                if (gameObjects[i] is Board || gameObjects[i] is BlockObject)
                {

                    var colObjRectangle = gameObjects[i].Rectangle;
                    var currentRectangle = Rectangle;

                    int d = 8; // I added a delta here to make the lines smaller, so no collision should be with both lines at the same time
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

                        // Leave and stop the loop here becuase we had a collision
                        break;
                    }
                }
            }

            Position = nextPosition;

        }
    }
}
