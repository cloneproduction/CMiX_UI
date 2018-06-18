using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMiX.Services;

namespace CMiX.ViewModels
{
    public class GeometryRotation :ViewModel
    {
        public GeometryRotation(string layername, IMessenger messenger) 
            : this(
                  layerName: layername,   
                  messenger: messenger,
                  rotationMode: default
                  )
        {
        }

        public GeometryRotation
            (
                string layerName,
                IMessenger messenger,
                GeometryRotationMode rotationMode
            )
        {
            LayerName = layerName;
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        }

        public IMessenger Messenger { get; }

        private string _layerName;
        public string LayerName
        {
            get => _layerName;
            set => SetAndNotify(ref _layerName, value);
        }

        private GeometryRotationMode _RotationMode;
        public GeometryRotationMode RotationMode
        {
            get => _RotationMode;
            set
            {
                SetAndNotify(ref _RotationMode, value);
                Messenger.SendMessage(LayerName + "/" + nameof(RotationMode), RotationMode);
            }
        }
    }
}
