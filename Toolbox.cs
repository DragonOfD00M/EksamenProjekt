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
            // Der bruges en sekundære constructor frem for primære constructor for at gøre koden mere overskuelig.
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
            // Det er simplere end at skrive public readonly int Width; Har også noget med indkapsling at gøre.
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
            public static void AddIngredients(Recipe recipe, (short, int)[] ingredients)
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
                    AddIngredients(recipe, Ingredients); // Her køres den brugerdefinerede funktion der går igennem alle ingredients.
                    recipe.AddTile(Workbench); // Her sættes den valgte tile fra InitRecipe funktionen
                    recipe.Register(); // Her tilføjes opskriften til spillet
                }
            }
        }
        /// <summary>
        /// Weapon er det næste trin ned i nedarvingshierakiet.
        /// Weapon tager nogle flere argumenter som gør det mere advanceret end CustomItem
        /// </summary>
        public class Weapon : CustomItem
        {
            /// <summary>
            /// Weapons indre constructor.
            /// </summary>
            /// <param name="damage">Den skade våbnet gør</param>
            /// <param name="knockback">Hvor meget våbnet skubber fjender tilbage med</param>
            /// <param name="useTime">Hvor lang tid det tager at bruge våbnet</param>
            /// <param name="useSound">Hvilken lyd våbnet laver, når det bruges</param>
            /// <inheritdoc cref="CustomItem(int, int, int, int)"/>
            public Weapon(int damage, int knockback,  int useTime, SoundStyle useSound, int width, int height, int value, int rarity) : base(width, height, value, rarity)
            {
                // Der tilføjes her Damage, Knockback, UseTime og UseSound.
                Damage = damage;
                Knockback = knockback;
                UseTime = useTime;
                UseSound = useSound;  
            }
            public int Damage { get; }
            public int Knockback { get; }
            public int UseTime { get; }
            public SoundStyle UseSound { get; }

            // Her ses hvordan en underkendelse af SetDefault kan udvide den originale funktion yderligere.
            public override void SetDefaults()
            {
                base.SetDefaults(); // den her indsætter CustomItems underkendelse af SetDefaults
                // Herefter indsættes de nye argumenter som værdier
                Item.damage = Damage;
                Item.knockBack = Knockback;
                Item.useTime = UseTime; // Både useTime og useAnimation bruger UseTime værdien.
                Item.useAnimation = UseTime; // Det giver god mening da man gerne vil have animationen til at repræsentere hvornår man kan bruge redskabet.
                Item.UseSound = UseSound;
            }
        }
        // I Terraria kan man vælge at spille som man har lyst til, men det udstyr man får fat i kan hjælpe meget alt efter om du spiller som f.eks. en ridder, en skytte eller en troldmand.
        /// <summary>
        /// MeleeWeapon nedarves til våben som skal ramme fjenden.
        /// Dette er som hovedregels sværd, men kan også være andeet, redskaber inkluderet.
        /// </summary>
        public class MeleeWeapon : Weapon
        {
            /// <summary>
            /// Der er ikke nogen specifikke variabler der laves i MeleeWeapon frem for weapon.
            /// Isteddet bruges det til DRY (don't repeat yourself) konceptet.
            /// </summary>
            /// <inheritdoc cref="Weapon(int, int, int, SoundStyle, int, int, int, int)"/>
            public MeleeWeapon(int damage, int knockback, int useTime, SoundStyle useSound, int width, int height, int value, int rarity) : base(damage, knockback, useTime, useSound, width, height, value, rarity) { }
            
            public override void SetDefaults() 
            { 
                base.SetDefaults();
                Item.DamageType = DamageClass.Melee; // DamageType er den skadetype der bruges. Her sættes den til Melee, da det er et MeleeWeapon.
            }
        }
        /// <summary>
        /// RangedWeapon nedarves til våben der skyder noget.
        /// F.eks. en bue eller et gevær.
        /// </summary>
        public class RangedWeapon : Weapon
        {
            /// <summary>
            /// Sekunder constructor.
            /// </summary>
            /// <inheritdoc cref="Weapon(int, int, int, SoundStyle, int, int, int, int)"/>
            public RangedWeapon(int damage, int knockback, int useTime, SoundStyle useSound, int width, int height, int value, int rarity) : base(damage, knockback, useTime, useSound, width, height, value, rarity) { }

            public override void SetDefaults()
            {
                base.SetDefaults();
                Item.DamageType = DamageClass.Ranged;
            }

        }
        /// <summary>
        /// MagicWeapon nedarves til våben der bruger magi til at skade.
        /// Tryllestave og lign.
        /// </summary>
        public class MagicWeapon : Weapon
        {
            /// <summary>
            /// Sekundær constructor for MagicWeapon
            /// </summary>
            /// <inheritdoc cref="Weapon(int, int, int, SoundStyle, int, int, int, int)"/>
            public MagicWeapon(int damage, int knockback, int useTime, SoundStyle useSound, int width, int height, int value, int rarity) : base(damage, knockback, useTime, useSound, width, height, value, rarity) { }
            public override void SetDefaults()
            {
                base.SetDefaults();
                Item.DamageType = DamageClass.Magic;
            }
        }
        /// <summary>
        /// Staff er nedarvet MagicWeapon
        /// </summary>
        public class Staff : MagicWeapon
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="manaCost">Hvor meget mana koster det at bruge våbnet</param>
            /// <inheritdoc cref="Sword.AddProjectile(short, int)"/>
            /// <inheritdoc cref="Weapon(int, int, int, SoundStyle, int, int, int, int)"/>
            public Staff(short projectile, int manaCost, int damage, int knockback, int projectileVelocity,int useTime, SoundStyle useSound, int width, int height, int value, int rarity) : base(damage, knockback, useTime, useSound, width, height, value, rarity)
            {
                Projectile = projectile;
                ProjectileVel = projectileVelocity;
                ManaCost = manaCost;
            }
            public short Projectile { get; }
            public int ProjectileVel { get; }
            public int ManaCost { get; }
            // SetStaticDefaults er lidt anderledes fra SetDefaults.
            public override void SetStaticDefaults()
            {
                Item.staff[Type] = true; // Dette gør at spillet ved at det er en stav der affyres. Det bruges til skydeanimationen.
            }
            public override void SetDefaults()
            {
                base.SetDefaults();
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.shoot = Projectile;
                Item.shootSpeed = ProjectileVel;
                Item.mana = ManaCost;
                Item.noMelee = true;
                Item.autoReuse = true;
            }
        }

        /// <summary>
        /// Pickaxe er, godt nok lidt ulogisk, nedarvet MeleeWeapon.
        /// Det er fordi, det ensete der får pickaxe til at skille sig ud fra weapon er dens strength parameter.
        /// Originalt havde vi en Tool klasse på samme niveau som Weapon, men grundet lighederne har vi valgt at skrotte den.
        /// Pickaxe er en den sidste i en gren af nedarvingshierakiet.
        /// </summary>
        public class Pickaxe : MeleeWeapon
        {
            /// <summary>
            /// Pickaxes sekundære constructor.
            /// Her tilføjes Stength.
            /// </summary>
            /// <param name="strenght">Strenght er den styrke hakken har og styre hvad der kan fjernes, og hvor mange slag det tager.</param>
            /// <inheritdoc cref="Weapon(int, int, int, SoundStyle, int, int, int, int)"/>
            public Pickaxe(int strenght, int damage, int knockback, int useTime, SoundStyle useSound, int width, int height, int value, int rarity) : base(damage, knockback, useTime, useSound, width, height, value, rarity)
            {
                Strength = strenght;
            }
            public int Strength { get; }
            // Fordi pickaxe er det sidste led kan man tilføje ting faste værdier som useStyle.
            // ItemUseStyleID kaldes også et magic number. Det er fordi, de egentlig bare kigger i en liste og giver et nummer f.eks. er swing = 1
            // Det gode ved magic numbers er at man ikke behøver at vide hvilket tal swing svare til, man kan bare skrive swing.
            public override void SetDefaults()
            {
                base.SetDefaults();
                Item.pick = Strength;
                Item.autoReuse = true; // autoReuse er om man skal kunne holde musseknappen inde, hvilken man skal på en hakke.
                Item.useStyle = ItemUseStyleID.Swing; // Animationen der skal bruges er også fast, nemlig Swing
                Item.attackSpeedOnlyAffectsWeaponAnimation = true; // Den her gør som navnet antyder.
            }
        }

        /// <summary>
        /// Axe er ligesom pickaxe nedarvet MeleeWeapon.
        /// Axe er ikke en økse der bruges til kamp, men til at hukke træer ned.
        /// Der inkluderes en hammer funktion i Axe. Denne funktion er til at fjerne baggrundsvægge.
        /// </summary>
        public class Axe : MeleeWeapon
        {
            /// <summary>
            /// Den sekundære constructor i Axe.
            /// Tilføjer AxeStrength og HammerStength, tilsvarende stength hos Pickaxe.
            /// </summary>
            /// <param name="axeStrength">Hvor stærk er økse funktionen</param>
            /// <param name="hammerStrength">Hvor stærk er hammer funktionen</param>
            /// <inheritdoc cref="Weapon(int, int, int, SoundStyle, int, int, int, int)"/>
            public Axe(int axeStrength, int hammerStrength, int damage, int knockback, int useTime, SoundStyle useSound, int width, int height, int value, int rarity) : base(damage, knockback, useTime, useSound, width, height, value, rarity)
            {
                AxeStrenght = axeStrength;
                HammerStrenght = hammerStrength;
            }
            public int AxeStrenght { get; }
            public int HammerStrenght { get; }

            public override void SetDefaults() // Det samme som Pickaxe bare med strenght for både økse og hammer.
            {
                base.SetDefaults();
                Item.axe = AxeStrenght;
                Item.hammer = HammerStrenght;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.autoReuse = true;
                Item.attackSpeedOnlyAffectsWeaponAnimation = true;
            }
        }
        /// <summary>
        /// Sword er nedarvet MeleeWeapon og er den sidste i sin række.
        /// </summary>
        public class Sword : MeleeWeapon
        {
            /// <summary>
            /// Den skundære constructor for Sword.
            /// Includere autoReuse som et argument.
            /// </summary>
            /// <param name="autoReuse">Skal man kunne holde musen inde</param>
            /// <inheritdoc cref="Weapon(int, int, int, SoundStyle, int, int, int, int)"/>
            public Sword(int damage, int knockback, bool autoReuse, int useTime, SoundStyle useSound, int width, int height, int value, int rarity) : base(damage, knockback, useTime, useSound, width, height, value, rarity)
            {
                AutoReuse = autoReuse; // Imodsætning til pickaxe og axe, vil man gerne kunne vælge om man kan holde musen inde på et sværd.
            }
            public bool AutoReuse { get; }
            // Her erklæres variabler til hvis sværet skal skyde et projektil.
            public bool HasProjectile = false;
            public short projectile;
            public int ProjectileVel;
            // Vi har kun valgt et sværd, men der er flere i Terraria.
            // Man ville derfor kunne derfor have sværdtyper der nedarves af sværd, men bruger forskellige useStyles.
            public override void SetDefaults()
            {
                base.SetDefaults();
                Item.useStyle = ItemUseStyleID.Swing;
                Item.autoReuse = AutoReuse;
                if (HasProjectile) // Denne kode kører kun hvis man har tilføjet et projektil.
                {
                    Item.shoot = projectile;
                    Item.shootSpeed = ProjectileVel;
                }
            }
            /// <summary>
            /// En brugerdefineret funktion der tilføjer et projektil til sværet.
            /// </summary>
            /// <param name="projectile">Hvilket projektil skal skydes. (Brug ProjectileID magic number)</param>
            /// <param name="projectileVelocity">Hvor hurtigt er projektilet</param>
            public void AddProjectile(short projectile, int projectileVelocity)
            {
                HasProjectile = true; // Gør at koden for projektilet i Swords underkendelse af SetDefault()
                this.projectile = projectile; // Sætter projectile til at være argumentet projectile. 'this.' bruges da navnende er identiske.
                ProjectileVel = projectileVelocity;
            }
        }
        /// <summary>
        /// Bow er den sidste af de fire klasser der er nedarvet weapon.
        /// Ligesom de andre er den enden af nedarvingen.
        /// </summary>
        public class Bow : RangedWeapon
        {
            /// <summary>
            /// Den sekundære constructor of Bow
            /// Heri tilføjes projectile og projectilgeVelocity
            /// </summary>
            /// <inheritdoc cref="Sword.AddProjectile(short, int)"/>
            /// <inheritdoc cref="Weapon(int, int, int, SoundStyle, int, int, int, int)"/>
            public Bow(short projectile, int damage, int knockback, int projectileVelocity, int useTime, SoundStyle useSound, int width, int height, int value, int rarity) : base(damage, knockback, useTime, useSound, width, height, value, rarity)
            {
                Projectile = projectile;
                ProjectileVel = projectileVelocity;
            }

            public short Projectile { get; }
            public int ProjectileVel { get; }
            // Man kan både have en bue der brugger ammunition, eller en bue der ikke gør
            // Som standard vælger vi at den ikke skal bruge ammunition.
            public bool usesAmmo = false; 
            public int AmmoType;
            public override void SetDefaults()
            {
                base.SetDefaults();
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.noMelee = true; // noMe
                Item.shoot = Projectile;
                Item.shootSpeed = ProjectileVel;
                if (usesAmmo) // Kun hvis den bruger ammunition.
                {
                    Item.useAmmo = AmmoType;
                }

            }
            /// <summary>
            /// AddAmmo køres i constructoren af modfilen hvis ens bue skal bruge ammonition
            /// </summary>
            /// <param name="ammoType">Brug AmmoID magic number</param>
            public void AddAmmo(int ammoType)
            {
                usesAmmo = true; // gør at SetDefaults tilføjer ammunition
                AmmoType = ammoType;
            }
        }
        /// <summary>
        /// ArmorPiece er nedarvet CustomItem og derfor på samme niveau i nedarvingshierakiet som Weapon.
        /// </summary>
        public class ArmorPiece : CustomItem
        {
            /// <summary>
            /// Den sekundære constructor for ArmorPiece. Heri indsættes defence argumentet.
            /// </summary>
            /// <param name="defence">Hvor godt blokeres skade.</param>
            /// <inheritdoc cref="CustomItem(int, int, int, int)"/>
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
        /// <summary>
        /// Helmet er nedarvet ArmorPiece og er den mest komlicerede af de 3 stykker rustning.
        /// </summary>
        public class Helmet: ArmorPiece
        {
            /// <summary>
            /// Deb sekundære constructor for Helmet. Der tilføjes ingen nye argumenter her.
            /// </summary>
            /// <inheritdoc cref="ArmorPiece(int, int, int, int, int)"/>
            public Helmet(int defence, int width, int height, int value, int rarity) : base(defence, width, height, value, rarity) { }
            // Dem der lavede terraria har valgt at et armorset skal konstrueres i hjelmen derfor erklæres nødvendige værdier her.
            public bool ArmorSetActive = false;
            public int PairedBreastplate;
            public int PairedLeggings;

            /// <summary>
            /// En brugerdefineret funktion til at skave et armorset.
            /// </summary>
            /// <param name="pairedBreastplate">Brug ModContent.ItemType&lt;BreastplateToPair&gt;()</param>
            /// <param name="pairedLeggings">Brug ModContent.ItemType&lt;LeggingsToPair&gt;()</param>
            public void MakeIntoArmorset(int pairedBreastplate, int pairedLeggings)
            {
                ArmorSetActive = true;
                PairedBreastplate = pairedBreastplate;
                PairedLeggings = pairedLeggings;
                
            }
            public override bool IsArmorSet(Item head, Item body, Item legs)
            {
                if (ArmorSetActive)
                {
                    return body.type == PairedBreastplate && legs.type == PairedLeggings;
                }
                return false;
            }

        }
        /// <summary>
        /// Der er ikke brug for yderligere argumenter til Breastplate, men den laves for continuitets skyld.
        /// </summary>
        public class Breastplate: ArmorPiece
        {
            /// <inheritdoc cref="ArmorPiece(int, int, int, int, int)"/>
            public Breastplate(int defence, int width, int height, int value, int rarity) : base(defence, width, height, value, rarity) { }
        }
        /// <summary>
        /// Der er ikke brug for yderligere argumenter til Leggings, men den laves for continuitets skyld.
        /// </summary>
        public class Leggings : ArmorPiece
        {
            /// <inheritdoc cref="ArmorPiece(int, int, int, int, int)"/>
            public Leggings(int defence, int width, int height, int value, int rarity) : base(defence, width, height, value, rarity) { }
        }
    }
}
