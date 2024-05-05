using BreakoutC3172.Objects.Blocks;
using BreakoutC3172.SystemsCore;
using Microsoft.Xna.Framework.Audio;

namespace BreakoutC3172.Objects
{
    public class Board : GameObject
    {
        public float acc = 20;
        public float maxSpeed = 6f;
        public Vector2 velocity;

        private SoundEffect[] soundsLighn = new SoundEffect[4];

        public Texture2D LigtnTexture;

        public Board(List<Texture2D> textures, Vector2 position, float scale) : base(textures, position, scale)
        {
            // Load the sound effects in your LoadContent method or wherever appropriate
            soundsLighn[0] = Globals.Content.Load<SoundEffect>("Audio/SoundEffects/so_sfx_lightn_new_1");
            soundsLighn[1] = Globals.Content.Load<SoundEffect>("Audio/SoundEffects/so_sfx_lightn_new_2");
            soundsLighn[2] = Globals.Content.Load<SoundEffect>("Audio/SoundEffects/so_sfx_lightn_new_3");
            soundsLighn[3] = Globals.Content.Load<SoundEffect>("Audio/SoundEffects/so_sfx_lightn_new_4");

            LigtnTexture = Globals.Content.Load<Texture2D>("lightn_anim_1");
        }

        public override void Update(List<GameObject> gameObjects, List<int> indicesToRemove)
        {

            if (InputManager.KeyClicked(Keys.Space))
            {
                AbilityLigthn(gameObjects);
            }

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

        public void AbilityLigthn(List<GameObject> gameObjects)
        {
            // TODO

            // I gotta create a "lightn" instance 3 times at slightly different timings,
            // the "lightn" instance will have to be a part of the list of all objects?
            // It would ideally remove itself when its timer is out, and also delete blocks so it needs the list of all blocks


            var tempList = new List<GameObject>();
            foreach (GameObject obj in gameObjects)
            {
                if (obj is BlockObject)
                {
                    tempList.Add(obj);
                }
            }
            // Lightn 1
            var randPos1 = new Vector2(200, 100);
            if (tempList.Count > 0)
            {
                int randomIndex = Globals.RandomGenerator.Next(tempList.Count);
                var randomObject = tempList[randomIndex];
                randPos1 = randomObject.Position;
            }
            var newObj1 = new Lightn(new() { LigtnTexture }, randPos1, 1, 0, soundsLighn);
            gameObjects.Add(newObj1);

            // Lightn 2
            var randPos2 = new Vector2(200, 100);
            if (tempList.Count > 0)
            {
                int randomIndex = Globals.RandomGenerator.Next(tempList.Count);
                var randomObject = tempList[randomIndex];
                randPos2 = randomObject.Position;
            }
            var newObj2 = new Lightn(new() { LigtnTexture }, randPos2, 1, 0.3f, soundsLighn);
            gameObjects.Add(newObj2);

            // Lightn 3
            var randPos3 = new Vector2(200, 100);
            if (tempList.Count > 0)
            {
                int randomIndex = Globals.RandomGenerator.Next(tempList.Count);
                var randomObject = tempList[randomIndex];
                randPos3 = randomObject.Position;
            }
            var newObj3 = new Lightn(new() { LigtnTexture }, randPos3, 1, 0.42f, soundsLighn);
            gameObjects.Add(newObj3);

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
