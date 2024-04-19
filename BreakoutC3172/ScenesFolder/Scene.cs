using BreakoutC3172.Objects;
using BreakoutC3172.Objects.Blocks;
using BreakoutC3172.SystemsCore;

namespace BreakoutC3172.ScenesFolder
{
    internal abstract class Scene
    {
        protected readonly RenderTarget2D target;
        //protected readonly GameManager gameManager;

        protected List<GameObject> gameObjects = new();
        protected List<int> indicesToRemove = new();

        private Texture2D ui_overlay;

        // Make a list of UI Objects and render it after the gameObjects List?
        // This would be for In-Game UI Elements


        public Scene(GameManager gameManager)
        {
            //this.gameManager = gameManager;
            target = UtilityFunctions.GetNewRenderTarget();
            Load();
        }

        protected virtual void Load()
        {

        }

        public abstract void Activate();

        public virtual void Update()
        {
            // Update all objects
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Update(gameObjects, indicesToRemove);
            }

            // Remove marked objects
            foreach (int index in indicesToRemove)
            {
                gameObjects.RemoveAt(index);
            }
            indicesToRemove.Clear();
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
