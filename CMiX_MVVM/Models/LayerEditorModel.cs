using System.Collections.Generic;

namespace CMiX.MVVM.Models
{
    public class LayerEditorModel
    {
        public LayerEditorModel()
        {
            LayerModels = new List<LayerModel>();
            SelectedLayerModel = new LayerModel();
        }

        public List<LayerModel> LayerModels { get; set; }
        public LayerModel SelectedLayerModel { get; set; }
    }
}