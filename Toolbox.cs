using EksamenProjekt.Content.Items;
using Microsoft.Build.Tasks;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace EksamenProjekt
{
    /// <summary>
    /// Toolbox er vores produkt som indeholder en masse redkaber (klasser) som skal gøre det nemmere at kode mods til Terraria
    /// Vi har brugt tmodloaders oficielle GitHub og mod eksempler til at finde syntaxen, og så har vi selv valgt hvilke redskaber der skal bruges.
    /// Vi har valgt kun at sætte os ind i hvordan man laver items, og ikke blocks og ling. da de kræver et helt andet, mere kompliceret system.
    /// </summary>
    public class Toolbox
    {
        /// <summary>
        /// CustomItem er den første klasse i vores nedarvingshieraki. Den er selv nedarvet Terraria.ModLoader.ModItem, så derfor kan man lave overrides allerede herinde.
        /// </summary>
        public class CustomItem : ModItem
        {
            // Der bruges en indre constructor frem for 'primary constructor' for at gøre koden mere overskuelig.
            /// <summary>
            /// Constructoren tager argumenterne width, height, value og rarity
            /// </summary>
            /// <param name="width">Hitboxens Bredde</param>
            /// <param name="height">Hitboxens Højde</param>
            /// <param name="value">Itemens værdi, altså hvor dyr den er at købe.</param>
            /// <param name="rarity">Itemens rarity, altså hvor shælden den er.</param>
            public CustomItem(int width, int height, int value, int rarity)
            {
                Width = width;
                Height = height;
                Value = value; 
                Rarity = rarity;
            }
            // De følgende linjer gør at man kan gemme argumenterne som variabler til senere brug.
            // De skal defineres uden for constructoren med { get; } da værdien skal sættes en gang og ikke ændres igen senere.
            // Det er simplere end at skrive public readonly int Width; Har også noget med indkapsling at gøre ifølge Hr. GPT.
            public int Width { get; }
            public int Height { get; }
            public int Value { get; }
            public int Rarity { get; }
            // Her klargøres andre variabler der skal sættes uden for constructoren.
            public bool hasRecipe = false;
            public (short, int)[] Ingredients;
            public ushort Workbench;
            
            /*En stor del af Terraria modding er at bruge metode underkendelse (Method Overiding).
            Det er fordi, ModItem har en masse prædefinerede funktioner og fordi vi netop nedarver ModItem til resten af vores Items.
            SetDefaults() er en meget vigtig funktion som bruges til at give vores items værdier.
            Her i CustomItem gives de kun 4 værdier, men som itemsne bliver mere og mere specifikke inkluderes flere og flere elementer.
            Det smarte er at man kan underkende denne funktion i nedarvede funktioner for at gøre den mere specifik.*/
            public override void SetDefaults()
            {
                // Item er en specifik værdi fra ModItem som tog lidt tid at forstå præcis hvordan skulle implementeres.
                Item.width = Width;
                Item.height = Height;
                Item.value = Value;
                Item.rare = Rarity;
            }
            // Hvis man vil have en item, så skal man kunne lave den på en eller anden måde og det sørger vi for med de 2 næste funktioner.
            /// <summary>
            /// MakeRecipe er en brugerdefineret funktion som giver værdier til variabler som skal bruges til at lave et item.
            /// </summary>
            /// <param name="workbench">En ushort (TileIDs type) som bestemmer hvorhenne man laver itemmen</param>
            /// <param name="ingredients">
            /// En et værdipar af en short (ItemIDs type) som ræpresenterer indgrædiensen. 
            /// Den anden er en int som ræpresenterer mængden.
            /// </param>
            public void InitRecipe(ushort workbench, params (short, int)[] ingredients)
            {
                hasRecipe = true; // hasRecipe 
                Workbench = workbench; 
                // Ingredients er lidt speciel. Det er et params argument.
                // Det specielle ved params er at det gør ingredients til en liste med mulighed for en uendelig mængde (short, int) værdipar.
                // Man kan derfor skrive så mange ingredienser man har lyst til.
                // Hvis det hjælper så svare det til *args i python.
                Ingredients = ingredients;
            }
            /// <summary>
            /// AddIngredients er en funktion som bruger en speciel løkke til at tilføje alle elementer i ingredients til opskriften.
            /// </summary
            public static void AddIngredients(Recipe recipe, params (short, int)[] ingredients)
            {
                // Et foreach løkke går igennem alle elementer i en liste og giver dem et specifikt navn.
                // I dette tilfælde er navnet ingredient i listen ingredients.
                // Det er standard navngivning at have listen i flertal og navnet i ental.
                // foreach løkken svare til for løkken i python men vær opmærksom på at en for løkke i c# fungere på en anden måde.
                foreach ((short, int) ingredient in ingredients)
                {
                    recipe.AddIngredient(ingredient.Item1, ingredient.Item2); // AddIngredient er en metode til Terrarias type Recipe som laves i en underkendelse længere nede.
                }
            }
            // Her bruges endnu en underkendelse. ModItem har nemlig allerede en funktion der laver en opskrift.
            // Vi vil bare ændre den så det er vores egne værdier der er med.
            public override void AddRecipes()
            {
                if (hasRecipe) // Først tjekker vi om vi har lavet en opskrift. Husk InitRecipe sætter denne til true, mens den normalt er false.
                {
                    Recipe recipe = CreateRecipe(); // Vi bruger en ModItem metode til at klargøre 
                    AddIngredients(recipe, Ingredients);
                    recipe.AddTile(Workbench);
                    recipe.Register();
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
        
        public class Helmet: ArmorPiece
        {
            public Helmet(int defence, int width, int height, int value, int rarity) : base(defence, width, height, value, rarity) { }
            public bool ArmorSetActive = false;
            public int PairedBreastplate;
            public int PairedLeggings;
            public void MakeIntoArmorset(int pairedBreastplate, int pairedLeggings)
            {
                ArmorSetActive = true;
                PairedBreastplate = pairedBreastplate;
                PairedLeggings = pairedLeggings;
                
            }
            public override bool IsArmorSet(Item head, Item body, Item legs)
            {
                return body.type == PairedBreastplate && legs.type == PairedLeggings;
            }

        }
        public class Breastplate: ArmorPiece
        {
            public Breastplate(int defence, int width, int height, int value, int rarity) : base(defence, width, height, value, rarity) { }
        }
        public class Leggings : ArmorPiece
        {
            public Leggings(int defence, int width, int height, int value, int rarity) : base(defence, width, height, value, rarity) { }
        }
    }
}
