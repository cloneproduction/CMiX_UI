﻿#pragma checksum "..\..\..\CMiX Controls\CustomSlider.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "485DB234F2F44212ED3668F79F6186A624763291"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using CMiX;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace CMiX {
    
    
    /// <summary>
    /// CustomSlider
    /// </summary>
    public partial class CustomSlider : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 76 "..\..\..\CMiX Controls\CustomSlider.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider CMiXSlider;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\..\CMiX Controls\CustomSlider.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddToSlider;
        
        #line default
        #line hidden
        
        
        #line 91 "..\..\..\CMiX Controls\CustomSlider.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SubToSlider;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/CMiX_UserControl;component/cmix%20controls/customslider.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\CMiX Controls\CustomSlider.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.CMiXSlider = ((System.Windows.Controls.Slider)(target));
            
            #line 77 "..\..\..\CMiX Controls\CustomSlider.xaml"
            this.CMiXSlider.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.CMiXSlider_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.AddToSlider = ((System.Windows.Controls.Button)(target));
            
            #line 90 "..\..\..\CMiX Controls\CustomSlider.xaml"
            this.AddToSlider.Click += new System.Windows.RoutedEventHandler(this.AddToSlider_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.SubToSlider = ((System.Windows.Controls.Button)(target));
            
            #line 91 "..\..\..\CMiX Controls\CustomSlider.xaml"
            this.SubToSlider.Click += new System.Windows.RoutedEventHandler(this.SubToSlider_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

