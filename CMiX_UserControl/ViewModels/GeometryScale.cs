using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMiX.Services;

namespace CMiX.ViewModels
{
    public class GeometryScale : ViewModel
    {
        public GeometryScale(string layername, IMessenger messenger)
            : this(
                  layerName: layername,
                  messenger: messenger,
                  scaleMode: default
                  )
        {
        }

        public GeometryScale
            (
                string layerName,
                IMessenger messenger,
                GeometryScaleMode scaleMode
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

        private GeometryScaleMode _ScaleMode;
        public GeometryScaleMode ScaleMode
        {
            get => _ScaleMode;
            set
            {
                SetAndNotify(ref _ScaleMode, value);
                Messenger.SendMessage(LayerName + "/" + nameof(ScaleMode), ScaleMode);
            }
        }
    }
}
