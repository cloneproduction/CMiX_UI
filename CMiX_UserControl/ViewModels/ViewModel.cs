using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CMiX.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public static class ViewModelExtensions
    {
        public static void SetAndNotify<TObj, TRet>(this TObj self, ref TRet backingField, TRet newValue, [CallerMemberName] string propertyName = null)
            where TObj : ViewModel
        {
            backingField = newValue;
            self.NotifyPropertyChanged(propertyName);
        }
    }
}