using System;
using System.Windows;
using System.Windows.Input;
using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services.Message;

namespace CMiX.MVVM.ViewModels
{
    public class Entity : Component, IBeat
    {
        public Entity(int id, MasterBeat beat) : base(id)
        {
            BeatModifier = new BeatModifier(beat);
            Geometry = new Geometry(beat, this);
            Texture = new Texture(this);
            Coloration = new Coloration(beat, this);

            Hub.Subscribe<MessageReceived>(this, message =>
            {
                Console.WriteLine("Component Received Message from  " + message.Address);
                Console.WriteLine("message.Address " + message.Address + " this.GetMessageAddress() " + this.GetMessageAddress());
                if (message.Address == this.GetMessageAddress())
                    Console.WriteLine("Update Component !");
            });
        }

        public ICommand CopyEntityCommand { get; }
        public ICommand PasteEntityCommand { get; }
        public ICommand ResetEntityCommand { get; }
        
        public BeatModifier BeatModifier { get; }
        public Geometry Geometry { get; }
        public Texture Texture { get; }
        public Coloration Coloration { get; }
        public MasterBeat MasterBeat { get; set; }
    }
}