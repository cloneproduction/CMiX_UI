using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels.Components;

namespace CMiX.MVVM.ViewModels
{
    public class Outliner : ViewModel
    {
        public Outliner(ObservableCollection<Component> components)
        {
            OutlinerDragDropManager = new OutlinerDragDropManager();
            Components = components;
        }

        public ObservableCollection<Component> Components { get; set; }


        private OutlinerDragDropManager _outlinerDragDropManager;
        public OutlinerDragDropManager OutlinerDragDropManager
        {
            get => _outlinerDragDropManager;
            set => SetAndNotify(ref _outlinerDragDropManager, value);
        }
    }
}
