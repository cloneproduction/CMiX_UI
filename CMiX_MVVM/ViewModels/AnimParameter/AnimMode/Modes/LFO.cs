using CMiX.MVVM.Models;
using CMiX.MVVM.Resources;
using CMiX.MVVM.Services;
using CMiX.MVVM.Tools;

namespace CMiX.MVVM.ViewModels
{
    public class LFO : Sender, IAnimMode
    {
        public LFO(string name, Sender parentSender) : base(name, parentSender)
        {

        }

        public override void Receive(Message message)
        {
            this.SetViewModel(message.Obj as LFOModel);
        }

        public void UpdateOnBeatTick(double[] doubleToAnimate, double period, IRange range, Easing easing, BeatModifier beatModifier)
        {

        }

        public void UpdateOnGameLoop(double[] doubleToAnimate, double period, IRange range, Easing easing, BeatModifier beatModifier)
        {
            double periodOffset = 1.0 / doubleToAnimate.Length;
            double offset = 0.0;
            double val = 0.0;

            for (int i = 0; i < doubleToAnimate.Length; i++)
            {
                val = period + offset;
                if (val < 0.0)
                    val = 1.0 - (0.0 - val) % (1.0 - 0.0);
                else
                    val = 0.0 + (val - 0.0) % (1.0 - 0.0);

                doubleToAnimate[i] = Utils.Map(Easings.Interpolate((float)val, easing.SelectedEasing), 0.0, 1.0, 0.0 - range.Width / 2, 0.0 + range.Width / 2);

                offset += periodOffset;
            }
        }
    }
}