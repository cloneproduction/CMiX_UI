using System.Windows;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;
using Memento;
using GongSolutions.Wpf.DragDrop;

namespace CMiX.Studio.ViewModels
{
    public abstract class AssetSelector : ViewModel, ISendable, IUndoable
    {
        public Mementor Mementor { get; set; }
        public string MessageAddress { get; set; }
        public MessageService MessageService { get; set; }




    }
}
