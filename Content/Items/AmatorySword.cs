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
			AddProjectile(ProjectileID.PineNeedleFriendly, 20);
			MakeRecipe(TileID.WorkBenches, (ItemID.Wood, 10), (ItemID.CopperShortsword, 1));
		}
	}
}
