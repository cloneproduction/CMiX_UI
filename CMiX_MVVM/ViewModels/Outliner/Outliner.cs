using System.Collections.ObjectModel;

namespace CMiX.MVVM.ViewModels
{
    public class Outliner : ViewModel
    {
        public Outliner(ObservableCollection<IComponent> components)
        {
            OutlinerDragDropManager = new OutlinerDragDropManager();
            Components = components;
        }

        public ObservableCollection<IComponent> Components { get; set; }


        private OutlinerDragDropManager _outlinerDragDropManager;
        public OutlinerDragDropManager OutlinerDragDropManager
        {
            get => _outlinerDragDropManager;
            set => SetAndNotify(ref _outlinerDragDropManager, value);
        }
    }
}
