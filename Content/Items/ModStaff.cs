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
    internal class ModStaff : Staff
    {
        internal ModStaff():base(ProjectileID.BlackBolt, 10, 999, 5, 50, 10, SoundID.Item71, 40, 40, Item.buyPrice(silver: 1), ItemRarityID.Yellow)
        {
            InitRecipe(TileID.WorkBenches, (ItemID.Wood, 20), (ItemID.WoodFishingPole, 1));
        }
    }
}
