#region usings
using System;
using System.ComponentModel.Composition;
using VVVV.PluginInterfaces.V1;
using VVVV.PluginInterfaces.V2;
using VVVV.Utils.VColor;
using VVVV.Utils.VMath;
using VVVV.Core.Logging;
using CMiX.MVVM.Message;
#endregion usings

namespace CMiX.Engine
{
	#region PluginInfo
	[PluginInfo(Name = "CMiX.Engine", Category = "Value")]
	#endregion PluginInfo
	public class CMiXEngine : IPluginEvaluate
	{
		#region fields & pins
		[Input("Input", DefaultValue = 1.0)]
		public ISpread<double> FInput;

		[Output("Output")]
		public ISpread<double> FOutput;

		[Import()]
		public ILogger FLogger;
        #endregion fields & pins

        //Client Client;

        public CMiXEngine()
        {
            //Client = new Client();
            //Client.Start();
        }


		//called when data for any output pin is requested
		public void Evaluate(int SpreadMax)
		{
			FOutput.SliceCount = SpreadMax;

			for (int i = 0; i < SpreadMax; i++)
				FOutput[i] = FInput[i] * 4;

			//FLogger.Log(LogType.Debug, "hi tty!");
		}
	}
}
