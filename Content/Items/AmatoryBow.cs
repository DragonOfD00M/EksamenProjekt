using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static EksamenProjekt.Toolbox;

namespace EksamenProjekt.Content.Items
{
    internal class AmatoryBow : ModItem
    {
        readonly Bow bow = new(20000, 40, 40, 50, Item.buyPrice(silver: 1), ItemRarityID.Green, SoundID.Item2, ProjectileID.WoodenArrowFriendly, 10);
        public override void SetDefaults()
        {
            bow.AddAmmo(AmmoID.Arrow);
            bow.InitiateDefaults(this);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            bow.AddIngredients(recipe, (ItemID.Wood, 2));
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
        public override Vector2? HoldoutOffset() => new Vector2(-8f, 0f);
    }
}
