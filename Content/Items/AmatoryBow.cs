using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static EksamenProjekt.Toolbox;

namespace EksamenProjekt.Content.Items
{
    internal class AmatoryBow : Bow
    {
        internal AmatoryBow() : base(999,5,40,40,20,Item.buyPrice(silver: 2),ItemRarityID.Yellow, SoundID.Item2, ProjectileID.WoodenArrowFriendly, 20)
        {
            AddAmmo(AmmoID.Arrow);
            MakeRecipe(TileID.WorkBenches, (ItemID.Wood, 20), (ItemID.WoodenBow, 1));
        }
        public override Vector2? HoldoutOffset() => new Vector2(-8f, 0f); // Placere buen så man faktisk holder den.
    }
}
