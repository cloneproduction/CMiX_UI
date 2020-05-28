using System;
using System.Windows;
using System.Windows.Input;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;

namespace CMiX.MVVM.ViewModels
{
    public class Entity : Component
    {
        public Entity(int id, Beat beat)
            : base(id, beat)
        {
            BeatModifier = new BeatModifier(beat);
            Geometry = new Geometry(beat);
            Geometry.SendChangeEvent += this.OnChildPropertyToSendChange;

            Texture = new Texture();
            Coloration = new Coloration(beat);

            CopyEntityCommand = new RelayCommand(p => CopyEntity());
            PasteEntityCommand = new RelayCommand(p => PasteEntity());
            //ResetEntityCommand = new RelayCommand(p => ResetEntity());
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (this.GetMessageAddress() == e.MessageAddress)
            {
                this.SetViewModel(e.Model as EntityModel);
                Console.WriteLine("Entity Updated");
            }
        }

        #region PROPERTIES
        public ICommand CopyEntityCommand { get; }
        public ICommand PasteEntityCommand { get; }
        public ICommand ResetEntityCommand { get; }
        
        public BeatModifier BeatModifier { get; }
        public Geometry Geometry { get; }
        public Texture Texture { get; }
        public Coloration Coloration { get; }
        #endregion

        #region COPY/PASTE
        public void Reset()
        {
            this.Enabled = true;
            this.BeatModifier.Reset();
            this.Geometry.Reset();
            this.Texture.Reset();
            this.Coloration.Reset();
        }


        public void CopyEntity()
        {
            IDataObject data = new DataObject();
            data.SetData(nameof(EntityModel), this.GetModel(), false);
            Clipboard.SetDataObject(data);
        }

        public void PasteEntity()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent(nameof(EntityModel)))
            {
                //this.Mementor.BeginBatch();
                var entityModel = data.GetData(nameof(EntityModel)) as EntityModel;
                this.SetViewModel(entityModel);
                //this.Mementor.EndBatch();
                //SendMessages(nameof(ContentModel), contentmodel);
            }
        }

        //public void ResetEntity()
        //{
        //    EntityModel entityModel = GetModel();
        //    this.Reset();
        //    //SendMessages(nameof(ContentModel), entityModel);
        //}
        #endregion
    }
}