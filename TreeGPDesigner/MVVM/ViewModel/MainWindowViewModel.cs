using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TreeGPDesigner
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string name = "Euan";

        public ICommand ChangeNameCommand { get; }

        public MainWindowViewModel()
        {
            ChangeNameCommand = new RelayCommand(ChangeName);
        }

        private void ChangeName()
        {
            if (Name == "Euan")
            {
                Name = "Holmes";
            }
            else
            {
                Name = "Euan";
            }      
        }
    }
}
