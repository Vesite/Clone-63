using BreakoutC3172.SystemsCore;

namespace BreakoutC3172.Objects
{
    public class GameObject
    {

        protected List<Texture2D> textures;
        public Vector2 Position { get; set; }
        public float scale;
        public Rectangle Rectangle => new((int)(Position.X - textures[0].Width * scale / 2), (int)(Position.Y - textures[0].Height * scale / 2),
                                          (int)(textures[0].Width * scale), (int)(textures[0].Height * scale));
        public int hp = 1;
        public int hp_max = 1;

        public GameObject(List<Texture2D> textures, Vector2 position, float scale)
        {
            this.textures = textures;
            Position = position;
            this.scale = scale;
        }

        public virtual void Update(List<GameObject> gameObjects, List<int> indicesToRemove)
        {
            // Default implementation (do nothing)
        }

        public virtual void Draw()
        {
            // Default implementation
            Globals.SpriteBatch.Draw(textures[0], Position, null, Color.White, 0f, new Vector2(textures[0].Width / 2, textures[0].Height / 2), scale, SpriteEffects.None, 0f);
            if (Globals.IsDrawingOutline)
                UtilityFunctions.DrawOutline(Rectangle);
        }




        // Maybe not the best system, but this returns true if instance dies, and i have to deal with that somewhere else
        public bool UpdateHP(int amount)
        {
            hp += amount;

            if (hp <= 0)
            {
                hp = 0;
                // Kill this instance
                return true;
            }

            return false;
        }

    }
}
