namespace CMiX.MVVM.Models
{
    public class ComboBoxModel<T> : Model
    {
        public ComboBoxModel()
        {

        }
        public T Selection { get; set; }
    }
}