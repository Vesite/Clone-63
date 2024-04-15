using BreakoutC3172.Objects;

namespace BreakoutC3172.ScenesFolder
{
    internal abstract class Scene
    {
        protected readonly RenderTarget2D target;
        protected readonly GameManager gameManager;

        protected List<GameObject> gameObjects = new();
        protected List<int> indicesToRemove = new();

        private Texture2D ui_overlay;

        public Scene(GameManager gameManager)
        {
            this.gameManager = gameManager;
            target = UtilityFunctions.GetNewRenderTarget();
            Load();
        }

        protected virtual void Load()
        {
            ui_overlay = Globals.Content.Load<Texture2D>("ui_left");
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

            // Draw UI Here?
            Globals.SpriteBatch.Draw(ui_overlay, new Vector2(0, 0), null, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);

            Globals.SpriteBatch.End();

            Globals.GraphicsDevice.SetRenderTarget(null);
            return target;

        }

    }
}
