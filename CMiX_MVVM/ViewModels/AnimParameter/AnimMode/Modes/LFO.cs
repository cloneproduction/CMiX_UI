using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Media;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class LFO : Sendable, IAnimMode
    {
        public LFO()
        {
            Easing = new Easing(this);
            Stopwatch = new Stopwatch();
        }

        public LFO(string name, bool isEnabled, Sendable parentSendable) : this()
        {
            SubscribeToEvent(parentSendable);
        }

        public void Update()
        {
            Console.WriteLine("LFO Update " + Stopwatch.ElapsedMilliseconds);
            if (!Stopwatch.IsRunning)
                Stopwatch.Start();
            else
                Reset();
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            
        }

        public void Reset()
        {
            Stopwatch.Reset();
            Stopwatch.Start();
        }

        public Stopwatch Stopwatch { get; set; }

        private EasingType _selectedEasingType;
        public EasingType SelectedEasingType
        {
            get => _selectedEasingType;
            set => SetAndNotify(ref _selectedEasingType, value);
        }

        public IEnumerable<EasingType> EasingTypeSource
        {
            get => Enum.GetValues(typeof(EasingType)).Cast<EasingType>();
        }

        public Range Range { get; set; }

        public Easing Easing { get; set; }

        private bool _invert;
        public bool Invert
        {
            get => _invert;
            set => SetAndNotify(ref _invert, value);
        }
    }
}