namespace CMiX.MVVM.Models
{
    public class ProjectModel : IModel
    {
        public ProjectModel()
        {
            CompositionEditorModel = new CompositionEditorModel();
        }

        public string MessageAddress { get; set; }
        public CompositionEditorModel CompositionEditorModel { get; set; }
    }
}