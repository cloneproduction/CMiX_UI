using System;
using CMiX.Services;

namespace CMiX.ViewModels
{
    public class RangeControl : ViewModel
    {
        public RangeControl(IMessenger messenger, string layername)
            : this(
                  range: 0.0,
                  layername: layername,
                  messenger: messenger,
                  modifier: ((RangeModifier)0).ToString()
                  )
        { }

        public RangeControl(
            double range,
            IMessenger messenger,
            string layername,
            string modifier)
        {
            AssertNotNegative(() => range);
            LayerName = layername;
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            Range = range;
            Modifier = modifier;
        }

        private IMessenger Messenger { get; }

        private string _layerName;
        public string LayerName
        {
            get => _layerName;
            set => SetAndNotify(ref _layerName, value);
        }

        private double _range;
        public double Range
        {
            get => _range;
            set
            {
                SetAndNotify(ref _range, CoerceNotNegative(value));
                Messenger.SendMessage(LayerName + "/Range", Range);
            }
        }

        private string _modifier;
        public string Modifier
        {
            get => _modifier;
            set
            {
                SetAndNotify(ref _modifier, value);
                Messenger.SendMessage(LayerName + "/Modifier", Modifier);
            }
        }
    }
}