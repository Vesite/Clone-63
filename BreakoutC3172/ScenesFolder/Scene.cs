using BreakoutC3172.Objects;
using BreakoutC3172.Objects.Blocks;
using BreakoutC3172.SystemsCore;
using System.Diagnostics;

namespace BreakoutC3172.ScenesFolder
{
    internal abstract class Scene
    {
        protected readonly RenderTarget2D target;
        protected readonly GameManager gameManager;
        protected readonly SceneManager sceneManager;

        protected List<GameObject> gameObjects = new();
        protected List<int> indicesToRemove = new();

        private Texture2D ui_overlay;

        // Make a list of UI Objects and render it after the gameObjects List?
        // This would be for In-Game UI Elements


        public Scene(GameManager gameManager, SceneManager sceneManager)
        {
            this.gameManager = gameManager;
            this.sceneManager = sceneManager;

            target = UtilityFunctions.GetNewRenderTarget();
            Load();

        }

        protected virtual void Load()
        {

        }

        public abstract void Activate();

        public virtual void Update()
        {
            // Update all objects (in order so that i can add new objects to the list during the loop)
            for (int i = 0; i < gameObjects.Count; i++)
            {
                GameObject gameObject = gameObjects[i];
                gameObject.Update(gameObjects, indicesToRemove);
            }

            // Remove duplicates from indicesToRemove
            HashSet<int> uniqueIndices = new HashSet<int>(indicesToRemove);
            indicesToRemove.Clear();
            indicesToRemove.AddRange(uniqueIndices);

            // Sort the indices in ascending order
            indicesToRemove.Sort();

            // Remove marked objects
            for (int i = indicesToRemove.Count - 1; i >= 0; i--)
            {
                int index = indicesToRemove[i];
                if (index >= 0 && index < gameObjects.Count)
                {
                    gameObjects.RemoveAt(index);
                }
                else
                {
                    // Error? Should never happen
                    Debug.WriteLine("Error, sould never happen (indicesToRemove invelid)");
                }
            }
            indicesToRemove.Clear();

            //Debug.WriteLine("Object Count: " + gameObjects.Count);
        }

        protected virtual void Draw()
        {
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Draw();
            }
        }

        public virtual RenderTarget2D GetFrame()
        {
            Globals.GraphicsDevice.SetRenderTarget(target);
            Globals.GraphicsDevice.Clear(Color.Black);

            Globals.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

            Draw();

            Globals.SpriteBatch.End();

            Globals.GraphicsDevice.SetRenderTarget(null);
            return target;

        }


        public void SpawnBlocks(int[,] tiles, Texture2D blockDirt, Texture2D blockStone, Texture2D blockMetal, Texture2D breakTexture)
        {
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    if (tiles[x, y] == 0) continue;
                    var posX = y * 32 + 16;
                    var posY = x * 32 + 16;

                    if (tiles[x, y] == 1)
                    {
                        var texture = blockDirt;
                        var hp = 1;
                        gameObjects.Add(new BlockBasic(new() { texture, breakTexture }, new(posX, posY), 1, hp));
                    }
                    else if (tiles[x, y] == 2)
                    {
                        var texture = blockStone;
                        var hp = 2;
                        gameObjects.Add(new BlockBasic(new() { texture, breakTexture }, new(posX, posY), 1, hp));
                    }
                    else if (tiles[x, y] == 3)
                    {
                        var texture = blockMetal;
                        var hp = 3;
                        gameObjects.Add(new BlockBasic(new() { texture, breakTexture }, new(posX, posY), 1, hp));
                    }
                }
            }
        }

    }
}
