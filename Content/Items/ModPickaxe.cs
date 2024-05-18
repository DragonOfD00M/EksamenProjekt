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
    internal class ModPickaxe : Pickaxe
    {
        internal ModPickaxe():base(999, 0, 0, 10, SoundID.Item1, 40, 40, Item.buyPrice(silver: 1), ItemRarityID.Yellow)
        {
            InitRecipe(TileID.WorkBenches, (ItemID.Wood, 20), (ItemID.CopperPickaxe, 20));
        }
    }
}
