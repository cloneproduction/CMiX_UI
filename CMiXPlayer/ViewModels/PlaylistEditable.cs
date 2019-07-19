using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels;
using System;
using System.Windows;
using System.Collections.ObjectModel;
using GongSolutions.Wpf.DragDrop;
using System.IO;
using Ceras;

namespace CMiXPlayer.ViewModels
{
    public class PlaylistEditable : ViewModel, IDropTarget
    {
        public PlaylistEditable(CerasSerializer serializer)
        {
            Serializer = serializer;
        }

        private Playlist _playlist;
        public Playlist Playlist
        {
            get => _playlist;
            set => SetAndNotify(ref _playlist, value);
        }

        public CerasSerializer Serializer { get; set; }

        public void DragOver(IDropInfo dropInfo)
        {
            Console.WriteLine("DragOver");
            dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
            var dataObject = dropInfo.Data as IDataObject;
            if (dataObject != null && dataObject.GetDataPresent(DataFormats.FileDrop))
            {
                dropInfo.Effects = DragDropEffects.Copy;
            }

            //if (Mementor.IsInBatch)
            //{
            //    Mementor.EndBatch();
            //}
        }

        public void Drop(IDropInfo dropInfo)
        {
            var dataObject = dropInfo.Data as DataObject;
            if (dataObject != null)
            {
                if (dataObject.ContainsFileDropList())
                {
                    //Mementor.BeginBatch();
                    var filedrop = dataObject.GetFileDropList();
                    foreach (string str in filedrop)
                    {
                        if (Path.GetExtension(str).ToUpperInvariant() == ".COMPMIX")
                        {
                            byte[] data = File.ReadAllBytes(str);
                            CompositionModel compositionmodel = Serializer.Deserialize<CompositionModel>(data);
                            Playlist.Compositions.Add(compositionmodel);
                        }
                    }
                    //Mementor.EndBatch();
                }
            }
        }
    }
}
