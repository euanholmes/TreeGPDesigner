using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media;

namespace TreeGPDesigner.MVVM.ViewModel
{
    //Viewmodel class for Select Training Data View
    public partial class GPR5SelectTrainingDataViewModel : ViewModelBase
    {
        //Select Dataset Variables
        private List<string> datasetsUI = AppInfoSingleton.Instance.CurrentTemplate.DatasetUI;

        private List<bool> currentDatasets = AppInfoSingleton.Instance.CurrentTemplate.CurrentDatasets;

        [ObservableProperty]
        private List<SelectDataset> datasets = new List<SelectDataset>();

        [ObservableProperty]
        private string errorMessage = "";

        //Commands
        public ICommand NavNextCommand { get; }
        public ICommand NavBackCommand { get; }

        //Constructor
        public GPR5SelectTrainingDataViewModel()
        {
            NavNextCommand = new RelayCommand(NavNext);
            NavBackCommand = new RelayCommand(NavBack);

            GetDatasets();
        }

        //Navigation Functions
        public void NavNext()
        {
            bool noDatasets = true;

            foreach(bool dataset in AppInfoSingleton.Instance.CurrentTemplate.CurrentDatasets)
            {
                if (dataset)
                {
                    noDatasets = false;
                    break;
                }
            }

            if (noDatasets)
            {
                ErrorMessage = "*Select a Dataset.";
            }
            else
            {
                AppInfoSingleton.Instance.CurrentViewModel = new GPR6SelectTreeGrowingMethodViewModel();
            }
        }

        public void NavBack()
        {
            AppInfoSingleton.Instance.CurrentViewModel = new GPR4SelectNodesViewModel();
        }

        //Select Dataset Functions
        public void GetDatasets()
        {
            for (int i = 0; i < datasetsUI.Count; i++)
            {
                Datasets.Add(new SelectDataset(datasetsUI[i], Background, TextColour, NormalButtonColour, i.ToString(), currentDatasets[i], this));
            }
        }
    }

    //Class used for item control in this view.
    public class SelectDataset
    {
        private string? datasetInfo;
        private Brush? backgroundColour;
        private Brush? textColour;
        private Brush? normalButtonColour;
        private string? checkBoxCommandParameter;
        private bool? isSelected;
        private ICommand checkBoxCommand;
        private GPR5SelectTrainingDataViewModel selectDatasetVM;

        public SelectDataset(string? datasetInfo, Brush? backgroundColour, Brush? textColour, Brush? normalButtonColour, string? checkBoxCommandParameter, bool? isSelected, GPR5SelectTrainingDataViewModel selectDatasetVM)
        {
            this.datasetInfo = datasetInfo;
            this.backgroundColour = backgroundColour;
            this.textColour = textColour;
            this.normalButtonColour = normalButtonColour;
            this.checkBoxCommandParameter = checkBoxCommandParameter;
            this.isSelected = isSelected;
            this.CheckBoxCommand = new RelayCommand<string>(param => CheckBox(param));
            this.selectDatasetVM = selectDatasetVM;
        }

        public string? DatasetInfo { get => datasetInfo; set => datasetInfo = value; }
        public Brush? BackgroundColour { get => backgroundColour; set => backgroundColour = value; }
        public Brush? TextColour { get => textColour; set => textColour = value; }
        public Brush? NormalButtonColour { get => normalButtonColour; set => normalButtonColour = value; }
        public string? CheckBoxCommandParameter { get => checkBoxCommandParameter; set => checkBoxCommandParameter = value; }
        public bool? IsSelected { get => isSelected; set => isSelected = value; }
        public ICommand CheckBoxCommand { get => checkBoxCommand; set => checkBoxCommand = value; }
        public GPR5SelectTrainingDataViewModel SelectDatasetVM { get => selectDatasetVM; set => selectDatasetVM = value; }

        public void CheckBox(string datasetNumString)
        {
            selectDatasetVM.ErrorMessage = "";
            int datasetNumInt = Convert.ToInt32(datasetNumString);

            if (AppInfoSingleton.Instance.CurrentTemplate.CurrentDatasets[datasetNumInt] == false)
            {
                AppInfoSingleton.Instance.CurrentTemplate.CurrentDatasets[datasetNumInt] = true;
            }
            else
            {
                AppInfoSingleton.Instance.CurrentTemplate.CurrentDatasets[datasetNumInt] = false;
            }
        }
    }
}
