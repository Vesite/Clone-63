using BreakoutC3172.Objects.Blocks;
using BreakoutC3172.SystemsCore;
using Microsoft.Xna.Framework.Audio;

namespace BreakoutC3172.Objects
{
    public class Lightn : GameObject
    {

        public float TimeMax = 1f;
        public float TimeRemaining = 1f;
        public AnimatedSprite AnimatedSprite;
        public float TimeDelay;

        private float Alpha = 1.0f;
        private bool DelayEnd = false;
        private bool Exploded1 = false;
        private bool Exploded2 = false;

        private SoundEffect[] Sounds;
        private SoundEffect SoundExplosion;

        public Lightn(List<Texture2D> textures, Vector2 position, float scale, float timeDelay, SoundEffect[] sounds) : base(textures, position, scale)
        {
            Sounds = sounds;
            TimeDelay = timeDelay;

            SoundExplosion = Globals.Content.Load<SoundEffect>("Audio/SoundEffects/so_sfx_explosion_retro_2");
        }


        public override void Update(List<GameObject> gameObjects, List<int> indicesToRemove)
        {
            if (TimeDelay > 0)
            {
                TimeDelay = Math.Max(TimeDelay - Globals.Time, 0);
            }
            else
            {
                if (!DelayEnd)
                {
                    DelayEnd = true;
                    AnimatedSprite = new AnimatedSprite(textures[0], 1, 31, TimeMax);
                    UtilityFunctions.PlaySoundArray(Sounds, 1);
                    SoundExplosion.Play(1, 0, 0);
                }

                TimeRemaining = Math.Max(TimeRemaining - Globals.Time, 0);
                AnimatedSprite.Update(gameObjects, indicesToRemove);

                Alpha = (TimeRemaining / TimeMax) * 4f;

                // Kill this object if the animation is finished
                if (Alpha == 0)
                {
                    int index = gameObjects.IndexOf(this); // Find the index of the current object
                    if (index != -1) // Check if the object exists in the list
                    {
                        indicesToRemove.Add(index); // Add its index to indicesToRemove
                    }
                }

                if ((!Exploded1) && (TimeRemaining < TimeMax * 0.85f))
                {
                    Exploded1 = true;
                    // Basically find and damage the blocks at "targetPosition"
                    for (int i = 0; i < gameObjects.Count; i++)
                    {
                        if (gameObjects[i] is BlockObject)
                        {
                            if ((gameObjects[i].Position.X == Position.X) && (gameObjects[i].Position.Y == Position.Y))
                            {
                                if (gameObjects[i].UpdateHP(-1))
                                {
                                    indicesToRemove.Add(i);
                                }
                            }
                            else if ((gameObjects[i].Position.X == Position.X - 32) && (gameObjects[i].Position.Y == Position.Y))
                            {
                                if (gameObjects[i].UpdateHP(-1))
                                {
                                    indicesToRemove.Add(i);
                                }
                            }
                            else if ((gameObjects[i].Position.X == Position.X + 32) && (gameObjects[i].Position.Y == Position.Y))
                            {
                                if (gameObjects[i].UpdateHP(-1))
                                {
                                    indicesToRemove.Add(i);
                                }
                            }
                            else if ((gameObjects[i].Position.X == Position.X) && (gameObjects[i].Position.Y == Position.Y - 32))
                            {
                                if (gameObjects[i].UpdateHP(-1))
                                {
                                    indicesToRemove.Add(i);
                                }
                            }
                            else if ((gameObjects[i].Position.X == Position.X) && (gameObjects[i].Position.Y == Position.Y + 32))
                            {
                                if (gameObjects[i].UpdateHP(-1))
                                {
                                    indicesToRemove.Add(i);
                                }
                            }

                        }
                    }
                }

                if ((!Exploded2) && (TimeRemaining < TimeMax * 0.75f))
                {
                    Exploded2 = true;
                    // Basically find and damage the blocks at "targetPosition"
                    for (int i = 0; i < gameObjects.Count; i++)
                    {
                        if (gameObjects[i] is BlockObject)
                        {
                            if ((gameObjects[i].Position.X == Position.X) && (gameObjects[i].Position.Y == Position.Y))
                            {
                                if (gameObjects[i].UpdateHP(-1))
                                {
                                    indicesToRemove.Add(i);
                                }
                            }
                        }
                    }
                }

            }


        }

        public override void Draw()
        {
            if (DelayEnd)
            {
                AnimatedSprite.Draw(Position, 0, Alpha);
            }

        }


    }
}
