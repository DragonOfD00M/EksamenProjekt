using EksamenProjekt.Content.Items;
using Microsoft.Build.Tasks;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace EksamenProjekt
{
    public class Toolbox
    {
        // Jeg tror toolbox skal være der, men det er ihvertfald den der indeholder alle andre klasser.
        public class CustomItem : ModItem
        {
            // CustomItem er nedarvet af Tmodloaders ModItem, det er også den 'ældste' af vores klasser.
            public CustomItem(int width, int height, int value, int rarity)
            {
                Width = width;
                Height = height;
                Value = value; 
                Rarity = rarity;
            }
            public int Width { get; }
            public int Height { get; }
            public int Value { get; }
            public int Rarity { get; }
            public bool hasRecipe = false;
            public (short, int)[] Ingredients;
            public ushort Workbench;
            // Da CusttomItem er nedarvet ModItem kan vi køre SetDefaults herinde,
            public override void SetDefaults()
            {
                Item.width = Width;
                Item.height = Height;
                Item.value = Value;
                Item.rare = Rarity;
            }
            // Custom funktion der bruger params til at kunne tage en uendelig mængde af ingredienser
            // Minder om *args i python
            
            public void MakeRecipe(ushort workbench, params (short, int)[] ingredients)
            {
                hasRecipe = true;
                Workbench = workbench;
                Ingredients = ingredients;
            }

            public override void AddRecipes()
            {
                if (hasRecipe)
                {
                    Recipe recipe = CreateRecipe();
                    AddIngredients(recipe, Ingredients);
                    recipe.AddTile(Workbench);
                    recipe.Register();
                }

            }

            public static void AddIngredients(Recipe recipe, params (short, int)[] ingredients)
            {
                // foreach er ligesom et python for loop
                // for loops i C# er anderledes
                foreach ((short, int) ingredient in ingredients)
                {
                    recipe.AddIngredient(ingredient.Item1, ingredient.Item2);
                }
            }
        }
        public class Tool : CustomItem
        {
            // Tool er nedarvet CustomItem
            public Tool(int useTime, SoundStyle useSound, int width, int height, int value, int rarity) : base(width, height, value, rarity)
            {
                UseSpeed = useTime;
                Sound = useSound;
            }
            public int UseSpeed { get; }
            public SoundStyle Sound { get; }
            public override void SetDefaults()
            {
                base.SetDefaults();
                Item.useTime = UseSpeed;
                Item.useAnimation = UseSpeed;
                Item.UseSound = Sound;
            }
        }
        public class Pickaxe : Tool
        {
            public Pickaxe(int strenght, int damage, int knockback, int useTime, SoundStyle useSound, int width, int height, int value, int rarity) : base(useTime, useSound, width, height, value, rarity)
            {
                Strength = strenght;
                Damage = damage;
                Knockback = knockback;
            }
            public int Strength { get; }
            public int Damage { get; }
            public int Knockback { get; }
            public override void SetDefaults()
            {
                base.SetDefaults();
                Item.damage = Damage;
                Item.knockBack = Knockback;
                Item.pick = Strength;
                Item.DamageType = DamageClass.Melee;
                Item.autoReuse = true;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.attackSpeedOnlyAffectsWeaponAnimation = true;
            }
        }
        public class Axe : Tool
        {
            public Axe(int axeStrength, int hammerStrength, int damage, int knockback, int useTime, SoundStyle useSound, int width, int height, int value, int rarity): base(useTime, useSound, width, height, value, rarity)
            {
                AxeStrenght = axeStrength;
                HammerStrenght = hammerStrength;
                Damage = damage;
                Knockback = knockback;
            }
            public int AxeStrenght { get; }
            public int HammerStrenght { get; }
            public int Damage { get; }
            public int Knockback { get; }
            public override void SetDefaults()
            {
                base.SetDefaults();
                Item.axe = AxeStrenght;
                Item.hammer = HammerStrenght;                
                Item.damage = Damage;
                Item.DamageType = DamageClass.Melee;
                Item.knockBack = Knockback;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.autoReuse = true;
                Item.attackSpeedOnlyAffectsWeaponAnimation = true;

            }
        }
        public class Weapon : Tool
        {
            // Weapon er et item, så den er nedarvet Tool
            public Weapon(int damage, int knockback, int width, int height, int useTime, int value, int rarity, SoundStyle useSound) : base(useTime, useSound, width, height, value, rarity)
            {
                // Vi tilføjer Damage, Knockback,til Weapon
                Damage = damage;
                Knockback = knockback;
                
            }
            public int Damage { get; }
            public int Knockback { get; }

            // I SetDefaults kan man bruge base.SetDefaults for at inkludere fra en ældre klasse
            public override void SetDefaults()
            {
                base.SetDefaults();
                Item.damage = Damage;
                Item.knockBack = Knockback;
            }
        }
        
        // Sword er nedarvet Weapon
        public class Sword : Weapon
        {
            public Sword(int damage, int knockback, int width, int height, int useTime, int value, int rarity, SoundStyle useSound, bool autoReuse) : base(damage, knockback, width, height, useTime, value, rarity, useSound)
            {
                // autoReuse er unikt til sword
                AutoReuse = autoReuse;
            }
            public bool AutoReuse { get; }
            public bool HasProjectile = false;
            public short projectile;
            public int ProjectileSpeed;
            public override void SetDefaults()
            {
                base.SetDefaults();
                // Man kan i sword definere DamageType og useStyle
                // Måske kan man nedarve ydeligere til forskellige typer sword der har forskellige useStyles.
                Item.DamageType = DamageClass.Melee;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.autoReuse = AutoReuse;
                if (HasProjectile == true)
                {
                    Item.shoot = projectile;
                    Item.shootSpeed = ProjectileSpeed;
                }
            }
            // Denne funktion bruges i Item filens contructor, hvis man gerne vil have at sværdet skal skyde ting.
            public void AddProjectile(short projectile, int shootSpeed)
            {
                HasProjectile = true;
                this.projectile = projectile;
                ProjectileSpeed = shootSpeed;
            }
        }
        // Bow er også nedarvet Weapon
        public class Bow : Weapon
        {
            public Bow(int damage, int knockback, int width, int height, int useTime, int value, int rarity, SoundStyle useSound, short projectile, int shootSpeed) : base(damage, knockback, width, height, useTime, value, rarity, useSound)
            {
                // Bow har et projectile og en shootspeed som er unikt for den.
                // Burde måske omnavngives til Gun, da det egentlig er det samme i Terraria
                Projectile = projectile;
                ShootSpeed = shootSpeed;
            }

            public short Projectile { get; }
            public int ShootSpeed { get; }
            public bool usesAmmo = false;
            public int AmmoType;
            public override void SetDefaults()
            {
                base.SetDefaults();
                // Igen laver man specifikke DamageClass og useStyle
                Item.DamageType = DamageClass.Ranged;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.noMelee = true; // Jeg ved ik' jeg satte den bare til true. Kan laves som en parameter hvis man har lyst.
                Item.shoot = Projectile;
                Item.shootSpeed = ShootSpeed;
                if (usesAmmo == true)
                {
                    Item.useAmmo = AmmoType;
                }

            }
            // Hvis Bow skal bruge ammo så bruges denne funktion
            public void AddAmmo(int ammoType)
            {
                usesAmmo = true;
                AmmoType = ammoType;
            }
        }
        public class ArmorPiece : CustomItem
        {
            public ArmorPiece(int defence, int width, int height, int value, int rarity) : base(width, height, value, rarity)
            {
                Defence = defence;
            }
            public int Defence { get; }
            public override void SetDefaults()
            {
                base.SetDefaults();
                Item.defense = Defence;
            }
        }
        [AutoloadEquip(EquipType.Head)]
        public class Helmet: ArmorPiece
        {
            public Helmet(int defence, int width, int height, int value, int rarity) : base(defence, width, height, value, rarity)
            {

            }

        }
        [AutoloadEquip(EquipType.Body)]
        public class Breastplate: ArmorPiece
        {
            public Breastplate(int defence, int width, int height, int value, int rarity) : base(defence, width, height, value, rarity)
            {

            }

        }
        public class Leggings : ArmorPiece
        {
            public Leggings(int defence, int width, int height, int value, int rarity) : base(defence, width, height, value, rarity)
            {

            }
        }
    }
}
