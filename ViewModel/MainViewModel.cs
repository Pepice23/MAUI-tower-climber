
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading;

namespace MAUI_tower_climber.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        Weapon item = new(1);
        Player player = new();
        Monster monster = new();

        [ObservableProperty]
        int playerLevel = 1;

        [ObservableProperty]
        int playerFloor = 1;

        [ObservableProperty]
        string playerAvatar = string.Empty;

        [ObservableProperty]
        int playerDamagePerClick = 1;

        [ObservableProperty]
        int playerDamagePerSecond = 1;

        [ObservableProperty]
        int currentXP = 1;

        [ObservableProperty]
        int maxXP = 10;

        [ObservableProperty]
        double playerXpProgress = 0;

        [ObservableProperty]
        bool playerVisible = true;

        [ObservableProperty]
        double floorProgress = 0;

        [ObservableProperty]
        double monsterProgress = 0;

        [ObservableProperty]
        int monsterCount = 0;

        /// <summary>
        /// Player Observables
        /// </summary>

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
            MonsterCount++;
            MonsterProgress = (double)MonsterCount / 15;
        }

        [RelayCommand]
        void RemoveMonsterCount()
        {
            MonsterCount--;
            MonsterProgress = (double)MonsterCount / 15;
        }
    }
}
