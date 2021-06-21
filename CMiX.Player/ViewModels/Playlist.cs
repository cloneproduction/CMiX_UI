using System;
using CMiX.Core.Presentation.ViewModels;
using CMiX.Core.Models;
using System.Collections.ObjectModel;
using Ceras;
using GongSolutions.Wpf.DragDrop;
using System.Windows;
using System.IO;
using CMiXPlayer.Models;
using System.Linq;

namespace CMiXPlayer.ViewModels
{
    public class Playlist : ViewModel, IDropTarget
    {
        #region CONSTRUCTORS
        public Playlist(CerasSerializer serializer)
        {
            Compositions = new ObservableCollection<CompositionModel>();
            Serializer = serializer;
        }
        #endregion

        #region PROPERTIES
        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        public ObservableCollection<CompositionModel> Compositions { get; set; }

        public CerasSerializer Serializer { get; set; }
        #endregion

        #region DRAG/DROP
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
                            CompositionModel compositionmodel = Serializer.Deserialize<CompositionModel>(data);
                            Compositions.Add(compositionmodel);
                        }
                    }
                }
            }
        }
        #endregion

        #region COPY/PASTE
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
        #endregion
    }
}