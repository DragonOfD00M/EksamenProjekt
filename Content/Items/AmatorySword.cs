using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static EksamenProjekt.Toolbox;


namespace EksamenProjekt.Content.Items
{ 
	// This is a basic item template.
	// Please see tModLoader's ExampleMod for every other example:
	// https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
	public class AmatorySword : ModItem
	{
        readonly Sword sword = new(600, 40, 40, 50, Item.buyPrice(silver: 1),ItemRarityID.Blue, SoundID.Item1, 6, false);
        // The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.EksamenProjekt.hjson' file.
        public override void SetDefaults()
		{
			sword.AddProjectile(ProjectileID.CrystalShard, 20);
			sword.InitiateDefaults(this);
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			sword.AddIngredients(recipe, (ItemID.Wood, 1));
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}
