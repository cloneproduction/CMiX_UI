using System;
using System.Windows;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class Transform : Sendable
    {
        public Transform()
        {
            Translate = new Translate(this);
            Scale = new Scale(this);
            Rotation = new Rotation(this);

            Is3D = false;
        }

        public Transform(Sendable parentSendable) : this()
        {
            SubscribeToEvent(parentSendable);
        }
        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as TransformModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }

        public Translate Translate { get; set; }
        public Scale Scale { get; set; }
        public Rotation Rotation { get; set; }

        private bool _is3D;
        public bool Is3D
        {
            get => _is3D;
            set
            {
                //if (Mementor != null)
                //    Mementor.PropertyChange(this, "Is3D");
                SetAndNotify(ref _is3D, value);
            }
        }

        #region COPY/PASTE/RESET
        public void CopyGeometry()
        {
            IDataObject data = new DataObject();
            data.SetData("TransformModel", this.GetModel(), false);
            Clipboard.SetDataObject(data);
        }

        public void PasteGeometry()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("TransformModel"))
            {
                var transformmodel = data.GetData("TransformModel") as TransformModel;
                this.SetViewModel(transformmodel);
            }
        }
        #endregion
    }
}