using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using TreeGPDesigner.MVVM.Model;

namespace TreeGPDesigner.MVVM.ViewModel
{
    //Viewmodel class for GP Basics 3 View
    public partial class GPBasics3ViewModel : ViewModelBase
    {
        //GP Basics 3 Variables
        private static Random random = new();

        private BinPackingTemplate bpTemplate = new();

        private List<double> items1 = new();
        private List<double> items2 = new();
        private List<double> items3 = new();
        private List<double> items4 = new();

        private List<List<double>> bins1;
        private List<List<double>> bins2;
        private List<List<double>> bins3;
        private List<List<double>> bins4;

        private ObservableCollection<Bin> binList1 = new();
        private ObservableCollection<Bin> binList2 = new();
        private ObservableCollection<Bin> binList3 = new();
        private ObservableCollection<Bin> binList4 = new();

        [ObservableProperty]
        private int bc1 = random.Next(50, 61);

        [ObservableProperty]
        private int bc2 = random.Next(50, 61);

        [ObservableProperty]
        private int bc3 = random.Next(50, 61);

        [ObservableProperty]
        private int bc4 = random.Next(50, 61);

        [ObservableProperty]
        private string items1String;

        [ObservableProperty]
        private string items2String;

        [ObservableProperty]
        private string items3String;

        [ObservableProperty]
        private string items4String;

        [ObservableProperty]
        private ObservableCollection<Bin> bins = new();

        [ObservableProperty]
        private int currentBinList = 0;

        [ObservableProperty]
        private string informationText = "In GP wrappers can be used to surround the generated program tree and provide it with functionality and data" +
            " common to all programs of this type. For example when generating a bin packing algorithm the wrapper can present to the solution program" +
            " a list of items, bin capacity and current bin weight which can be used in the solution program. When using a wrapper, a dataset might also" +
            " be required. A bin packing dataset would include a list of problems which would each have a bin capacity and a list of items. Note that " +
            "not all GP applications require wrappers, or even datasets if there are only static terminal nodes used in your program trees.\n\n" +
            "Try the offline bin packing wrapper to the right with some randomly generated bin packing problems. The solution program used in the" +
            " wrapper here is the \"first fit descending algorithm\". This algorithm will pack the current item into the first bin it fits into, " +
            "if no bin is found a new a new bin will be opened, and it will be packed into the new bin.";

        //Commands
        public ICommand NavPreviousCommand { get; }
        public ICommand NavNextCommand { get; }
        public ICommand RunWrapperCommand { get; }
        public ICommand RadioButtonCommand { get; }

        //Constructor
        public GPBasics3ViewModel()
        {
            NavPreviousCommand = new RelayCommand(NavPrevious);
            NavNextCommand = new RelayCommand(NavNext);
            RunWrapperCommand = new RelayCommand(RunWrapper);
            RadioButtonCommand = new RelayCommand<string>(param => RadioButton(param));

            items1 = new() { random.Next(10, Bc1), random.Next(10, Bc1), random.Next(10, Bc1), random.Next(10, Bc1), random.Next(10, Bc1), random.Next(10, Bc1), 
            random.Next(1, 10), random.Next(1, 10), random.Next(1, 10), random.Next(1, 10) };
            items2 = new() { random.Next(10, Bc2), random.Next(10, Bc2), random.Next(10, Bc2), random.Next(10, Bc2), random.Next(10, Bc2), random.Next(10, Bc2),
            random.Next(1, 10), random.Next(1, 10), random.Next(1, 10), random.Next(1, 10) };
            items3 = new() { random.Next(10, Bc3), random.Next(10, Bc3), random.Next(10, Bc3), random.Next(10, Bc3), random.Next(10, Bc3), random.Next(10, Bc3),
            random.Next(1, 10), random.Next(1, 10), random.Next(1, 10), random.Next(1, 10) };
            items4 = new() { random.Next(10, Bc4), random.Next(10, Bc4), random.Next(10, Bc4), random.Next(10, Bc4), random.Next(10, Bc4), random.Next(10, Bc4),
            random.Next(1, 10), random.Next(1, 10), random.Next(1, 10), random.Next(1, 10) };

            Items1String = ConvertItemsToString(items1);
            Items2String = ConvertItemsToString(items2);
            Items3String = ConvertItemsToString(items3);
            Items4String = ConvertItemsToString(items4);

            bins1 = bpTemplate.BPOfflineWrapper(items1, Bc1, bpTemplate.KnownAlgorithms[0]);
            bins2 = bpTemplate.BPOfflineWrapper(items2, Bc2, bpTemplate.KnownAlgorithms[0]);
            bins3 = bpTemplate.BPOfflineWrapper(items3, Bc3, bpTemplate.KnownAlgorithms[0]);
            bins4 = bpTemplate.BPOfflineWrapper(items4, Bc4, bpTemplate.KnownAlgorithms[0]);

            GetBinLists();
        }

        //Navigation Functions
        public void NavPrevious()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new GPBasics2ViewModel();
        }
        public void NavNext()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new GPBasics4ViewModel();
        }

        //GP Basics 3 Functions
        public void RunWrapper()
        {
            Bins.Clear();

            if (CurrentBinList == 0)
            {
                Bins = new ObservableCollection<Bin>(binList1);
            }
            else if (CurrentBinList == 1)
            {
                Bins = new ObservableCollection<Bin>(binList2);
            }
            else if (CurrentBinList == 2)
            {
                Bins = new ObservableCollection<Bin>(binList3);
            }
            else
            {
                Bins = new ObservableCollection<Bin>(binList4);
            }
        }

        public void RadioButton(string radioButtonNumString)
        {
            int radioButtonNum = Convert.ToInt32(radioButtonNumString);
            currentBinList = radioButtonNum;
        }

        public void GetBinLists()
        {
            binList1 = GetBinList(bins1);
            binList2 = GetBinList(bins2);
            binList3 = GetBinList(bins3);
            binList4 = GetBinList(bins4);
        }

        public ObservableCollection<Bin> GetBinList(List<List<double>> rawBinList)
        {
            ObservableCollection<Bin> binList = new();

            for (int i = 0; i < rawBinList.Count; i++)
            {
                Bin bin = new();
                bin.TextColour = TextColour;
                for (int j = 0; j < rawBinList[i].Count; j++)
                {
                    bin.BinItems.Add(new BinItem(rawBinList[i][j], bin.TextColour));
                }
                binList.Add(bin);
            }

            return binList;
        }

        public string ConvertItemsToString(List<double> itemsList)
        {
            string itemsString = "";

            for (int i = 0; i < itemsList.Count; i++)
            {
                if (i == 0)
                {
                    itemsString += itemsList[i];
                }
                else
                {
                    itemsString += ", " + itemsList[i];
                }
            }

            return itemsString;
        }
    }

    //Bin item class used in item control in this view.
    public class BinItem
    {
        private double currentBinItem;
        private Brush textColour;

        public BinItem(double currentBinItem, Brush textColour)
        {
            this.currentBinItem = currentBinItem;
            this.textColour = textColour;
        }

        public double CurrentBinItem { get => currentBinItem; set => currentBinItem = value; }
        public Brush TextColour { get => textColour; set => textColour = value; }
    }

    //Bin class used in item control in this view.
    public class Bin
    {
        private List<BinItem> binItems = new();
        private Brush textColour;

        public Bin()
        {

        }

        public Bin(List<BinItem> binItems, Brush textColour)
        {
            this.binItems = binItems;
            this.textColour = textColour;
        }

        public List<BinItem> BinItems { get => binItems; set => binItems = value; }
        public Brush TextColour { get => textColour; set => textColour = value; }
    }
}
