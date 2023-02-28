using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI_tower_climber
{
    internal class Item
    {
        public string Name { get; set; }
        public int Quality { get; set; }
        public int Level { get; set; }
        public int PerClickDamage { get; set; }
        public int PerSecondDamage { get; set; }
        public int Price { get; set; }
        public string PicturePath { get; set; }


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

        void GetRandomRarity()
        {
            DiceRoll();
            if (roll <= 40)
            {
                this.Quality = (int)Rarity.Poor;
                this.Name = "Rusty Weapon";
            }
            if (roll > 40 && roll <= 75)
            {
                this.Quality = (int)Rarity.Uncommon;
                this.Name = "Regular Weapon";
            }
            if (roll > 75 && roll <= 85)
            {
                this.Quality = (int)Rarity.Rare;
                this.Name = "Fine Weapon";
            }
            if (roll > 85 && roll <= 98)
            {
                this.Quality = (int)Rarity.Epic;
                this.Name = "Master Weapon";
            }
            if (roll > 98)
            {
                this.Quality = (int)Rarity.Legendary;
                this.Name = "Legendary Weapon";
            }
        }

        void GetRandomStat(int level, int Quality)
        {
            this.PerClickDamage = level * Quality + roll;
            this.PerSecondDamage = level * Quality + roll;
        }

        public void GenerateRandomItem(int Level)
        {
            GetRandomRarity();
            this.Level = Level;
            GetRandomStat(Level, Quality);
            this.Price = Quality * Level;
            this.PicturePath = "";
        }
    }
}
