using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace MAUI_tower_climber.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        Weapon item = new(1);
        Player player = new();
        Monster monster = new();
        Random random = new Random();
        PeriodicTimer NormalTimer;
        PeriodicTimer BossTimer;
        bool IsBattleStarted = false;
        int MaxBossTime = 30;
        TimeSpan GameSpeed = TimeSpan.FromSeconds(1);

        // Boss Timer & Progress Bar
        [ObservableProperty]
        int remainingBossTime;

        [ObservableProperty]
        double remainingBossTimeProgress;

        // Total Monster Count
        [ObservableProperty]
        int totalMonsterCount = 0;

        //Floor Monster Text & Progress Bar
        [ObservableProperty]
        bool isBoss = false;

        [ObservableProperty]
        int floorMonsterCount = 0;

        [ObservableProperty]
        double monsterProgress = 0;

        //Floor Text & Progress Bar
        [ObservableProperty]
        int playerFloor = 1;

        [ObservableProperty]
        double floorProgress = 0;

        //Player Visibility & Avatar
        [ObservableProperty]
        bool playerVisible = true;

        [ObservableProperty]
        string playerAvatar = string.Empty;

        // Level
        [ObservableProperty]
        int playerLevel = 1;

        //Players Damage
        [ObservableProperty]
        int playerDamagePerClick = 1;

        [ObservableProperty]
        int playerDamagePerSecond;  

        //Players XP & Progress Bar
        [ObservableProperty]
        int currentXP;

        [ObservableProperty]
        int maxXP;

        [ObservableProperty]
        double playerXpProgress;

        // Background Picture
        [ObservableProperty]
        string backgroundPicture;

        //Player Money
        [ObservableProperty]
        int playerMoney;

        [RelayCommand]
        void SetPlayer()
        {
            PlayerLevel = player.Level;
            PlayerAvatar = player.Avatar;
            PlayerDamagePerClick = player.PerClickDamage;
            PlayerDamagePerSecond = player.PerSecondDamage;
            CurrentXP = player.CurrentXP;
            MaxXP = player.XPToNextLevel;
            PlayerXpProgress = (double)CurrentXP / MaxXP;
            PlayerVisible = player.Visible;
            BackgroundPicture = player.BackgroundPicture;
        }

        // Monster Visibility & Avatar
        [ObservableProperty]
        bool monsterVisible = true;

        [ObservableProperty]
        string monsterAvatar = string.Empty;

        // Monster HP
        [ObservableProperty]
        int monsterCurrentHP;

        [ObservableProperty]
        int monsterMaxHP;

        [ObservableProperty]
        double monsterHPProgress;

        [ObservableProperty]
        string outcome = string.Empty;

        [RelayCommand]
        void SetMonster()
        {
            MonsterVisible = true;
            MonsterAvatar = monster.SetRandomMonsterAvatar();
            SetMonsterHP();
            MonsterHPProgress = (double)MonsterCurrentHP / MonsterMaxHP;
            RemainingBossTime = MaxBossTime;
        }

        void SetMonsterHP()
        {
            MonsterMaxHP = (int)(Math.Pow(monster.BaseMonsterHP, TotalMonsterCount) * 100);
            MonsterCurrentHP = MonsterMaxHP;
        }

        void AddFloor()
        {
            PlayerFloor++;
            FloorProgress = (double)PlayerFloor / 100;
        }

        void AddMonsterCount()
        {
            FloorMonsterCount++;
            MonsterProgress = (double)FloorMonsterCount / 15;
        }

        void ResetMonsterCount()
        {
            FloorMonsterCount = 1;
            MonsterProgress = (double)FloorMonsterCount / 15;
        }

        [RelayCommand]
        async void StartBattle()
        {
            IsBattleStarted = true;
            if (FloorMonsterCount == 15)
            {
                IsBoss = true;
                BossTimer = new PeriodicTimer(GameSpeed);
                while (await BossTimer.WaitForNextTickAsync())
                {
                    RemainingBossTime--;
                    RemainingBossTimeProgress = (double)RemainingBossTime / MaxBossTime;
                    MonsterCurrentHP -= PlayerDamagePerSecond;
                    MonsterHPProgress = (double)MonsterCurrentHP / MonsterMaxHP;

                    if (MonsterCurrentHP <= 0 && RemainingBossTime > 0)
                    {
                        BossTimer.Dispose();
                        PlayerWins();
                    }
                    if (RemainingBossTime <= 0 && MonsterCurrentHP > 0)
                    {
                        BossTimer.Dispose();
                        PlayerLoses();
                    }
                }
                SetMonster();
            }
            if (FloorMonsterCount < 15)
            {
                IsBoss = false;
                NormalTimer = new PeriodicTimer(GameSpeed);
                while (await NormalTimer.WaitForNextTickAsync())
                {
                    MonsterCurrentHP -= PlayerDamagePerSecond;
                    MonsterHPProgress = (double)MonsterCurrentHP / MonsterMaxHP;

                    if (MonsterCurrentHP <= 0)
                    {
                        NormalTimer.Dispose();
                        PlayerWins();
                    }
                }
                SetMonster();
                if (IsBattleStarted)
                {
                    StartBattle();
                }
            }
        }

        void AddMoneyAfterBattle()
        {
            PlayerMoney += random.Next(PlayerFloor * FloorMonsterCount, PlayerFloor * FloorMonsterCount * 2);
        }

        void CheckLevelUp()
        {
            if (CurrentXP >= MaxXP)
            {
                PlayerLevel++;
                CurrentXP -= MaxXP;
                MaxXP = (int)(MaxXP * 1.15);
                PlayerXpProgress = (double)CurrentXP / MaxXP;
            }
        }

        void CalculateXP()
        {
            double XPPercent = random.Next(5, 11);
            double XPbase = XPPercent / 100;
            double XP = XPbase * MaxXP;
            CurrentXP += (int)XP;
            PlayerXpProgress = (double)CurrentXP / MaxXP;

        }

        void ChangeBackground()
        {
            int randomNumber = random.Next(1, 59);
            BackgroundPicture = $"bg{randomNumber}.png";
        }

        void CheckNextFloor()
        {
            if (FloorMonsterCount > 15)
            {
                AddFloor();
                ResetMonsterCount();
                // TODO: get new weapon
                ChangeBackground();
            }
        }

        void CheckPlayerGetsLoot()
        {
            int randomNumber = random.Next(1, 101);
            if (randomNumber <= 20)
            {
                Debug.WriteLine("Player gets new loot");
            }
        }

        void PlayerWins()
        {
            MonsterVisible = false;
            Outcome = "You win"!;
            AddMonsterCount();
            TotalMonsterCount++;
            CalculateXP();
            CheckLevelUp();
            CheckNextFloor();
            CheckPlayerGetsLoot();
            AddMoneyAfterBattle();
            //Todo check game end
        }

        void PlayerLoses()
        {
            PlayerVisible = false;
            Outcome = "You Lose!";
            ResetMonsterCount();
            TotalMonsterCount = PlayerFloor - 1 * 15 + 1;
        }

        [RelayCommand]
        void StopTimer()
        {
            NormalTimer.Dispose();
            IsBattleStarted = false;
        }

        [RelayCommand]
        void NormalSpeed()
        {
            GameSpeed = TimeSpan.FromSeconds(1);
        }

        [RelayCommand]
        void TwoSpeed()
        {
            GameSpeed = TimeSpan.FromMilliseconds(500);
        }

        [RelayCommand]
        void FourSpeed()
        {
            GameSpeed = TimeSpan.FromMilliseconds(250);
        }
    }

}

