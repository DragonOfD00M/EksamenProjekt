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
    internal class ModHelmet : Toolbox.Helmet
    {
        internal ModHelmet() : base(999, 40, 40, Item.buyPrice(silver: 1), ItemRarityID.Yellow) { }

    }

}
