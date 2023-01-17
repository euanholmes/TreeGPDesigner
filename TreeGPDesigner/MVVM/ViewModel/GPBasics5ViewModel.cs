using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using TreeGPDesigner.MVVM.Model;

namespace TreeGPDesigner.MVVM.ViewModel
{
    public partial class GPBasics5ViewModel : ObservableObject
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

        [ObservableProperty]
        private Brush? background = AppInfoSingleton.Instance.CurrentBackground;

        //Commands
        public ICommand NavHomeMenuCommand { get; }
        public ICommand NavTutorialsMenuCommand { get; }
        public ICommand NavNextCommand { get; }
        public ICommand SelectCommand { get; }
        public ICommand RadioButtonCommand { get; }

        //GP Basics 5 Variables
        private List<Node> programs = new();

        private int selectionMethod = 0;

        private BinPackingTemplate bpTemplate = new();

        [ObservableProperty]
        private string selection1Name;

        [ObservableProperty]
        private string selection1Fitness;

        [ObservableProperty]
        private Brush selection1Colour;

        [ObservableProperty]
        private string selection2Name;

        [ObservableProperty]
        private string selection2Fitness;

        [ObservableProperty]
        private Brush selection2Colour;

        [ObservableProperty]
        private string selection3Name;

        [ObservableProperty]
        private string selection3Fitness;

        [ObservableProperty]
        private Brush selection3Colour;

        [ObservableProperty]
        private bool selectEnabled = true;

        [ObservableProperty]
        private string informationText = 
            "Selection is used to determine which programs will be available for mutation and crossover. There are many different " +
            "selection methods, three popular ones are tournament selection, fitness proportionate selection and truncation selection. Tournament selection " +
            "involves using a tournament bracket system to determine each program that will be selected, with the higher fitness program winning each bracket. " +
            "Fitness proportionate selection will assign each program a probability based on their fitness then a roulette system will determine which programs " +
            "are selected. Truncation selection will simply select a percentage of the top fittest programs. These different selection methods provide different " +
            "benefits for example tournament selection will give a higher change that average programs will make it through and increase the populations " +
            "diversity while truncation selection will guarantee that high fitness programs make it through.\n\nTry out these different selection methods to see " +
            "which programs are selected.";


        //Constructor
        public GPBasics5ViewModel()
        {
            NavHomeMenuCommand = new RelayCommand(NavHomeMenu);
            NavTutorialsMenuCommand = new RelayCommand(NavTutorialsMenu);
            NavNextCommand = new RelayCommand(NavNext);
            SelectCommand = new RelayCommand(Select);
            RadioButtonCommand = new RelayCommand<string>(param => RadioButton(param));

            Node program1 = new TerminalNode("1", 0, 1, false);
            program1.Name = "Program 1";
            program1.Fitness = 100;
            program1.NotFailedYet = true;
            programs.Add(program1);

            Node program2 = new TerminalNode("1", 0, 1, false);
            program2.Name = "Program 2";
            program2.Fitness = 98;
            program2.NotFailedYet = true;
            programs.Add(program2);

            Node program3 = new TerminalNode("1", 0, 1, false);
            program3.Name = "Program 3";
            program3.Fitness = 97;
            program3.NotFailedYet = true;
            programs.Add(program3);

            Node program4 = new TerminalNode("1", 0, 1, false);
            program4.Name = "Program 4";
            program4.Fitness = 96;
            program4.NotFailedYet = true;
            programs.Add(program4);

            Node program5 = new TerminalNode("1", 0, 1, false);
            program5.Name = "Program 5";
            program5.Fitness = 94;
            program5.NotFailedYet = false;
            programs.Add(program5);

            Node program6 = new TerminalNode("1", 0, 1, false);
            program6.Name = "Program 6";
            program6.Fitness = 93;
            program6.NotFailedYet = false;
            programs.Add(program6);

            Node program7 = new TerminalNode("1", 0, 1, false);
            program7.Name = "Program 7";
            program7.Fitness = 92;
            program7.NotFailedYet = false;
            programs.Add(program7);

            Node program8 = new TerminalNode("1", 0, 1, false);
            program8.Name = "Program 8";
            program8.Fitness = 83;
            program8.NotFailedYet = false;
            programs.Add(program8);

            Node program9 = new TerminalNode("1", 0, 1, false);
            program9.Name = "Program 9";
            program9.Fitness = 73;
            program9.NotFailedYet = false;
            programs.Add(program9);

            Node program10 = new TerminalNode("1", 0, 1, false);
            program10.Name = "Program 10";
            program10.Fitness = 51;
            program10.NotFailedYet = false;
            programs.Add(program10);
        }

        //Navigation Functions
        public void NavHomeMenu()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new HomeViewModel();
        }

        public void NavTutorialsMenu()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new TutorialsMenuViewModel();
        }

        public void NavNext()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new GPBasics6ViewModel();
        }

        //GP Basics 5 Functions
        public async void Select()
        {
            SelectEnabled = false;
            bpTemplate.Generation = programs;

            if (selectionMethod == 0)
            {
                bpTemplate.TournamentSelection(3);
            }
            else if (selectionMethod == 1)
            {
                bpTemplate.FitnessProportionateSelection(3);
            }
            else
            {
                bpTemplate.TruncationSelection(3);
            }

            Node selection1 = bpTemplate.GeneticFunctionPool[0];
            Node selection2 = bpTemplate.GeneticFunctionPool[1];
            Node selection3 = bpTemplate.GeneticFunctionPool[2];

            Selection1Name = "";
            Selection1Fitness = "";
            Selection1Colour = Brushes.Transparent;
            Selection2Name = "";
            Selection2Fitness = "";
            Selection2Colour = Brushes.Transparent;
            Selection3Name = "";
            Selection3Fitness = "";
            Selection3Colour = Brushes.Transparent;

            await Task.Delay(500);

            Selection1Name = selection1.Name;
            Selection1Fitness = selection1.Fitness.ToString();

            if (selection1.NotFailedYet && selection1.Fitness == 100)
            {
                Selection1Colour = AppInfoSingleton.RainbowBrush;
            }
            else if (selection1.NotFailedYet)
            {
                Selection1Colour = Brushes.Green;
            }
            else
            {
                Selection1Colour = Brushes.Red;
            }

            await Task.Delay(500);

            Selection2Name = selection2.Name;
            Selection2Fitness = selection2.Fitness.ToString();

            if (selection2.NotFailedYet && selection2.Fitness == 100)
            {
                Selection2Colour = AppInfoSingleton.RainbowBrush;
            }
            else if(selection2.NotFailedYet)
            {
                Selection2Colour = Brushes.Green;
            }
            else
            {
                Selection2Colour = Brushes.Red;
            }

            await Task.Delay(500);

            Selection3Name = selection3.Name;
            Selection3Fitness = selection3.Fitness.ToString();

            if (selection3.NotFailedYet && selection3.Fitness == 100)
            {
                Selection3Colour = AppInfoSingleton.RainbowBrush;
            }
            else if(selection3.NotFailedYet)
            {
                Selection3Colour = Brushes.Green;
            }
            else
            {
                Selection3Colour = Brushes.Red;
            }

            SelectEnabled = true;
        }

        public void RadioButton(string selectionMethodString)
        {
            int selectionMethodInt = Convert.ToInt32(selectionMethodString);
            selectionMethod = selectionMethodInt;
        }
    }
}
