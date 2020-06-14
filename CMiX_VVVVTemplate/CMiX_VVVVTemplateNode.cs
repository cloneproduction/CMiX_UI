using System.ComponentModel.Composition;
using VVVV.PluginInterfaces.V1;
using VVVV.PluginInterfaces.V2;
using VVVV.Core.Logging;
using CMiX.MVVM.ViewModels;
using CMiX.Studio.ViewModels.MessageService;

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


        public CMiX_VVVVTemplateNode()
        {
			Project = ComponentFactory.CreateProject();
            var receiver = new Receiver();
			Project.Receiver = receiver;
            Settings settings = new Settings("Pouet", "Pouet", "192.168.0.192", 2222);
            receiver.SetSettings(settings);
            receiver.DataReceivedEvent += Receiver_DataReceivedEvent;
			receiver.DataReceivedEvent += Project.OnParentReceiveChange;
		}

		public void OnImportsSatisfied()
		{
			FProjectOut[0] = this.Project;
		}

		private void Receiver_DataReceivedEvent(object sender, CMiX.MVVM.Services.ModelEventArgs e)
        {
			FProjectOut[0] = this.Project;
			FDataType[0] = e.Model.GetType().Name;
			FLogger.Log(LogType.Debug, "Receiver_DataReceivedEvent");
		}

        public void Evaluate(int SpreadMax)
		{
			FReceiverIsRunning.SliceCount = 1;
			FDataType.SliceCount = 1;
			FProjectOut.SliceCount = 1;
			
			if (FStartServer[0])
				Project.Receiver.StartClient();

			if(FStopServer[0])
				Project.Receiver.StopClient();

			if (Project.Receiver != null)
				FReceiverIsRunning[0] = Project.Receiver.Client.IsRunning;
			else
				FReceiverIsRunning[0] = false;
		}


    }
}
