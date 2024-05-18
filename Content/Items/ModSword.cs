using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using static EksamenProjekt.Toolbox;

namespace EksamenProjekt.Content.Items
{
    internal class ModSword : Sword
    {
        internal ModSword() : base(999, 5, true, 10, SoundID.Item1, 10, 10, Item.buyPrice(silver: 1), ItemRarityID.Yellow)
        {
            AddProjectile(ProjectileID.CannonballFriendly, 5);
            InitRecipe(TileID.WorkBenches, (ItemID.Wood, 20), (ItemID.CopperShortsword, 1));
        }
    }
}
