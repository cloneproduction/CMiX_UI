using System.Collections.Generic;

namespace CMiX.MVVM.Models
{
    public class EntityEditorModel
    {
        public EntityEditorModel()
        {
            EntityModels = new List<EntityModel>();
            SelectedEntityModel = new EntityModel();
        }

        public List<EntityModel> EntityModels { get; set; }
        public EntityModel SelectedEntityModel { get; set; }
    }
}