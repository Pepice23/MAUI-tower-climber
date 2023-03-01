
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading;

namespace MAUI_tower_climber.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        ItemCreator item = new(1);


        [ObservableProperty]
        double monsterProgressHP = 1;

        [ObservableProperty]
        double monsterCurrentHPText = 100;

        [ObservableProperty]
        int monsterFullHPText = 100;



        [RelayCommand]
        void PlayerAttack()
        {
            Console.WriteLine(item);
            MonsterCurrentHPText -= 10;
            MonsterProgressHP = MonsterCurrentHPText / MonsterFullHPText;
        }



    }
}
