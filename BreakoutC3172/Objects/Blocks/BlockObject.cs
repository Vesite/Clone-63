using BreakoutC3172.SystemsCore;

namespace BreakoutC3172.Objects.Blocks
{
    internal abstract class BlockObject : GameObject
    {
        public BlockObject(List<Texture2D> textures, Vector2 position, float scale, int hp) : base(textures, position, scale)
        {
            this.hp = hp;
            this.hp_max = hp;
        }

        public override void Draw()
        {

            base.Draw();

            // Breaking Texture
            if (!(hp == hp_max))
            {
                if (hp == 1)
                {
                    // Draw big cracks
                    UtilityFunctions.DrawAtlasImage(textures[1], 1, 32, Position, 4, 0);
                }
                else if (hp >= 2)
                {
                    // Draw small cracks
                    UtilityFunctions.DrawAtlasImage(textures[1], 1, 32, Position, 1, 0);
                }
            }

        }
    }
}
