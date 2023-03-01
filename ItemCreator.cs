using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI_tower_climber
{
    internal class ItemCreator
    {
        public string Name;
        public int Quality;
        public int Level;
        public int PerClickDamage;
        public int PerSecondDamage;
        public int Price;
        public string PicturePath;

        public ItemCreator(int Level)
        {
            Quality = GetRandomRarity();
            Name = GetWeaponName(Quality);
            this.Level = Level;
            PerClickDamage = GetRandomStat(Level, Quality);
            PerSecondDamage = GetRandomStat(Level, Quality);
            Price = Level * Quality;
            PicturePath = GetRandomImage();
        }

        Random random = new Random();
        int roll;


        enum Rarity
        {
            Poor = 1,
            Uncommon,
            Rare,
            Epic,
            Legendary
        }

        void DiceRoll()
        {
            roll = random.Next(1, 101);
        }

        int GetRandomRarity()
        {
            DiceRoll();
            if (roll <= 40)
            {
                return Quality = (int)Rarity.Poor;
            }
            if (roll > 40 && roll <= 75)
            {
                return Quality = (int)Rarity.Uncommon;
            }
            if (roll > 75 && roll <= 85)
            {
                return Quality = (int)Rarity.Rare;
            }
            if (roll > 85 && roll <= 98)
            {
                return Quality = (int)Rarity.Epic;
            }
            if (roll > 98)
            {
                return Quality = (int)Rarity.Legendary;
            }
            return 1;
        }

        string GetWeaponName(int Quality)
        {
            if (Quality == 1)
            {
                return Name = "Rusty Weapon";
            }
            if (Quality == 2)
            {
                return Name = "Regular Weapon";
            }
            if (Quality == 3)
            {
                return Name = "Fine Weapon";
            }
            if (Quality == 4)
            {
                return Name = "Master Weapon";
            }
            if (Quality == 5)
            {
                return Name = "Legendary Weapon";
            }
            return Name;
        }

        int GetRandomStat(int level, int Quality)
        {
            return level * Quality + roll;
        }

        string GetRandomImage()
        {
            int RandomNumber = random.Next(1, 16);
            return $"weapon{RandomNumber}.jpeg";
        }
    }
}
