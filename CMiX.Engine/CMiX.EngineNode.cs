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
	[PluginInfo(Name = "CMiX.Engine", Category = "Value", AutoEvaluate =true)]
	#endregion PluginInfo
	public class CMiXEngine : IPluginEvaluate
	{
		#region fields & pins
		[Input("Input", DefaultValue = 1.0)]
		public ISpread<double> FInput;

		[Output("Output")]
		public ISpread<double> FOutput;

        [Output("MessageReceived")]
        public ISpread<string> FMessageReceived;

        [Import()]
		public ILogger FLogger;
        #endregion fields & pins

        NetMQMessenger NetMQMessenger;

        public CMiXEngine()
        {
            NetMQMessenger = new NetMQMessenger();
            NetMQMessenger.StartSubscriber();
        }

		public void Evaluate(int SpreadMax)
		{
			FOutput.SliceCount = SpreadMax;

            FLogger.Log(LogType.Message, NetMQMessenger.ReceivedString);
            FMessageReceived[0] = NetMQMessenger.ReceivedString;
        }
	}
}
