using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace EksamenProjekt.Content.Items
{
    internal class AmatoryPickaxe : Toolbox.Pickaxe
    {
        internal AmatoryPickaxe() : base(250, 6, 6, 10, SoundID.PlayerKilled, 40, 40, Item.buyPrice(silver: 1), ItemRarityID.Yellow)
        {
            MakeRecipe(TileID.WorkBenches, (ItemID.Wood, 20), (ItemID.CopperPickaxe, 1));
        }
    }
}
