using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using CMiX.Core.Models;
using CMiX.Core.Models.Scheduler;
using CMiX.Core.Network.Communicators;
using CMiX.Core.Presentation.ViewModels.Components;
using GongSolutions.Wpf.DragDrop;

namespace CMiX.Core.Presentation.ViewModels.Scheduler
{
    public class Playlist : ViewModel, IControl, IDropTarget
    {
        public Playlist()
        {
            Compositions = new ObservableCollection<Composition>();
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }



        public ObservableCollection<Composition> Compositions { get; set; }

        public ControlCommunicator Communicator { get; set; }

        public Guid ID { get; set; }


        public void DragOver(IDropInfo dropInfo)
        {
            dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
            var dataObject = dropInfo.Data as IDataObject;
            if (dataObject != null && dataObject.GetDataPresent(DataFormats.FileDrop))
            {
                dropInfo.Effects = DragDropEffects.Copy;
            }
        }

        public void Drop(IDropInfo dropInfo)
        {
            var dataObject = dropInfo.Data as DataObject;
            if (dataObject != null)
            {
                if (dataObject.ContainsFileDropList())
                {
                    var filedrop = dataObject.GetFileDropList();
                    foreach (string str in filedrop)
                    {
                        if (Path.GetExtension(str).ToUpperInvariant() == ".COMPMIX")
                        {
                            byte[] data = File.ReadAllBytes(str);
                            //CompositionModel compositionmodel = Serializer.Deserialize<CompositionModel>(data);
                            //Compositions.Add(compositionmodel);
                        }
                    }
                }
            }
        }


        public void SetCommunicator(Communicator communicator)
        {
            Communicator = new ControlCommunicator(this);
            Communicator.SetCommunicator(communicator);
        }

        public void UnsetCommunicator(Communicator communicator)
        {
            Communicator.UnsetCommunicator(communicator);
        }


        public void SetViewModel(IModel model)
        {
            //PlaylistModel playlistModel = model as PlaylistModel;
            //this.Name = playlistModel.Name;
            //foreach (var composition in playlistModel.Compositions)
            //{
            //    Compositions.Add(composition);
            //}
        }

        public IModel GetModel()
        {
            throw new NotImplementedException();
        }
    }
}
