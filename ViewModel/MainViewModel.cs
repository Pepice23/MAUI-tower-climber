﻿
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


        //Floor Monster Text & Progress Bar
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
        int playerDamagePerSecond = 1;

        //Players XP & Progress Bar
        [ObservableProperty]
        int currentXP = 1;

        [ObservableProperty]
        int maxXP = 10;

        [ObservableProperty]
        double playerXpProgress = 0;

        // Background Picture
        [ObservableProperty]
        string backgroundPicture;

        //Player Money
        [ObservableProperty]
        int playerMoney = 0;

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

        [ObservableProperty]
        bool monsterVisible = true;

        [ObservableProperty]
        int monsterLevel = 1;

        [ObservableProperty]
        string monsterAvatar = string.Empty;

        [ObservableProperty]
        int monsterCurrentHP = 1;

        [ObservableProperty]
        int monsterMaxHP = 100;

        [ObservableProperty]
        double monsterHPProgress = 0;

        [RelayCommand]
        void setMonster()
        {
            MonsterVisible = monster.MonsterVisible;
            MonsterLevel = player.Floor;
            MonsterAvatar = monster.SetRandomMonsterAvatar();
            MonsterCurrentHP = monster.CurrentMonsterHP;
            MonsterMaxHP = monster.MonsterMaxHP;
            MonsterHPProgress = (double)MonsterCurrentHP / MonsterMaxHP;
        }

        [RelayCommand]
        void TogglePlayer()
        {
            PlayerVisible = !PlayerVisible;
        }

        [RelayCommand]
        void ToggleMonster()
        {
            MonsterVisible = !MonsterVisible;
        }

        [RelayCommand]
        void AddFloor()
        {
            PlayerFloor++;
            FloorProgress = (double)PlayerFloor / 100;
        }

        [RelayCommand]
        void RemoveFloor()
        {
            PlayerFloor--;
            FloorProgress = (double)PlayerFloor / 100;
        }

        [RelayCommand]
        void AddMonsterCount()
        {
            FloorMonsterCount++;
            MonsterProgress = (double)FloorMonsterCount / 15;
        }

        [RelayCommand]
        void RemoveMonsterCount()
        {
            FloorMonsterCount--;
            MonsterProgress = (double)FloorMonsterCount / 15;
        }



        [RelayCommand]
        async void StartBattle()
        {
            PeriodicTimer timer = new PeriodicTimer(TimeSpan.FromSeconds(1));
            while (await timer.WaitForNextTickAsync())
            {
                MonsterCurrentHP -= PlayerDamagePerSecond;
                MonsterHPProgress = (double)MonsterCurrentHP / MonsterMaxHP;

                if (MonsterCurrentHP <= 0)
                {
                    timer.Dispose();
                    AddMonsterCount();
                    CalculateXP();
                    CheckLevelUp();
                    AddMoneyAfterBattle();
                    setMonster();

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
    }

}

