using System.Windows;
using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class XYZModifier : Sendable, IModifier
    {
        public XYZModifier(string name, Beat beat) 
        {
            Name = name;
            Uniform = new AnimParameter(nameof(Uniform), beat, true, this);
            X = new AnimParameter(nameof(X), beat, false, this);
            Y = new AnimParameter(nameof(Y), beat, false, this);
            Z = new AnimParameter(nameof(Z), beat, false, this);
        }

        public XYZModifier(string name, Beat beat, Sendable parentSendable) : this(name, beat)
        {
            SubscribeToEvent(parentSendable);
        }

        public override string GetMessageAddress()
        {
            return $"{Name}/";
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as XYZModifierModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }

        public string Name { get; set; }
        public AnimParameter Uniform { get; set; }
        public AnimParameter X { get; set; }
        public AnimParameter Y { get; set; }
        public AnimParameter Z { get; set; }
    }
}
