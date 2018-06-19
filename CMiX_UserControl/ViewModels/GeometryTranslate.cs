using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMiX.Services;

namespace CMiX.ViewModels
{
    public class GeometryTranslate : ViewModel
    {
        public GeometryTranslate(string layername, IMessenger messenger)
            : this(
                  layerName: layername,
                  messenger: messenger,
                  translateMode: default
                  )
        {
        }

        public GeometryTranslate
            (
                string layerName,
                IMessenger messenger,
                GeometryTranslateMode translateMode
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

        private GeometryTranslateMode _TranslateMode;
        public GeometryTranslateMode TranslateMode
        {
            get => _TranslateMode;
            set
            {
                SetAndNotify(ref _TranslateMode, value);
                Messenger.SendMessage(LayerName + "/" + nameof(TranslateMode), TranslateMode);
            }
        }
    }
}
