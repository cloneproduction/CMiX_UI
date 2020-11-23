using System.ComponentModel.Composition;
using VVVV.PluginInterfaces.V1;
using VVVV.PluginInterfaces.V2;
using VVVV.Core.Logging;
using CMiX.MVVM.ViewModels;
using CMiX.Studio.ViewModels.MessageService;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.Nodes
{
    [PluginInfo(Name = "CMiX", Category = "CMiX_VVVV", AutoEvaluate = false),]
	public class CMiX_VVVVTemplateNode : IPluginEvaluate, IPartImportsSatisfiedNotification
	{
		[Import()]
		public ILogger FLogger;

		[Input("Start Receiver")]
		public ISpread<bool> FStartServer;

		[Input("Stop Receiver")]
		public ISpread<bool> FStopServer;

		[Output("Receiver Is Running")]
		public ISpread<bool> FReceiverIsRunning;

		[Output("Data Type")]
		public ISpread<string> FDataType;

		[Output("Project")]
		public ISpread<Project> FProjectOut;

        public Project Project { get; set; }
        public string DataType { get; set; }
		public Receiver Receiver { get; set; }

		public CMiX_VVVVTemplateNode()
        {
			Settings settings = new Settings("Pouet", "Pouet", "192.168.0.192", 2222);
			MessengerTerminal messengerTerminal = new MessengerTerminal();
			messengerTerminal.StartReceiver(settings);
            messengerTerminal.MessageReceived += MessengerTerminal_MessageReceived;
			Project = new Project(0, messengerTerminal);
		}

        private void MessengerTerminal_MessageReceived(object sender, MVVM.Services.MessageEventArgs e)
        {
			//FLogger.Log(LogType.Debug, "Received " + e.Message.Obj.GetType().Name);
		}

        public void OnImportsSatisfied()
		{
			FProjectOut[0] = this.Project;
		}



        public void Evaluate(int SpreadMax)
		{
			FReceiverIsRunning.SliceCount = 1;
			FDataType.SliceCount = 1;
			FProjectOut.SliceCount = 1;
			
			//if (FStartServer[0])
			//	this.Receiver.StartClient();

			//if(FStopServer[0])
			//	Receiver.StopClient();

			if (Receiver != null)
				FReceiverIsRunning[0] = Receiver.Client.IsRunning;
			else
				FReceiverIsRunning[0] = false;
		}
    }
}