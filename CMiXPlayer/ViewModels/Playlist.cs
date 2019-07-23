using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using System.Collections.ObjectModel;
using Ceras;
using GongSolutions.Wpf.DragDrop;
using System.Windows;
using System.IO;

namespace CMiXPlayer.ViewModels
{
    public class Playlist : ViewModel, IDropTarget
    {
        public Playlist(CerasSerializer serializer)
        {
            Compositions = new ObservableCollection<CompositionModel>();
            Serializer = serializer;
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        public ObservableCollection<CompositionModel> Compositions { get; set; }


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
                            Compositions.Add(compositionmodel);
                        }
                    }
                    //Mementor.EndBatch();
                }
            }
        }
    }
}
