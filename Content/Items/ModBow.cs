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
    internal class ModBow : Bow
    {
        internal ModBow() : base(ProjectileID.WoodenArrowFriendly, 999, 5, 50, 10, SoundID.Item2, 40, 40, Item.buyPrice(silver: 1), ItemRarityID.Yellow)
        {
            AddAmmo(AmmoID.Arrow);
            InitRecipe(TileID.WorkBenches, (ItemID.Wood, 20), (ItemID.WoodenBow, 1));
        }
    }
}
