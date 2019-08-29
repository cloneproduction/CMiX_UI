using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
using CMiX.ColorPicker;

namespace ColorPicker.Effects
{
    public class RGBShaderControl : ShaderEffect
    {
        #region Constructors

        static RGBShaderControl()
        {
            _pixelShader.UriSource = Global.MakePackUri("RGBShaderControl.fx");
        }

        public RGBShaderControl()
        {
            this.PixelShader = _pixelShader;

            // Update each DependencyProperty that's registered with a shader register.  This
            // is needed to ensure the shader gets sent the proper default value.
            UpdateShaderValue(TextureProperty);

            UpdateShaderValue(RedAmountProperty);
            UpdateShaderValue(GreenAmountProperty);
            UpdateShaderValue(BlueAmountProperty);
        }

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty TextureProperty =
            ShaderEffect.RegisterPixelShaderSamplerProperty("Texture", typeof(RGBShaderControl), 0);
        public Brush Texture
        {
            get { return (Brush)GetValue(TextureProperty); }
            set { SetValue(TextureProperty, value); }
        }



        public static readonly DependencyProperty RedAmountProperty =
            DependencyProperty.Register("RedAmount", typeof(double), typeof(RGBShaderControl),
            new UIPropertyMetadata(1.0, PixelShaderConstantCallback(0)));
        public double RedAmount
        {
            get { return (double)GetValue(RedAmountProperty); }
            set { SetValue(RedAmountProperty, value); }
        }


        public static readonly DependencyProperty GreenAmountProperty =
            DependencyProperty.Register("GreenAmount", typeof(double), typeof(RGBShaderControl),
            new UIPropertyMetadata(1.0, PixelShaderConstantCallback(0)));
        public double GreenAmount
        {
            get { return (double)GetValue(GreenAmountProperty); }
            set { SetValue(GreenAmountProperty, value); }
        }


        public static readonly DependencyProperty BlueAmountProperty =
            DependencyProperty.Register("BlueAmount", typeof(double), typeof(RGBShaderControl),
            new UIPropertyMetadata(1.0, PixelShaderConstantCallback(0)));
        public double BlueAmount
        {
            get { return (double)GetValue(BlueAmountProperty); }
            set { SetValue(BlueAmountProperty, value); }
        }
        #endregion

        #region Member Data

        private static PixelShader _pixelShader = new PixelShader();

        #endregion

    }
}
