using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace EksamenProjekt
{
    internal class Toolbox
    {
        public class Weapon(int damage, int width, int height, int useTime, int value, int rare, SoundStyle useSound)
        {
            public int Damage { get; } = damage;
            public int Width { get; } = width;
            public int Height { get; } = height;
            public int UseSpeed { get; } = useTime;
            public int Value { get; } = value;
            public int Rarity { get; } = rare;
            public SoundStyle Sound { get; } = useSound;

            public void AddIngredients(Recipe recipe, params (short, int)[] ingredients)
            {
                foreach ((short, int) ingredient in ingredients)
                {
                    recipe.AddIngredient(ingredient.Item1, ingredient.Item2);
                }
            }
        }
        public class Sword(int damage, int width, int height, int useTime, int value, int rare, SoundStyle useSound, int knockback, bool Reuse) : Weapon(damage, width, height, useTime, value, rare, useSound)
        {
            public bool HasProjectile = false;
            public short projectile;
            public int ProjectileSpeed;
            public void InitiateDefaults(ModItem item)
            {
                item.Item.damage = Damage;
                item.Item.DamageType = DamageClass.Melee;
                item.Item.useStyle = ItemUseStyleID.Swing;
                item.Item.width = Width;
                item.Item.height = Height;
                item.Item.useTime = UseSpeed;
                item.Item.useAnimation = UseSpeed;
                item.Item.knockBack = knockback;
                item.Item.value = Value;
                item.Item.rare = Rarity;
                item.Item.UseSound = Sound;
                item.Item.autoReuse = Reuse;
                if (HasProjectile == true)
                {
                    item.Item.shoot = projectile;
                    item.Item.shootSpeed = ProjectileSpeed;
                }
            }
            public void AddProjectile(short projectile, int shootSpeed)
            {
                HasProjectile = true;
                this.projectile = projectile;
                ProjectileSpeed = shootSpeed;
            }
        }
        public class Bow(int damage, int width, int height, int useTime, int value, int rare, SoundStyle useSound, short projectile, int shootSpeed, int Knockback = 0) : Weapon(damage, width, height, useTime, value, rare, useSound) 
        {
            public bool usesAmmo = false;
            public int AmmoType;
            public void InitiateDefaults(ModItem item)
            {
                item.Item.damage = Damage;
                item.Item.DamageType = DamageClass.Ranged;
                item.Item.useStyle = ItemUseStyleID.Shoot;
                item.Item.noMelee = true;
                item.Item.width = Width;
                item.Item.height = Height;
                item.Item.useTime = UseSpeed;
                item.Item.useAnimation = UseSpeed;
                item.Item.value = Value;
                item.Item.rare = Rarity;
                item.Item.UseSound = Sound;
                item.Item.shoot = projectile;
                item.Item.shootSpeed = shootSpeed;
                item.Item.knockBack = Knockback;
                if (usesAmmo == true)
                {
                    item.Item.useAmmo = AmmoType;
                }
            }

            public void AddAmmo(int ammoType)
            {
                usesAmmo = true;
                AmmoType = ammoType;
            }
        }

    }
}
