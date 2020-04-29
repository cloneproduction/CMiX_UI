using Memento;
using GongSolutions.Wpf.DragDrop;
using System.Windows;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;

namespace CMiX.Studio.ViewModels
{
    public class AssetPathSelector<T> : ViewModel, IUndoable, IDropTarget
    {
        public AssetPathSelector(string messageAddress, MessengerService messengerService, Mementor mementor)
        {
            MessageAddress = $"{messageAddress}/";
            MessengerService = messengerService;
            Mementor = mementor;
        }

        public Mementor Mementor { get; set; }
        public string MessageAddress { get; set; }
        public MessengerService MessengerService { get; set; }

        private string _selectedPath;
        public string SelectedPath
        {
            get => _selectedPath;
            set => SetAndNotify(ref _selectedPath, value);
        }

        public void DragOver(IDropInfo dropInfo)
        {
            if (dropInfo.DragInfo != null)
            {
                if (dropInfo.DragInfo.SourceItem != null && dropInfo.DragInfo.SourceItem is T)
                {
                    dropInfo.Effects = DragDropEffects.Copy;
                }
            }
        }

        public void Drop(IDropInfo dropInfo)
        {
            SelectedPath = ((IAssets)dropInfo.DragInfo.SourceItem).Path;
        }

        public void SetViewModel(AssetPathSelectorModel model)
        {
            MessengerService.Disable();

            if (model.SelectedPath != null)
                SelectedPath = model.SelectedPath;

            MessengerService.Enable();
        }
    }
}