using System.Windows.Controls;
using System.Windows.Input;

namespace CMiX
{
    class LayerRadioButton : RadioButton
    {
        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            OnToggle();
        }
    }
}
