namespace CMiX.MVVM.ViewModels
{
    public class GeometryScale : Sendable
    {
        public GeometryScale() 
        {
            Mode = default;
        }

        private GeometryScaleMode _Mode;
        public GeometryScaleMode Mode
        {
            get => _Mode;
            set
            {
                //if(Mementor != null)
                //    Mementor.PropertyChange(this, nameof(Mode));
                SetAndNotify(ref _Mode, value);
                //SendMessages(MessageAddress + nameof(Mode), Mode);
            }
        }
    }
}