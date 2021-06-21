using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using Ceras;
using CMiX.Core.Models;
using CMiX.Core.Models.Scheduler;
using CMiX.Core.Network.Communicators;
using GongSolutions.Wpf.DragDrop;

namespace CMiX.Core.Presentation.ViewModels.Scheduler
{
    public class Playlist : ViewModel, IControl, IDropTarget
    {
        public Playlist()
        {
            Compositions = new ObservableCollection<CompositionModel>();
            
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }



        public ObservableCollection<CompositionModel> Compositions { get; set; }



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

        public void Copy(PlaylistModel playlistmodel)
        {
            playlistmodel.Name = Name;
            playlistmodel.Compositions = Compositions.ToList();
        }

        public void Paste(PlaylistModel playlistmodel)
        {
            Name = playlistmodel.Name;
            foreach (var composition in playlistmodel.Compositions)
            {
                Compositions.Add(composition);
            }
        }

        public void SetCommunicator(Communicator communicator)
        {
            throw new NotImplementedException();
        }

        public void UnsetCommunicator(Communicator communicator)
        {
            throw new NotImplementedException();
        }

        public void SetViewModel(IModel model)
        {
            throw new NotImplementedException();
        }

        public IModel GetModel()
        {
            throw new NotImplementedException();
        }
    }
}
