using MvvmDialogs.DialogTypeLocators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.Studio.ViewModels
{
    public class CustomTypeLocator : IDialogTypeLocator
    {
        public Type Locate(INotifyPropertyChanged viewModel)
        {
            Type viewModelType = viewModel.GetType();
            string viewModelTypeName = viewModelType.FullName;
            Console.WriteLine("viewModelTypeName " + viewModelTypeName);
            // Get dialog type name by removing the 'VM' suffix
            //string dialogTypeName = viewModelTypeName.Substring(0, viewModelTypeName.Length - "VM".Length);


            return viewModelType.Assembly.GetType(viewModelTypeName);
            //return viewModelType.Assembly.GetType(dialogTypeName);
        }
    }
}
