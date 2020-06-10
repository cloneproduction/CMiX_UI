using System.Windows;
using System.Windows.Input;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class Geometry : Sendable
    {
        public Geometry(Beat beat) 
        {
            Instancer = new Instancer(beat, this);
            Transform = new Transform(this);
            GeometryFX = new GeometryFX();
            AssetPathSelector = new AssetPathSelector<AssetGeometry>(this);
        }

        public Geometry(Beat beat, Sendable parentSendable) : this(beat)
        {
            SubscribeToEvent(parentSendable);
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as GeometryModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }

        public ICommand CopyGeometryCommand { get; }
        public ICommand PasteGeometryCommand { get; }
        public ICommand ResetGeometryCommand { get; }

        public AssetPathSelector<AssetGeometry> AssetPathSelector { get; set; }

        public Transform Transform { get; set; }
        public Instancer Instancer { get; set; }
        public GeometryFX GeometryFX { get; set; }

        public void CopyGeometry()
        {
            IDataObject data = new DataObject();
            data.SetData("GeometryModel", this.GetModel(), false);
            Clipboard.SetDataObject(data);
        }

        public void PasteGeometry()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("GeometryModel"))
            {
                //Mementor.BeginBatch();
                var geometrymodel = data.GetData("GeometryModel") as GeometryModel;
                this.SetViewModel(geometrymodel);
                //Mementor.EndBatch();
                //SendMessages(MessageAddress, geometrymodel);
            }
        }
    }
}