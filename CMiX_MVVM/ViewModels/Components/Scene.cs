using System.Windows.Input;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;
using CMiX.MVVM.Services;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class Scene : Component
    {
        public Scene(int id, Beat beat) : base(id, beat)
        {
            Name = "Scene";
            BeatModifier = new BeatModifier(beat);
            PostFX = new PostFX();
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            Console.WriteLine("MessageReceive on Scene " +  this.Name + this.ID);
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
            {
                this.SetViewModel(e.Model as SceneModel);
                //Console.WriteLine("Scene Entity Count = " + this.Components.Count);
            }
                
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }

        public BeatModifier BeatModifier { get; }
        public PostFX PostFX { get; }
    }
}