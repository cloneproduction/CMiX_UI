using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class None : Sendable, IAnimMode
    {
        public None()
        {

        }

        public void Update()
        {
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {

        }
    }
}