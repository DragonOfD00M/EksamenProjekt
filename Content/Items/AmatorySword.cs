using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static EksamenProjekt.Toolbox;


namespace EksamenProjekt.Content.Items
{ 
	internal class AmatorySword : Sword
    {
		internal AmatorySword() : base(999, 5, 40, 40, 20, Item.buyPrice(silver: 1), ItemRarityID.Yellow, SoundID.Item1, true)
		{
			AddProjectile(ProjectileID.PinkLaser, 20);
		}

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			AddIngredients(recipe, (ItemID.Wood, 10), (ItemID.CopperShortsword, 1));
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}
