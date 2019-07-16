using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels;
using System.IO;
using Ceras;

namespace CMiXPlayer.Jobs
{
    class JobSendComposition : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var dataMap = context.MergedJobDataMap;
            FileSelector compositionmodel = (FileSelector)dataMap["CompoSelector"];
            OSCMessenger oscmessenger = (OSCMessenger)dataMap["OSCMessenger"];
            CerasSerializer serializer = (CerasSerializer)dataMap["Serializer"];

            SendComposition(oscmessenger, compositionmodel, serializer);

            await Console.Out.WriteLineAsync("Composition Sent");
        }

        private void SendComposition(OSCMessenger oscmessenger, FileSelector composelector, CerasSerializer serializer)
        {
            var filename = composelector.SelectedFileNameItem.FileName;
            byte[] data = File.ReadAllBytes(filename);
            CompositionModel compositionmodel = serializer.Deserialize<CompositionModel>(data);
            oscmessenger.SendMessage("/CompositionReloaded", true);
            oscmessenger.QueueObject(compositionmodel);
            oscmessenger.SendQueue();
        }
    }
}
