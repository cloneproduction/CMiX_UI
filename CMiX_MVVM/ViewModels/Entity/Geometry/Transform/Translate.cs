﻿using System.Windows.Media.Media3D;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels
{
    public class Translate : Sender
    {
        public Translate(string name, IColleague parentSender) : base(name, parentSender)
        {
            X = new Slider(nameof(X), this);
            Y = new Slider(nameof(Y), this);
            Z = new Slider(nameof(Z), this);
        }

        public Slider X { get; set; }
        public Slider Y { get; set; }
        public Slider Z { get; set; }

        public override void Receive(Message message)
        {
            this.SetViewModel(message.Obj as TranslateModel);
        }

        public Vector3D GetVector()
        {
            return new Vector3D(X.Amount, Y.Amount, Z.Amount);
        }
    }
}