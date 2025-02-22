﻿using System;
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
    [AutoloadEquip(EquipType.Head)] // Det her er for at spillet ved at det er rustning man kan tage på
    internal class ModHelmet : Helmet
    {
        internal ModHelmet() : base(999, 40, 40, Item.buyPrice(silver: 1), ItemRarityID.Yellow) 
        {
            MakeIntoArmorset(ModContent.ItemType<ModBreastplate>(), ModContent.ItemType<ModLeggings>());
            InitRecipe(TileID.WorkBenches, (ItemID.StoneBlock, 20), (ItemID.WoodHelmet, 1));
        }
        //Funktionen laver en armorset bonus, så hvis man har alle 3 stykker rustning på.
        public override void UpdateArmorSet(Player player)
        {
            player.GetDamage(DamageClass.Melee) += 10;
        }

    }

}
