
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MAUI_tower_climber.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        double monsterProgressHP = 1;

        [ObservableProperty]
        double monsterCurrentHPText = 100;

        [ObservableProperty]
        int monsterFullHPText = 100;



        [RelayCommand]
        void PlayerAttack()
        {
            MonsterCurrentHPText -= 10;
            MonsterProgressHP = MonsterCurrentHPText / MonsterFullHPText;
        }
    }
}
