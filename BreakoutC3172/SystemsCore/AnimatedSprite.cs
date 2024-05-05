using BreakoutC3172.Objects;

namespace BreakoutC3172.SystemsCore
{
    public class AnimatedSprite
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int totalFrames;
        private float AnimationSeconds;

        private float timeSinceLastFrameChange = 0f;

        public AnimatedSprite(Texture2D texture, int rows, int columns, float animationSeconds)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            AnimationSeconds = animationSeconds;
        }

        public void Update(List<GameObject> gameObjects, List<int> indicesToRemove)
        {
            // This needs to use real time "seconds"
            var timeEachFrame = AnimationSeconds / totalFrames;

            // (float)gt.ElapsedGameTime.TotalSeconds;
            // Increment current frame based on elapsed time
            timeSinceLastFrameChange += Globals.Time;
            if (timeSinceLastFrameChange >= timeEachFrame)
            {
                currentFrame += 1;
                if (currentFrame == totalFrames)
                {
                    currentFrame = 0;
                }

                timeSinceLastFrameChange = timeSinceLastFrameChange - timeEachFrame;
            }

        }

        public void Draw(Vector2 position, float rotation, float alpha)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = currentFrame / Columns;
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, width, height);

            // Calculate the origin for rotation (center of the sprite)
            Vector2 origin = new Vector2(destinationRectangle.Width / 2, destinationRectangle.Height / 2);

            // This was another version of draw i found
            Globals.SpriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White * alpha, rotation, origin, SpriteEffects.None, 0);

        }
    }

}
