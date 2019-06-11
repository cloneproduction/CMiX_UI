using CMiX.Services;
using Memento;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CMiX.ViewModels
{
    public class ColorSelector : ViewModel
    {
        public ColorSelector(string messageaddress, ObservableCollection<OSCMessenger> oscmessengers, Mementor mementor) : base(oscmessengers, mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(ColorSelector));
            SelectedColor = new Color();
        }

        private Color _selectedcolor;
        public Color SelectedColor
        {
            get { return _selectedcolor; }
            set
            {
                if (Mementor != null)
                    Mementor.PropertyChange(this, "SelectedColor");
                SetAndNotify(ref _selectedcolor, value);
                //SendMessages(MessageAddress + nameof(SelectedColor), SelectedColor);
            }
        }
    }
}
