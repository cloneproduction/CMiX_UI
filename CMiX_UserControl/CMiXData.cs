using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CMiX
{
    [Serializable]
    public class CMiXData : INotifyPropertyChanged
    {
        public CMiXData()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private ObservableCollection<ChannelData> _ChData = new ObservableCollection<ChannelData> { new ChannelData { Name = "Layer0" }};
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<ChannelData> ChData
        {
            get { return _ChData; }
            set { _ChData = value; }
        }

        private ObservableCollection<double> _MasterPeriod = new ObservableCollection<double>(new double[] { 0.0 });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<double> MasterPeriod
        {
            get { return _MasterPeriod; }
            set { _MasterPeriod = value; }
        }

        private ObservableCollection<double> _ChannelBeatMultiplier = new ObservableCollection<double>(new double[] { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<double> ChannelBeatMultiplier
        {
            get { return _ChannelBeatMultiplier; }
            set { _ChannelBeatMultiplier = value; }
        }

        private ObservableCollection<double> _ChannelSpriteMultiplier = new ObservableCollection<double>(new double[] { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<double> ChannelSpriteMultiplier
        {
            get { return _ChannelSpriteMultiplier; }
            set { _ChannelSpriteMultiplier = value; }
        }

        private ObservableCollection<double> _ChannelSpriteChanceToHit = new ObservableCollection<double>(new double[] { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<double> ChannelSpriteChanceToHit
        {
            get { return _ChannelSpriteChanceToHit; }
            set { _ChannelSpriteChanceToHit = value; }
        }

        private ObservableCollection<double> _ChannelMaskMultiplier = new ObservableCollection<double>(new double[] { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<double> ChannelMaskMultiplier
        {
            get { return _ChannelMaskMultiplier; }
            set { _ChannelMaskMultiplier = value; }
        }

        private ObservableCollection<double> _ChannelMaskChanceToHit = new ObservableCollection<double>(new double[] { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<double> ChannelMaskChanceToHit
        {
            get { return _ChannelMaskChanceToHit; }
            set { _ChannelMaskChanceToHit = value; }
        }

        private ObservableCollection<double> _PostFXBeatMultiplier = new ObservableCollection<double>(new double[] { 1.0 });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<double> PostFXBeatMultiplier
        {
            get { return _PostFXBeatMultiplier; }
            set { _PostFXBeatMultiplier = value; }
        }

        private ObservableCollection<double> _CamBeatMultiplier = new ObservableCollection<double>(new double[] { 1.0 });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<double> CamBeatMultiplier
        {
            get { return _CamBeatMultiplier; }
            set { _CamBeatMultiplier = value; }
        }

        private ObservableCollection<double> _CamChanceToHit= new ObservableCollection<double>(new double[] { 1.0 });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<double> CamChanceToHit
        {
            get { return _CamChanceToHit; }
            set { _CamChanceToHit = value; }
        }

        private ObservableCollection<ObservableCollection<ListBoxFileName>> _ChannelSelectedSpriteTex = new ObservableCollection<ObservableCollection<ListBoxFileName>>( new[] { new ObservableCollection<ListBoxFileName>(), new ObservableCollection<ListBoxFileName>(), new ObservableCollection<ListBoxFileName>(), new ObservableCollection<ListBoxFileName>(), new ObservableCollection<ListBoxFileName>(), new ObservableCollection<ListBoxFileName>() } );
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<ObservableCollection<ListBoxFileName>> ChannelSelectedSpriteTex
        {
            get { return _ChannelSelectedSpriteTex; }
            set { _ChannelSelectedSpriteTex = value; }
        }

        private ObservableCollection<ObservableCollection<ListBoxFileName>> _ChannelSelectedSpriteGeom = new ObservableCollection<ObservableCollection<ListBoxFileName>>(new[] { new ObservableCollection<ListBoxFileName>(), new ObservableCollection<ListBoxFileName>(), new ObservableCollection<ListBoxFileName>(), new ObservableCollection<ListBoxFileName>(), new ObservableCollection<ListBoxFileName>(), new ObservableCollection<ListBoxFileName>() });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<ObservableCollection<ListBoxFileName>> ChannelSelectedSpriteGeom
        {
            get { return _ChannelSelectedSpriteGeom; }
            set { _ChannelSelectedSpriteGeom = value; }
        }

        private ObservableCollection<ObservableCollection<ListBoxFileName>> _ChannelSelectedMaskTex = new ObservableCollection<ObservableCollection<ListBoxFileName>>(new[] { new ObservableCollection<ListBoxFileName>(), new ObservableCollection<ListBoxFileName>(), new ObservableCollection<ListBoxFileName>(), new ObservableCollection<ListBoxFileName>(), new ObservableCollection<ListBoxFileName>(), new ObservableCollection<ListBoxFileName>() });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<ObservableCollection<ListBoxFileName>> ChannelSelectedMaskTex
        {
            get { return _ChannelSelectedMaskTex; }
            set { _ChannelSelectedMaskTex = value; }
        }

        private ObservableCollection<ObservableCollection<ListBoxFileName>> _ChannelSelectedMaskGeom = new ObservableCollection<ObservableCollection<ListBoxFileName>>(new[] { new ObservableCollection<ListBoxFileName>(), new ObservableCollection<ListBoxFileName>(), new ObservableCollection<ListBoxFileName>(), new ObservableCollection<ListBoxFileName>(), new ObservableCollection<ListBoxFileName>(), new ObservableCollection<ListBoxFileName>() });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<ObservableCollection<ListBoxFileName>> ChannelSelectedMaskGeom
        {
            get { return _ChannelSelectedMaskGeom; }
            set { _ChannelSelectedMaskGeom = value; }
        }

        private ObservableCollection<string> _ChannelSpriteCount = new ObservableCollection<string>(new[] { "1", "1", "1", "1", "1", "1" });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<string> ChannelSpriteCount
        {
            get { return _ChannelSpriteCount; }
            set { _ChannelSpriteCount = value; }
        }
        
        private ObservableCollection<string> _ChannelMaskCount = new ObservableCollection<string>(new[] { "1", "1", "1", "1", "1", "1" });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<string> ChannelMaskCount
        {
            get { return _ChannelMaskCount; }
            set { _ChannelMaskCount = value; }
        }

        private ObservableCollection<string> _ChannelSpriteTranslateName = new ObservableCollection<string>(new string[] { "STD_CTR", "STD_CTR", "STD_CTR", "STD_CTR", "STD_CTR", "STD_CTR" });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<string> ChannelSpriteTranslateName
        {
            get { return _ChannelSpriteTranslateName; }
            set { _ChannelSpriteTranslateName = value; }
        }

        private ObservableCollection<string> _ChannelSpriteScaleName = new ObservableCollection<string>(new string[] { "STD_CTR", "STD_CTR", "STD_CTR", "STD_CTR", "STD_CTR", "STD_CTR" });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<string> ChannelSpriteScaleName
        {
            get { return _ChannelSpriteScaleName; }
            set { _ChannelSpriteScaleName = value; }
        }

        private ObservableCollection<string> _ChannelSpriteRotateName = new ObservableCollection<string>(new string[] { "STD_CTR", "STD_CTR", "STD_CTR", "STD_CTR", "STD_CTR", "STD_CTR" });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<string> ChannelSpriteRotateName
        {
            get { return _ChannelSpriteRotateName; }
            set { _ChannelSpriteRotateName = value; }
        }

        private ObservableCollection<string> _ChannelMaskTranslateName = new ObservableCollection<string>(new string[] { "STD_CTR", "STD_CTR", "STD_CTR", "STD_CTR", "STD_CTR", "STD_CTR" });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<string> ChannelMaskTranslateName
        {
            get { return _ChannelMaskTranslateName; }
            set { _ChannelMaskTranslateName = value; }
        }

        private ObservableCollection<string> _ChannelMaskScaleName = new ObservableCollection<string>(new string[] { "STD_CTR", "STD_CTR", "STD_CTR", "STD_CTR", "STD_CTR", "STD_CTR" });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<string> ChannelMaskScaleName
        {
            get { return _ChannelMaskScaleName; }
            set { _ChannelMaskScaleName = value; }
        }

        private ObservableCollection<string> _ChannelMaskRotateName = new ObservableCollection<string>(new string[] { "STD_CTR", "STD_CTR", "STD_CTR", "STD_CTR", "STD_CTR", "STD_CTR" });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<string> ChannelMaskRotateName
        {
            get { return _ChannelMaskRotateName; }
            set { _ChannelMaskRotateName = value; }
        }

        private ObservableCollection<string> _ChannelHueName = new ObservableCollection<string>(new string[] { "STD_CTR", "STD_CTR", "STD_CTR", "STD_CTR", "STD_CTR", "STD_CTR" });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<string> ChannelHueName
        {
            get { return _ChannelHueName; }
            set { _ChannelHueName = value; }
        }

        private ObservableCollection<string> _ChannelSatName = new ObservableCollection<string>(new string[] { "STD_CTR", "STD_CTR", "STD_CTR", "STD_CTR", "STD_CTR", "STD_CTR" });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<string> ChannelSatName
        {
            get { return _ChannelSatName; }
            set { _ChannelSatName = value; }
        }

        private ObservableCollection<string> _ChannelValName = new ObservableCollection<string>(new string[] { "STD_CTR", "STD_CTR", "STD_CTR", "STD_CTR", "STD_CTR", "STD_CTR" });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<string> ChannelValName
        {
            get { return _ChannelValName; }
            set { _ChannelValName = value; }
        }

        private ObservableCollection<bool> _ChannelMaskOn = new ObservableCollection<bool>(new bool[] { false, false, false, false, false, false });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<bool> ChannelMaskOn
        {
            get { return _ChannelMaskOn; }
            set { _ChannelMaskOn = value; }
        }

        private ObservableCollection<bool> _ChannelSprite3D = new ObservableCollection<bool>(new bool[] { false, false, false, false, false, false });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<bool> ChannelSprite3D
        {
            get { return _ChannelSprite3D; }
            set { _ChannelSprite3D = value; }
        }

        private ObservableCollection<bool> _ChannelMask3D = new ObservableCollection<bool>(new bool[] { false, false, false, false, false, false });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<bool> ChannelMask3D
        {
            get { return _ChannelMask3D; }
            set { _ChannelMask3D = value; }
        }

        private ObservableCollection<bool> _ChannelSpriteAspectRatio = new ObservableCollection<bool>(new bool[] { false, false, false, false, false, false });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<bool> ChannelSpriteAspectRatio
        {
            get { return _ChannelSpriteAspectRatio; }
            set { _ChannelSpriteAspectRatio = value; }
        }

        private ObservableCollection<bool> _ChannelMaskAspectRatio = new ObservableCollection<bool>(new bool[] { false, false, false, false, false, false });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<bool> ChannelMaskAspectRatio
        {
            get { return _ChannelMaskAspectRatio; }
            set { _ChannelMaskAspectRatio = value; }
        }

        private ObservableCollection<double> _ChannelSpriteTranslate = new ObservableCollection<double>(new double[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<double> ChannelSpriteTranslate
        {
            get { return _ChannelSpriteTranslate; }
            set { _ChannelSpriteTranslate = value; }
        }

        private ObservableCollection<double> _ChannelSpriteScale = new ObservableCollection<double>(new double[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<double> ChannelSpriteScale
        {
            get { return _ChannelSpriteScale; }
            set { _ChannelSpriteScale = value; }
        }

        private ObservableCollection<double> _ChannelSpriteRotate = new ObservableCollection<double>(new double[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<double> ChannelSpriteRotate
        {
            get { return _ChannelSpriteRotate; }
            set { _ChannelSpriteRotate = value; }
        }

        private ObservableCollection<double> _ChannelMaskTranslate = new ObservableCollection<double>(new double[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<double> ChannelMaskTranslate
        {
            get { return _ChannelMaskTranslate; }
            set { _ChannelMaskTranslate = value; }
        }

        private ObservableCollection<double> _ChannelMaskScale = new ObservableCollection<double>(new double[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<double> ChannelMaskScale
        {
            get { return _ChannelMaskScale; }
            set { _ChannelMaskScale = value; }
        }

        private ObservableCollection<double> _ChannelMaskRotate = new ObservableCollection<double>(new double[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<double> ChannelMaskRotate
        {
            get { return _ChannelMaskRotate; }
            set { _ChannelMaskRotate = value; }
        }


        #region GSFX
        private ObservableCollection<ObservableCollection<ListBoxFileName>> _ChannelSelectedGSFXTex = new ObservableCollection<ObservableCollection<ListBoxFileName>>(new[] { new ObservableCollection<ListBoxFileName>(), new ObservableCollection<ListBoxFileName>(), new ObservableCollection<ListBoxFileName>(), new ObservableCollection<ListBoxFileName>(), new ObservableCollection<ListBoxFileName>(), new ObservableCollection<ListBoxFileName>() });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<ObservableCollection<ListBoxFileName>> ChannelSelectedGSFXTex
        {
            get { return _ChannelSelectedGSFXTex; }
            set { _ChannelSelectedGSFXTex = value; }
        }

        private ObservableCollection<double> _ChannelGSFXTranslate = new ObservableCollection<double>(new double[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<double> ChannelGSFXTranslate
        {
            get { return _ChannelGSFXTranslate; }
            set { _ChannelGSFXTranslate = value; }
        }

        private ObservableCollection<double> _ChannelGSFXScale = new ObservableCollection<double>(new double[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<double> ChannelGSFXScale
        {
            get { return _ChannelGSFXScale; }
            set { _ChannelGSFXScale = value; }
        }

        private ObservableCollection<double> _ChannelGSFXRotate = new ObservableCollection<double>(new double[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<double> ChannelGSFXRotate
        {
            get { return _ChannelGSFXRotate; }
            set { _ChannelGSFXRotate = value; }
        }

        private ObservableCollection<string> _ChannelGSFXTranslateName = new ObservableCollection<string>(new string[] { "STD_CTR", "STD_CTR", "STD_CTR", "STD_CTR", "STD_CTR", "STD_CTR" });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<string> ChannelGSFXTranslateName
        {
            get { return _ChannelGSFXTranslateName; }
            set { _ChannelGSFXTranslateName = value; }
        }

        private ObservableCollection<string> _ChannelGSFXScaleName = new ObservableCollection<string>(new string[] { "STD_CTR", "STD_CTR", "STD_CTR", "STD_CTR", "STD_CTR", "STD_CTR" });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<string> ChannelGSFXScaleName
        {
            get { return _ChannelGSFXScaleName; }
            set { _ChannelGSFXScaleName = value; }
        }

        private ObservableCollection<string> _ChannelGSFXRotateName = new ObservableCollection<string>(new string[] { "STD_CTR", "STD_CTR", "STD_CTR", "STD_CTR", "STD_CTR", "STD_CTR" });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<string> ChannelGSFXRotateName
        {
            get { return _ChannelGSFXRotateName; }
            set { _ChannelGSFXRotateName = value; }
        }
        #endregion

        private ObservableCollection<double> _ChannelSpriteBrightness = new ObservableCollection<double>(new double[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<double> ChannelSpriteBrightness
        {
            get { return _ChannelSpriteBrightness; }
            set { _ChannelSpriteBrightness = value; }
        }

        private ObservableCollection<double> _ChannelMaskBrightness = new ObservableCollection<double>(new double[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<double> ChannelMaskBrightness
        {
            get { return _ChannelMaskBrightness; }
            set { _ChannelMaskBrightness = value; }
        }

        private ObservableCollection<double> _ChannelSpriteContrast = new ObservableCollection<double>(new double[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<double> ChannelSpriteContrast
        {
            get { return _ChannelSpriteContrast; }
            set { _ChannelSpriteContrast = value; }
        }

        private ObservableCollection<double> _ChannelMaskContrast = new ObservableCollection<double>(new double[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<double> ChannelMaskContrast
        {
            get { return _ChannelMaskContrast; }
            set { _ChannelMaskContrast = value; }
        }

        private ObservableCollection<double> _ChannelSpriteSaturation = new ObservableCollection<double>(new double[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<double> ChannelSpriteSaturation
        {
            get { return _ChannelSpriteSaturation; }
            set { _ChannelSpriteSaturation = value; }
        }

        private ObservableCollection<double> _ChannelMaskSaturation = new ObservableCollection<double>(new double[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<double> ChannelMaskSaturation
        {
            get { return _ChannelMaskSaturation; }
            set { _ChannelMaskSaturation = value; }
        }

        private ObservableCollection<double> _ChannelSpriteInvert = new ObservableCollection<double>(new double[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<double> ChannelSpriteInvert
        {
            get { return _ChannelSpriteInvert; }
            set { _ChannelSpriteInvert = value; }
        }

        private ObservableCollection<double> _ChannelMaskInvert = new ObservableCollection<double>(new double[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<double> ChannelMaskInvert
        {
            get { return _ChannelMaskInvert; }
            set { _ChannelMaskInvert = value; }
        }

        private ObservableCollection<double> _ChannelSpriteFeedBack = new ObservableCollection<double>(new double[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<double> ChannelSpriteFeedBack
        {
            get { return _ChannelSpriteFeedBack; }
            set { _ChannelSpriteFeedBack = value; }
        }

        private ObservableCollection<double> _ChannelMaskFeedBack = new ObservableCollection<double>(new double[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<double> ChannelMaskFeedBack
        {
            get { return _ChannelMaskFeedBack; }
            set { _ChannelMaskFeedBack = value; }
        }

        private ObservableCollection<double> _ChannelSpriteBlur = new ObservableCollection<double>(new double[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<double> ChannelSpriteBlur
        {
            get { return _ChannelSpriteBlur; }
            set { _ChannelSpriteBlur = value; }
        }

        private ObservableCollection<double> _ChannelMaskBlur = new ObservableCollection<double>(new double[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<double> ChannelMaskBlur
        {
            get { return _ChannelMaskBlur; }
            set { _ChannelMaskBlur = value; }
        }

        private ObservableCollection<double> _ChannelHueRange = new ObservableCollection<double>(new double[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<double> ChannelHueRange
        {
            get { return _ChannelHueRange; }
            set { _ChannelHueRange = value; }
        }

        private ObservableCollection<double> _ChannelSatRange = new ObservableCollection<double>(new double[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<double> ChannelSatRange
        {
            get { return _ChannelSatRange; }
            set { _ChannelSatRange = value; }
        }

        private ObservableCollection<double> _ChannelValRange = new ObservableCollection<double>(new double[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<double> ChannelValRange
        {
            get { return _ChannelValRange; }
            set { _ChannelValRange = value; }
        }

        private ObservableCollection<string> _ChannelSpriteTransform = new ObservableCollection<string>(new string[] { "None", "None", "None", "None", "None", "None"});
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<string> ChannelSpriteTransform
        {
            get { return _ChannelSpriteTransform; }
            set { _ChannelSpriteTransform = value; }
        }

        private ObservableCollection<string> _ChannelSpriteView = new ObservableCollection<string>(new string[] { "STD_CTR", "STD_CTR", "STD_CTR", "STD_CTR", "STD_CTR", "STD_CTR" });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<string> ChannelSpriteView
        {
            get { return _ChannelSpriteView; }
            set { _ChannelSpriteView = value; }
        }

        private ObservableCollection<string> _ChannelMaskTransform = new ObservableCollection<string>(new string[] { "None", "None", "None", "None", "None", "None" });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<string> ChannelMaskTransform
        {
            get { return _ChannelMaskTransform; }
            set { _ChannelMaskTransform = value; }
        }

        private ObservableCollection<string> _ChannelMaskView = new ObservableCollection<string>(new string[] { "STD_CTR", "STD_CTR", "STD_CTR", "STD_CTR", "STD_CTR", "STD_CTR" });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<string> ChannelMaskView
        {
            get { return _ChannelMaskView; }
            set { _ChannelMaskView = value; }
        }

        private ObservableCollection<double> _ChannelAlpha = new ObservableCollection<double>(new double[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<double> ChannelAlpha
        {
            get { return _ChannelAlpha; }
            set { _ChannelAlpha = value; }
        }

        private ObservableCollection<string> _ChannelBlendMode = new ObservableCollection<string>(new string[] {"Add", "Normal", "Normal", "Normal", "Normal", "Normal" });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<string> ChannelBlendMode
        {
            get { return _ChannelBlendMode; }
            set { _ChannelBlendMode = value; }
        }

        private ObservableCollection<Color> _ChannelColor = new ObservableCollection<Color>(new Color[] { Color.FromArgb(255, 255, 255, 255), Color.FromArgb(255, 255, 255, 255), Color.FromArgb(255, 255, 255, 255), Color.FromArgb(255, 255, 255, 255), Color.FromArgb(255, 255, 255, 255), Color.FromArgb(255, 255, 255, 255), });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<Color> ChannelColor
        {
            get { return _ChannelColor; }
            set { _ChannelColor = value; }
        }

        private ObservableCollection<Color> _ChannelBgColor = new ObservableCollection<Color>(new Color[] { Color.FromArgb(0, 0, 0, 0), Color.FromArgb(0, 0, 0, 0), Color.FromArgb(0, 0, 0, 0), Color.FromArgb(0, 0, 0, 0), Color.FromArgb(0, 0, 0, 0), Color.FromArgb(0, 0, 0, 0), });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<Color> ChannelBgColor
        {
            get { return _ChannelBgColor; }
            set { _ChannelBgColor = value; }
        }

        private ObservableCollection<double> _Brightness = new ObservableCollection<double>(new double[] { 0.0 });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<double> Brightness
        {
            get { return _Brightness; }
            set { _Brightness = value; }
        }

        private ObservableCollection<double> _Contrast = new ObservableCollection<double>(new double[] { 0.0 });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<double> Contrast
        {
            get { return _Contrast; }
            set { _Contrast = value; }
        }

        private ObservableCollection<double> _Saturation = new ObservableCollection<double>(new double[] { 0.0 });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<double> Saturation
        {
            get { return _Saturation; }
            set { _Saturation = value; }
        }

        private ObservableCollection<string> _CamRotation = new ObservableCollection<string>(new string[] { "STD_CTR" });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<string> CamRotation
        {
            get { return _CamRotation; }
            set { _CamRotation = value; }
        }

        private ObservableCollection<string> _CamLookAt = new ObservableCollection<string>(new string[] { "STD" });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<string> CamLookAt
        {
            get { return _CamLookAt; }
            set { _CamLookAt = value; }
        }

        private ObservableCollection<string> _CamView = new ObservableCollection<string>(new string[] { "STD" });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<string> CamView
        {
            get { return _CamView; }
            set { _CamView = value; }
        }

        private List<double> _CamZoom = new List<double> { 0.0, 0.0 };
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public List<double> CamZoom
        {
            get { return _CamZoom; }
            set { _CamZoom = value; }
        }

        /*private ObservableCollection<double> _CamZoom = new ObservableCollection<double>(new double[] { 0.0, 0.0 });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<double> CamZoom
        {
            get { return _CamZoom; }
            set { _CamZoom = value; }
        }*/

        private ObservableCollection<double> _CamFOV = new ObservableCollection<double>(new double[] { 0.0, 1.0 });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<double> CamFOV
        {
            get { return _CamFOV; }
            set { _CamFOV = value; }
        }

        private ObservableCollection<string> _CompoName = new ObservableCollection<string>(new string[] { "" });
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<string> CompoName
        {
            get { return _CompoName; }
            set { _CompoName = value; }
        }

        private ObservableCollection<string> _FilePath = new ObservableCollection<string>(new string[] { ""});
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<string> FilePath
        {
            get { return _FilePath; }
            set { _FilePath = value; }
        }

        /*private ObservableCollection<string> _ChannelBeat = new ObservableCollection<string>();
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public ObservableCollection<string> ChannelBeat
        {
            get { return _ChannelBeat; }
            set { _ChannelBeat = value; }
        }*/
    }
}
