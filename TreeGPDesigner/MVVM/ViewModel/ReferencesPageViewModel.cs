using CommunityToolkit.Mvvm.ComponentModel;

namespace TreeGPDesigner.MVVM.ViewModel
{
    //Viewmodel class for References Page View
    public partial class ReferencesPageViewModel : ViewModelBase
    {
        //References Page Variables
        [ObservableProperty]
        private string referencesText = "Sim, K. (2014). Novel Hyper-heuristics Applied to the Domain of Bin Packing. Ph.D. dissertation, Edinburgh Napier University." +
            "\n\nSim, K. GP Bin Packing Java Code + GP Cart Java Code" +
            "\n\nTree Drawing Algorithm - Rachel Lim, C# Implementation of Reingold-Tillford algorithm found here: https://rachel53461.wordpress.com/2014/04/20/algorithm-for-drawing-trees/" +
            "\n\nWPF UI Basics - https://www.youtube.com/watch?v=Vjldip84CXQ&ab_channel=AngelSix" +
            "\n\nWPF Modern UI Design - https://www.youtube.com/watch?v=PzP8mw7JUzI&ab_channel=Payload" +
            "\n\nWPF MVVM Switching Views - https://www.youtube.com/watch?v=N26C_Cq-gAY&ab_channel=SingletonSean" +
            "\n\nWPF MVVM .NET Community Toolkit Nuget package - https://www.youtube.com/watch?v=uVIzK2snugk&ab_channel=KevinBost" +
            "\n\nCreate C# lambda expression from a string with Microsoft.Code.Analysis.Csharp.Scripting Nuget package" +
            " - https://www.strathweb.com/2018/01/easy-way-to-create-a-c-lambda-expression-from-a-string-with-roslyn/";

        //Constructor
        public ReferencesPageViewModel()
        {

        }
    }
}
