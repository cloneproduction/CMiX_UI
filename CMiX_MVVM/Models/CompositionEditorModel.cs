using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.Models 
{
    public class CompositionEditorModel
    {
        public CompositionEditorModel()
        {
            CompositionModels = new List<CompositionModel>();
            SelectedCompositionModel = new CompositionModel();
        }

        public List<CompositionModel> CompositionModels { get; set; }
        public CompositionModel SelectedCompositionModel { get; set; }
    }
}
