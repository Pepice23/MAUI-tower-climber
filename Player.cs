namespace MAUI_tower_climber
{
    internal class Player
    {
        public bool Visible;
        public int Level;
        public int Money;
        public int PerClickDamage;
        public int PerSecondDamage;
        public int DamageMultiplier;
        public int Floor;
        public int MonsterCount;
        public int CurrentXP;
        public int XPToNextLevel;
        public string Avatar;
        public string BackgroundPicture;

        public Player()
        {
            Visible = true;
            Level = 1;
            Money = 0;
            PerClickDamage = 10;
            PerSecondDamage = 10;
            DamageMultiplier = 1;
            Floor = 1;
            MonsterCount = 0;
            CurrentXP = 0;
            XPToNextLevel = 100;
            Avatar = "hero2.jpeg";
            BackgroundPicture = "bg1.png";
        }
    }
}
