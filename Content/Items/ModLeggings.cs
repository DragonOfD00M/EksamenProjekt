using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EksamenProjekt.Content.Items
{
    [AutoloadEquip(EquipType.Legs)]
    internal class ModLeggings : Toolbox.Leggings
    {
        internal ModLeggings() : base(999, 40, 40, Item.buyPrice(silver: 1), ItemRarityID.Yellow) 
        {
            InitRecipe(TileID.WorkBenches, (ItemID.StoneBlock, 20), (ItemID.WoodGreaves, 1));
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 200 / 100f;
        }
    }

}
