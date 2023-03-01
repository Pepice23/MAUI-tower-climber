
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading;

namespace MAUI_tower_climber.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        Weapon item = new(1);
        Player player = new();

        [ObservableProperty]
        int playerLevel = 1;

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
        }

    }
}
