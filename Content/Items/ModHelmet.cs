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
    [AutoloadEquip(EquipType.Head)]
    internal class ModHelmet : Toolbox.Helmet
    {
        internal ModHelmet() : base(999, 40, 40, Item.buyPrice(silver: 1), ItemRarityID.Yellow) 
        {
            MakeIntoArmorset(ModContent.ItemType<ModBreastplate>(), ModContent.ItemType<ModLeggings>());
            InitRecipe(TileID.WorkBenches, (ItemID.StoneBlock, 20), (ItemID.WoodHelmet, 1));
        }
        public override void UpdateArmorSet(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 10;
        }

    }

}
