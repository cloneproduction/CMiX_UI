using CMiX.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GongSolutions.Wpf.DragDrop;
using System.Windows;
using System.Collections.Specialized;
using CMiX.Controls;
using System.ComponentModel;

namespace CMiX.ViewModels
{
    public class Composition : ViewModel, IDropTarget
    {
        public Composition()
        {
            Name = string.Empty;

            var messenger = new OSCMessenger(new SharpOSC.UDPSender("127.0.0.1", 55555));
            Messenger = messenger;


            MasterBeat = new MasterBeat(messenger);
            Camera = new Camera(messenger, MasterBeat);
            AddLayerCommand = new RelayCommand(p => AddLayer());
            RemoveLayerCommand = new RelayCommand(p => RemoveLayer());

            Layers = new ObservableCollection<Layer>
            {
                new Layer(MasterBeat, "LayerPouet", messenger),
                new Layer(MasterBeat, "LayerProut", messenger)
            };
            Layers.CollectionChanged += ContentCollectionChanged;
        }

        public Composition(string name, Camera camera, MasterBeat masterBeat, IEnumerable<Layer> layers)
        {
            if (layers == null)
            {
                throw new ArgumentNullException(nameof(layers));
            }

            var messenger = new OSCMessenger(new SharpOSC.UDPSender("127.0.0.1", 55555));
            Messenger = messenger;

            Name = name;
            Camera = camera ?? throw new ArgumentNullException(nameof(camera));
            MasterBeat = masterBeat ?? throw new ArgumentNullException(nameof(masterBeat));
            Layers = new ObservableCollection<Layer>(layers);
            Layers.CollectionChanged += ContentCollectionChanged;
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        private IMessenger Messenger { get; }

        public MasterBeat MasterBeat { get; }

        public Camera Camera { get; }

        public ObservableCollection<Layer> Layers { get; }

        public ICommand AddLayerCommand { get; }
        public ICommand RemoveLayerCommand { get; }

        private int layerID = 0;

        private void AddLayer()
        {
            layerID += 1;
            Layers.Add(new Layer(MasterBeat, "Layer" + layerID.ToString(), Messenger));

            List<string> LayerNames = new List<string>();
            foreach(Layer lyr in this.Layers)
            {
                LayerNames.Add(lyr.LayerName);
            }
            Messenger.SendMessage("/LayerNames", LayerNames.ToArray());
        }

        private void RemoveLayer()
        {
            Layers.RemoveAt(Layers.Count - 1);
            List<string> LayerNames = new List<string>();
            foreach (Layer lyr in this.Layers)
            {
                LayerNames.Add(lyr.LayerName);
            }
            Messenger.SendMessage("/LayerNames", LayerNames.ToArray());
        }


        public void ContentCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            /*if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (Layer item in e.OldItems)
                {
                    //Removed items
                    item.PropertyChanged -= EntityViewModelPropertyChanged;
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (Layer item in e.NewItems)
                {
                    //Added items
                    item.PropertyChanged += EntityViewModelPropertyChanged;
                }
            }*/

            List<string> filename = new List<string>();
            foreach (Layer layer in Layers)
            {
                filename.Add(layer.LayerName);
            }
            Messenger.SendMessage("POUETPOUET" + nameof(Layer), filename.ToArray());
        }

        /*public void EntityViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            List<string> filename = new List<string>();
            foreach (Layer layer in Layers)
            {
                filename.Add(layer.LayerName);
            }
            Messenger.SendMessage("POUETPOUET" + nameof(Layer), filename.ToArray());
        }*/


        public void DragOver(IDropInfo dropInfo)
        {
            /*Layer sourceItem = dropInfo.Data as Layer;
            Layer targetItem = dropInfo.TargetItem as Layer;

            if (sourceItem != null && targetItem != null && targetItem.CanAcceptChildren)
            {
                dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
                dropInfo.Effects = DragDropEffects.Copy;
            }*/
        }

        public void Drop(IDropInfo dropInfo)
        {
            /*Layer sourceItem = dropInfo.Data as Layer;
            Layer targetItem = dropInfo.TargetItem as Layer;*/
            //targetItem.Children.Add(sourceItem);
        }
    }
}