using System;
using System.ComponentModel.Composition;
using VVVV.PluginInterfaces.V1;
using VVVV.PluginInterfaces.V2;
using VVVV.Utils.VColor;
using VVVV.Utils.VMath;
using VVVV.Core.Logging;
using System.Collections.Generic;

namespace CMiX.Engine
{
    [PluginInfo(Name = "AnimParameter", Category = "Value", Help = "Basic template with one value in/out", Tags = "")]
    public class EngineNode : IPluginEvaluate
    {
        #region fields & pins
        [Input("Period", DefaultValue = 1)]
        public IDiffSpread<double> FPeriod;

        [Input("RandomType", DefaultEnumEntry = "STEPPER")]
        public IDiffSpread<ModeType> FModeType;

        [Output("Output")]
        public ISpread<double> FOutput;
        #endregion fields & pins

        List<AnimParameter> AnimParameters = new List<AnimParameter>();

        public EngineNode()
        {
            for (int i = 0; i < 10; i++)
            {
                var newparam = new AnimParameter(1.0);
                newparam.ModeType = ModeType.STEPPER;
                AnimParameters.Add(newparam);
            }
        }

        public void Evaluate(int SpreadMax)
        {
            if (FPeriod.IsChanged)
            {
                if (FPeriod.SliceCount > 0)
                {
                    for (int i = 0; i < AnimParameters.Count; i++)
                    {
                        AnimParameters[i].Stopwatcher.Period = FPeriod[i];
                    }
                }
            }

            if (FModeType.IsChanged)
            {
                for (int i = 0; i < AnimParameters.Count; i++)
                {
                    AnimParameters[i].ModeType = FModeType[i];
                }
            }

            FOutput.SliceCount = AnimParameters.Count;
            for (int i = 0; i < AnimParameters.Count; i++)
            {
                AnimParameters[i].Update();
                FOutput[i] = AnimParameters[i].Mode.ParameterValue;
            }
        }
    }
}