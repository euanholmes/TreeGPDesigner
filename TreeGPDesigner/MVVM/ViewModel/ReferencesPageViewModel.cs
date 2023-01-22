using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace TreeGPDesigner.MVVM.ViewModel
{
    public partial class ReferencesPageViewModel : ObservableObject
    {
        //Common Variables
        [ObservableProperty]
        private Brush? textColour = AppInfoSingleton.Instance.CurrentText;

        [ObservableProperty]
        private Brush? normalButtonColour = AppInfoSingleton.Instance.CurrentNormalButtonColor;

        [ObservableProperty]
        private Brush? navButtonColour = AppInfoSingleton.Instance.CurrentNavButtonColor;

        [ObservableProperty]
        private Brush? panel1Colour = AppInfoSingleton.Instance.CurrentPanel1Color;

        [ObservableProperty]
        private Brush? panel2Colour = AppInfoSingleton.Instance.CurrentPanel2Color;

        //References Page Variables
        [ObservableProperty]
        private string referencesText = "Sim, K. (2014). Novel Hyper-heuristics Applied to the Domain of Bin Packing. Ph.D. dissertation, Edinburgh Napier University." +
            "\n\nSim, K. GP Bin Packing Java Code + GP Cart Java Code" +
            "\n\nTree Drawing Algorithm - Rachel Lim, C# Implementation of Reingold-Tillford algorithm found here: https://rachel53461.wordpress.com/2014/04/20/algorithm-for-drawing-trees/";

        //Commands
        public ICommand NavHomeMenuCommand { get; }

        //Constructor
        public ReferencesPageViewModel()
        {
            NavHomeMenuCommand = new RelayCommand(NavHomeMenu);
        }

        //Navigation Functions
        public void NavHomeMenu()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new HomeViewModel();
        }
    }
}
