using System.Collections.ObjectModel;

namespace CMiX.MVVM.Interfaces
{
    public interface IComponentModel : IModel
    {
        int ID { get; set; }
        string Name { get; set; }
        string MessageAddress { get; set; }
        bool IsVisible { get; set; }
        //BeatModel BeatModel { get; set; }
        //MessageServiceModel MessageServiceModel { get; set; }

        ObservableCollection<IComponentModel> ComponentModels { get; set; }
    }
}