﻿using System;
using System.Collections.Generic;
using System.Linq;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class LFO : Sendable, IAnimMode
    {
        public LFO()
        {

        }
        public LFO(Stopwatcher stopwatcher)
        {

            SelectedEasingType = EasingType.Linear;
            //UpdateValue = new Action(Update);
        }

        public void Update()
        {
            //Console.WriteLine("LFO Update");
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            
        }

        private EasingType _selectedEasingType;
        public EasingType SelectedEasingType
        {
            get => _selectedEasingType;
            set => SetAndNotify(ref _selectedEasingType, value);
        }

        public IEnumerable<EasingType> ModeTypeSource
        {
            get => Enum.GetValues(typeof(EasingType)).Cast<EasingType>();
        }

        public Range Range { get; set; }

        private bool _invert;
        public bool Invert
        {
            get => _invert;
            set => SetAndNotify(ref _invert, value);
        }
    }
}