namespace MAUI_tower_climber
{
    public class Monster
    {
        public int CurrentMonsterHP;
        public int MonsterMaxHP;
        public string MonsterAvatar;
        public bool MonsterVisible;
        public int TotalMonsterCount;
        double BaseMonsterHP;

        public Monster()
        {
            MonsterVisible = true;
            CurrentMonsterHP = 100;
            TotalMonsterCount = 1;
            BaseMonsterHP = 1.02;
            SetMonsterHP();
            MonsterAvatar = SetRandomMonsterAvatar();
        }

        Random random = new Random();

        public string SetRandomMonsterAvatar()
        {
            int randomNumber = random.Next(1, 26);
            return MonsterAvatar = $"enemy{randomNumber}.jpeg";
        }

        void SetMonsterHP()
        {
            MonsterMaxHP = (int)Math.Pow(BaseMonsterHP, TotalMonsterCount) * 100;
            CurrentMonsterHP = MonsterMaxHP;
        }

    }


}
