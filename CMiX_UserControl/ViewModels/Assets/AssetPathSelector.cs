using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;
using Memento;
using System;
using GongSolutions.Wpf.DragDrop;
using System.Windows;
using CMiX.MVVM.Models;

namespace CMiX.Studio.ViewModels
{
    public class AssetPathSelector : ViewModel, ISendable, IUndoable, IDropTarget
    {
        public AssetPathSelector(string messageAddress, MessageService messageService, Mementor mementor)
        {
            MessageAddress = $"{messageAddress}/";
            MessageService = messageService;
            Mementor = mementor;
        }

        public Mementor Mementor { get; set; }
        public string MessageAddress { get; set; }
        public MessageService MessageService { get; set; }

        private string _selectedPath;
        public string SelectedPath
        {
            get => _selectedPath;
            set
            {
                SetAndNotify(ref _selectedPath, value);
                Console.WriteLine("SelectedPath is " + value);
            }
        }

        public void DragOver(IDropInfo dropInfo)
        {
            if (dropInfo.DragInfo != null)
            {
                if (dropInfo.DragInfo.SourceItem != null && dropInfo.DragInfo.SourceItem is IAssets)
                {
                    dropInfo.Effects = DragDropEffects.Copy;
                }
            }
        }

        public void Drop(IDropInfo dropInfo)
        {
            SelectedPath = ((IAssets)dropInfo.DragInfo.SourceItem).Path;
        }

        public AssetPathSelectorModel GetModel()
        {
            AssetPathSelectorModel model = new AssetPathSelectorModel();

            if (SelectedPath != null)
                model.SelectedPath = this.SelectedPath;

            return model;
        }

        public void SetViewModel(AssetPathSelectorModel model)
        {
            MessageService.Disable();

            if (model.SelectedPath != null)
                SelectedPath = model.SelectedPath;

            MessageService.Enable();
        }
    }
}
