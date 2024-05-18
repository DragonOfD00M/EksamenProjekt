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
    [AutoloadEquip(EquipType.Legs)] // Det her er for at spillet ved at det er rustning man kan tage på
    internal class ModLeggings : Leggings
    {
        internal ModLeggings() : base(999, 40, 40, Item.buyPrice(silver: 1), ItemRarityID.Yellow) 
        {
            InitRecipe(TileID.WorkBenches, (ItemID.StoneBlock, 20), (ItemID.WoodGreaves, 1));
        }
        // Funktionen herunder laver en effekt man får når man tager det her stykke rustning på.
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 200 / 100f;
        }
    }

}
