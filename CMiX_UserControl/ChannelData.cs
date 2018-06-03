using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CMiX
{
    [Serializable]
    public class ChannelData : INotifyPropertyChanged, ICloneable
    {
        public ChannelData()
        {

        }

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, e);
        }
        
        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        public object Clone()
        {
            return this.MemberwiseClone();
        }


        public CounterViewModel SpriteCounterViewModel { get; }
        public CounterViewModel MaskCounterViewModel { get; }


        #region Properties
        string _Name = "Layer0";
        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value; OnPropertyChanged("Name");
                }
            }
        }

        bool _EnabledLayer = true;
        public bool EnabledLayer
        {
            get { return _EnabledLayer; }
            set
            {
                if (_EnabledLayer != value)
                {
                    _EnabledLayer = value; OnPropertyChanged("EnabledLayer");
                }
            }
        }

        double _MasterPeriod = 60.0;
        public double MasterPeriod
        {
            get { return _MasterPeriod; }
            set
            {
                if (_MasterPeriod != value)
                {
                    _MasterPeriod = value; OnPropertyChanged("MasterPeriod");
                }
            }
        }

        double _LayerPeriod = 60.0;
        public double LayerPeriod
        {
            get { return _LayerPeriod; }
            set
            {
                if (_LayerPeriod != value)
                {
                    _LayerPeriod = value; OnPropertyChanged("LayerPeriod");
                }
            }
        }

        double _Alpha = 0.0;
        public double Alpha
        {
            get { return _Alpha; }
            set
            {
                if (_Alpha != value)
                {
                    _Alpha = value; OnPropertyChanged("Alpha");
                }
            }
        }

        bool _OutputLayer = false;
        public bool OutputLayer
        {
            get { return _OutputLayer; }
            set
            {
                if (_OutputLayer != value)
                {
                    _OutputLayer = value; OnPropertyChanged("OutputLayer");
                }
            }
        }

        string _BlendMode = "Normal";
        public string BlendMode
        {
            get { return _BlendMode; }
            set
            {
                if (_BlendMode != value)
                {
                    _BlendMode = value; OnPropertyChanged("BlendMode");
                }
            }
        }

        string _ChannelMultiplier = "1";
        public string ChannelMultiplier
        {
            get { return _ChannelMultiplier; }
            set
            {
                if (_ChannelMultiplier != value)
                {
                    _ChannelMultiplier = value; OnPropertyChanged("ChannelMultiplier");
                }
            }
        }

        string _SpriteMultiplier = "1";
        public string SpriteMultiplier
        {
            get { return _SpriteMultiplier; }
            set
            {
                if (_SpriteMultiplier != value)
                {
                    _SpriteMultiplier = value; OnPropertyChanged("SpriteMultiplier");
                }
            }
        }

        double _SpriteChanceToHit = 1.0;
        public double SpriteChanceToHit
        {
            get { return _SpriteChanceToHit; }
            set
            {
                if (_SpriteChanceToHit != value)
                {
                    _SpriteChanceToHit = value; OnPropertyChanged("SpriteChanceToHit");
                }
            }
        }

        string _MaskMultiplier = "1";
        public string MaskMultiplier
        {
            get { return _MaskMultiplier; }
            set
            {
                if (_MaskMultiplier != value)
                {
                    _MaskMultiplier = value; OnPropertyChanged("MaskMultiplier");
                }
            }
        }

        double _MaskChanceToHit = 1.0;
        public double MaskChanceToHit
        {
            get { return _MaskChanceToHit; }
            set
            {
                if (_MaskChanceToHit != value)
                {
                    _MaskChanceToHit = value; OnPropertyChanged("MaskChanceToHit");
                }
            }
        }

        double _SpriteBrightness = 0.0;
        public double SpriteBrightness
        {
            get { return _SpriteBrightness; }
            set
            {
                if (_SpriteBrightness != value)
                {
                    _SpriteBrightness = value; OnPropertyChanged("SpriteBrightness");
                }
            }
        }

        double _MaskBrightness = 0.0;
        public double MaskBrightness
        {
            get { return _MaskBrightness; }
            set
            {
                if (_MaskBrightness != value)
                {
                    _MaskBrightness = value; OnPropertyChanged("MaskBrightness");
                }
            }
        }

        double _SpriteContrast = 0.0;
        public double SpriteContrast
        {
            get { return _SpriteContrast; }
            set
            {
                if (_SpriteContrast != value)
                {
                    _SpriteContrast = value; OnPropertyChanged("SpriteContrast");
                }
            }
        }

        double _SpriteKeying = 0.0;
        public double SpriteKeying
        {
            get { return _SpriteKeying; }
            set
            {
                if (_SpriteKeying != value)
                {
                    _SpriteKeying = value; OnPropertyChanged("SpriteKeying");
                }
            }
        }

        double _MaskContrast = 0.0;
        public double MaskContrast
        {
            get { return _MaskContrast; }
            set
            {
                if (_MaskContrast != value)
                {
                    _MaskContrast = value; OnPropertyChanged("MaskContrast");
                }
            }
        }

        double _SpriteSaturation = 0.0;
        public double SpriteSaturation
        {
            get { return _SpriteSaturation; }
            set
            {
                if (_SpriteSaturation != value)
                {
                    _SpriteSaturation = value; OnPropertyChanged("SpriteSaturation");
                }
            }
        }

        double _MaskSaturation = 0.0;
        public double MaskSaturation
        {
            get { return _MaskSaturation; }
            set
            {
                if (_MaskSaturation != value)
                {
                    _MaskSaturation = value; OnPropertyChanged("MaskSaturation");
                }
            }
        }

        double _MaskKeying = 0.0;
        public double MaskKeying
        {
            get { return _MaskKeying; }
            set
            {
                if (_MaskKeying != value)
                {
                    _MaskKeying = value; OnPropertyChanged("MaskKeying");
                }
            }
        }

        double _SpriteInvert = 0.0;
        public double SpriteInvert
        {
            get { return _SpriteInvert; }
            set
            {
                if (_SpriteInvert != value)
                {
                    _SpriteInvert = value; OnPropertyChanged("SpriteInvert");
                }
            }
        }

        double _MaskInvert = 0.0;
        public double MaskInvert
        {
            get { return _MaskInvert; }
            set
            {
                if (_MaskInvert != value)
                {
                    _MaskInvert = value; OnPropertyChanged("MaskInvert");
                }
            }
        }

        double _SpriteFeedBack = 0.0;
        public double SpriteFeedBack
        {
            get { return _SpriteFeedBack; }
            set
            {
                if (_SpriteFeedBack != value)
                {
                    _SpriteFeedBack = value; OnPropertyChanged("SpriteFeedBack");
                }
            }
        }

        double _MaskFeedBack = 0.0;
        public double MaskFeedBack
        {
            get { return _MaskFeedBack; }
            set
            {
                if (_MaskFeedBack != value)
                {
                    _MaskFeedBack = value; OnPropertyChanged("MaskFeedBack");
                }
            }
        }

        double _SpriteBlur = 0.0;
        public double SpriteBlur
        {
            get { return _SpriteBlur; }
            set
            {
                if (_SpriteBlur != value)
                {
                    _SpriteBlur = value; OnPropertyChanged("SpriteBlur");
                }
            }
        }

        double _MaskBlur = 0.0;
        public double MaskBlur
        {
            get { return _MaskBlur; }
            set
            {
                if (_MaskBlur != value)
                {
                    _MaskBlur = value; OnPropertyChanged("MaskBlur");
                }
            }
        }

        ObservableCollection<ListBoxFileName> _SpriteSelectedTex = new ObservableCollection<ListBoxFileName>();
        public ObservableCollection<ListBoxFileName> SpriteSelectedTex
        {
            get { return _SpriteSelectedTex; }
            set
            {
                if (_SpriteSelectedTex != value)
                {
                    _SpriteSelectedTex = value; OnPropertyChanged("SpriteSelectedTex");
                }
            }
        }

        ObservableCollection<ListBoxFileName> _SpriteSelectedGeom = new ObservableCollection<ListBoxFileName>();
        public ObservableCollection<ListBoxFileName> SpriteSelectedGeom
        {
            get { return _SpriteSelectedGeom; }
            set
            {
                if (_SpriteSelectedGeom != value)
                {
                    _SpriteSelectedGeom = value; OnPropertyChanged("SpriteSelectedGeom");
                }
            }
        }

        ObservableCollection<ListBoxFileName> _MaskSelectedTex = new ObservableCollection<ListBoxFileName>();
        public ObservableCollection<ListBoxFileName> MaskSelectedTex
        {
            get { return _MaskSelectedTex; }
            set
            {
                if (_MaskSelectedTex != value)
                {
                    _MaskSelectedTex = value; OnPropertyChanged("MaskSelectedTex");
                }
            }
        }

        ObservableCollection<ListBoxFileName> _MaskSelectedGeom = new ObservableCollection<ListBoxFileName>();
        public ObservableCollection<ListBoxFileName> MaskSelectedGeom
        {
            get { return _MaskSelectedGeom; }
            set
            {
                if (_MaskSelectedGeom != value)
                {
                    _MaskSelectedGeom = value; OnPropertyChanged("MaskSelectedGeom");
                }
            }
        }


        string _SpriteCount = "1";
        public string SpriteCount
        {
            get { return _SpriteCount; }
            set
            {
                if (_SpriteCount != value)
                {
                    _SpriteCount = value; OnPropertyChanged("SpriteCount");
                }
            }
        }

        string _SpriteSelectedTranslate = "STD_CTR";
        public string SpriteSelectedTranslate
        {
            get { return _SpriteSelectedTranslate; }
            set
            {
                if (_SpriteSelectedTranslate != value)
                {
                    _SpriteSelectedTranslate = value; OnPropertyChanged("SpriteSelectedTranslate");
                }
            }
        }

        string _SpriteSelectedScale = "STD_CTR";
        public string SpriteSelectedScale
        {
            get { return _SpriteSelectedScale; }
            set
            {
                if (_SpriteSelectedScale != value)
                {
                    _SpriteSelectedScale = value; OnPropertyChanged("SpriteSelectedScale");
                }
            }
        }

        ObservableCollection<string> _SpriteRotation = new ObservableCollection<string> { "STD_CTR", "True", "True", "True" };
        public ObservableCollection<string> SpriteRotation
        {
            get { return _SpriteRotation; }
            set
            {
                if (_SpriteRotation != value)
                {
                    _SpriteRotation = value; OnPropertyChanged("SpriteRotationAxis");
                }
            }
        }

        string _SpriteTransform = "NONE";
        public string SpriteTransform
        {
            get { return _SpriteTransform; }
            set
            {
                if (_SpriteTransform != value)
                {
                    _SpriteTransform = value; OnPropertyChanged("SpriteTransform");
                }
            }
        }

        string _SpriteViewTransform = "STD_CTR";
        public string SpriteViewTransform
        {
            get { return _SpriteViewTransform; }
            set
            {
                if (_SpriteViewTransform != value)
                {
                    _SpriteViewTransform = value; OnPropertyChanged("SpriteViewTransform");
                }
            }
        }

        double _SpriteTranslate = 0.0;
        public double SpriteTranslate
        {
            get { return _SpriteTranslate; }
            set
            {
                if (_SpriteTranslate != value)
                {
                    _SpriteTranslate = value; OnPropertyChanged("SpriteTranslate");
                }
            }
        }

        double _SpriteScale = 0.0;
        public double SpriteScale
        {
            get { return _SpriteScale; }
            set
            {
                if (_SpriteScale != value)
                {
                    _SpriteScale = value; OnPropertyChanged("SpriteScale");
                }
            }
        }

        double _SpriteRotate = 0.0;
        public double SpriteRotate
        {
            get { return _SpriteRotate; }
            set
            {
                if (_SpriteRotate != value)
                {
                    _SpriteRotate = value; OnPropertyChanged("SpriteRotate");
                }
            }
        }

        string _SpriteSelectedInvert = "INV_RGB";
        public string SpriteSelectedInvert
        {
            get { return _SpriteSelectedInvert; }
            set
            {
                if (_SpriteSelectedInvert != value)
                {
                    _SpriteSelectedInvert = value; OnPropertyChanged("SpriteSelectedInvert");
                }
            }
        }

        bool _SpriteAspectRatio = false;
        public bool SpriteAspectRatio
        {
            get { return _SpriteAspectRatio; }
            set
            {
                if (_SpriteAspectRatio != value)
                {
                    _SpriteAspectRatio = value; OnPropertyChanged("SpriteAspectRatio");
                }
            }
        }

        bool _Sprite3D = false;
        public bool Sprite3D
        {
            get { return _Sprite3D; }
            set
            {
                if (_Sprite3D != value)
                {
                    _Sprite3D = value; OnPropertyChanged("Sprite3D");
                }
            }
        }

        string _MaskCount = "1";
        public string MaskCount
        {
            get { return _MaskCount; }
            set
            {
                if (_MaskCount != value)
                {
                    _MaskCount = value; OnPropertyChanged("MaskCount");
                }
            }
        }

        string _MaskSelectedTranslate = "STD_CTR";
        public string MaskSelectedTranslate
        {
            get { return _MaskSelectedTranslate; }
            set
            {
                if (_MaskSelectedTranslate != value)
                {
                    _MaskSelectedTranslate = value; OnPropertyChanged("MaskSelectedTranslate");
                }
            }
        }

        double _MaskTranslate = 0.0;
        public double MaskTranslate
        {
            get { return _MaskTranslate; }
            set
            {
                if (_MaskTranslate != value)
                {
                    _MaskTranslate = value; OnPropertyChanged("MaskTranslate");
                }
            }
        }

        string _MaskSelectedScale = "STD_CTR";
        public string MaskSelectedScale
        {
            get { return _MaskSelectedScale; }
            set
            {
                if (_MaskSelectedScale != value)
                {
                    _MaskSelectedScale = value; OnPropertyChanged("MaskSelectedScale");
                }
            }
        }

        double _MaskScale = 0.0;
        public double MaskScale
        {
            get { return _MaskScale; }
            set
            {
                if (_MaskScale != value)
                {
                    _MaskScale = value; OnPropertyChanged("MaskScale");
                }
            }
        }

        ObservableCollection<string> _MaskRotation = new ObservableCollection<string> { "STD_CTR", "True", "True", "True" };
        public ObservableCollection<string> MaskRotation
        {
            get { return _MaskRotation; }
            set
            {
                if (_MaskRotation != value)
                {
                    _MaskRotation = value; OnPropertyChanged("MaskRotation");
                }
            }
        }

        double _MaskRotate = 0.0;
        public double MaskRotate
        {
            get { return _MaskRotate; }
            set
            {
                if (_MaskRotate != value)
                {
                    _MaskRotate = value; OnPropertyChanged("MaskRotate");
                }
            }
        }

        string _MaskSelectedInvert = "INV_RGB";
        public string MaskSelectedInvert
        {
            get { return _MaskSelectedInvert; }
            set
            {
                if (_MaskSelectedInvert != value)
                {
                    _MaskSelectedInvert = value; OnPropertyChanged("MaskSelectedInvert");
                }
            }
        }

        string _MaskTransform = "NONE";
        public string MaskTransform
        {
            get { return _MaskTransform; }
            set
            {
                if (_MaskTransform != value)
                {
                    _MaskTransform = value; OnPropertyChanged("MaskTransform");
                }
            }
        }

        string _MaskViewTransform = "STD_CTR";
        public string MaskViewTransform
        {
            get { return _MaskViewTransform; }
            set
            {
                if (_MaskViewTransform != value)
                {
                    _MaskViewTransform = value; OnPropertyChanged("MaskViewTransform");
                }
            }
        }

        bool _MaskAspectRatio = false;
        public bool MaskAspectRatio
        {
            get { return _MaskAspectRatio; }
            set
            {
                if (_MaskAspectRatio != value)
                {
                    _MaskAspectRatio = value; OnPropertyChanged("MaskAspectRatio");
                }
            }
        }

        bool _Mask3D = false;
        public bool Mask3D
        {
            get { return _Mask3D; }
            set
            {
                if (_Mask3D != value)
                {
                    _Mask3D = value; OnPropertyChanged("Mask3D");
                }
            }
        }

        bool _MaskOn = false;
        public bool MaskOn
        {
            get { return _MaskOn; }
            set
            {
                if (_MaskOn != value)
                {
                    _MaskOn = value; OnPropertyChanged("MaskOn");
                }
            }
        }

        bool _MaskKeepOriginal = false;
        public bool MaskKeepOriginal
        {
            get { return _MaskKeepOriginal; }
            set
            {
                if (_MaskKeepOriginal != value)
                {
                    _MaskKeepOriginal = value; OnPropertyChanged("MaskKeepOriginal");
                }
            }
        }

        #region GSFX
        ObservableCollection<ListBoxFileName> _SelectedGSFXTex = new ObservableCollection<ListBoxFileName>();
        public ObservableCollection<ListBoxFileName> SelectedGSFXTex
        {
            get { return _SelectedGSFXTex; }
            set
            {
                if (_SelectedGSFXTex != value)
                {
                    _SelectedGSFXTex = value; OnPropertyChanged("SelectedGSFXTex");
                }
            }
        }

        string _GSFXTranslateName = "STD_CTR";
        public string GSFXTranslateName
        {
            get { return _GSFXTranslateName; }
            set
            {
                if (_GSFXTranslateName != value)
                {
                    _GSFXTranslateName = value; OnPropertyChanged("GSFXTranslateName");
                }
            }
        }

        string _GSFXScaleName = "STD_CTR";
        public string GSFXScaleName
        {
            get { return _GSFXScaleName; }
            set
            {
                if (_GSFXScaleName != value)
                {
                    _GSFXScaleName = value; OnPropertyChanged("GSFXScaleName");
                }
            }
        }

        string _GSFXRotateName = "STD_CTR";
        public string GSFXRotateName
        {
            get { return _GSFXRotateName; }
            set
            {
                if (_GSFXRotateName != value)
                {
                    _GSFXRotateName = value; OnPropertyChanged("GSFXRotateName");
                }
            }
        }

        double _GSFXTranslate = 0.0;
        public double GSFXTranslate
        {
            get { return _GSFXTranslate; }
            set
            {
                if (_GSFXTranslate != value)
                {
                    _GSFXTranslate = value; OnPropertyChanged("GSFXTranslate");
                }
            }
        }

        double _GSFXScale = 0.0;
        public double GSFXScale
        {
            get { return _GSFXScale; }
            set
            {
                if (_GSFXScale != value)
                {
                    _GSFXScale = value; OnPropertyChanged("GSFXScale");
                }
            }
        }

        double _GSFXRotate = 0.0;
        public double GSFXRotate
        {
            get { return _GSFXRotate; }
            set
            {
                if (_GSFXRotate != value)
                {
                    _GSFXRotate = value; OnPropertyChanged("GSFXRotate");
                }
            }
        }
        #endregion

        string _Color = "#FFFFFFFF";
        public string Color
        {
            get { return _Color; }
            set
            {
                if (_Color != value)
                {
                    _Color  = value; OnPropertyChanged("Color");
                }

            }
        }

        string _BgColor = "#FFFFFFFF";
        public string BgColor
        {
            get { return _BgColor; }
            set
            {
                if (_BgColor != value)
                {
                    _BgColor = value; OnPropertyChanged("BgColor");
                }
            }
        }

        string _HueName = "STD_CTR";
        public string HueName
        {
            get { return _HueName; }
            set
            {
                if (_HueName != value)
                {
                    _HueName = value; OnPropertyChanged("HueName");
                }
            }
        }

        string _SatName = "STD_CTR";
        public string SatName
        {
            get { return _SatName; }
            set
            {
                if (_SatName != value)
                {
                    _SatName = value; OnPropertyChanged("SatName");
                }
            }
        }

        string _ValName = "STD_CTR";
        public string ValName
        {
            get { return _ValName; }
            set
            {
                if (_ValName != value)
                {
                    _ValName = value; OnPropertyChanged("ValName");
                }
            }
        }

        double _HueRange = 0.0;
        public double HueRange
        {
            get { return _HueRange; }
            set
            {
                if (_HueRange != value)
                {
                    _HueRange = value; OnPropertyChanged("HueRange");
                }
            }
        }

        double _SatRange = 0.0;
        public double SatRange
        {
            get { return _SatRange; }
            set
            {
                if (_SatRange != value)
                {
                    _SatRange = value; OnPropertyChanged("SatRange");
                }
            }
        }

        double _ValRange = 0.0;
        public double ValRange
        {
            get { return _ValRange; }
            set
            {
                if (_ValRange != value)
                {
                    _ValRange = value; OnPropertyChanged("ValRange");
                }
            }
        }
        #endregion
    }
}