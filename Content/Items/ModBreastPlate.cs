using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static EksamenProjekt.Toolbox;

namespace EksamenProjekt.Content.Items
{
    [AutoloadEquip(EquipType.Body)]
    internal class ModBreastplate : Breastplate
    {
        internal ModBreastplate() : base(999, 40, 40, Item.buyPrice(silver: 1), ItemRarityID.Yellow) 
        {
            InitRecipe(TileID.WorkBenches, (ItemID.StoneBlock, 30), (ItemID.WoodBreastplate, 1));
        }
    }

}
