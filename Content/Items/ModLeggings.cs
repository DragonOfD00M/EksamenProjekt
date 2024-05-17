using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace EksamenProjekt.Content.Items
{
    internal class ModLeggings : Toolbox.Leggings
    {
        internal ModLeggings() : base(999, 40, 40, Item.buyPrice(silver: 1), ItemRarityID.Yellow) { }
    }

}
