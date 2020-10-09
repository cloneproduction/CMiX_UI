using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GongSolutions.Wpf.DragDrop;
using CMiX.MVVM.Resources;

namespace CMiX.MVVM.ViewModels
{
    public class Outliner : ViewModel, IDropTarget, IDragSource
    {
        public Outliner(ObservableCollection<Component> components)
        {
            Components = components;
            RightClickCommand = new RelayCommand(p => RightClick());
        }

        private void RightClick()
        {
            CanAddComponent();
            CanRenameComponent();
            CanEditComponent();
            CanDuplicateComponent();
            CanDeleteComponent();
        }

        public ICommand RightClickCommand { get; }

        public ObservableCollection<Component> Components { get; set; }


        private Component _selectedComponent;
        public Component SelectedComponent
        {
            get => _selectedComponent;
            set => SetAndNotify(ref _selectedComponent, value);
        }


        private string _addContentText;
        public string AddContentText
        {
            get => _addContentText;
            set => SetAndNotify(ref _addContentText, value);
        }

        private bool _canAdd;
        public bool CanAdd
        {
            get => _canAdd;
            set => SetAndNotify(ref _canAdd, value);
        }

        private bool _canDuplicate;
        public bool CanDuplicate
        {
            get => _canDuplicate;
            set => SetAndNotify(ref _canDuplicate, value);
        }

        private bool _canRename;
        public bool CanRename
        {
            get => _canRename;
            set => SetAndNotify(ref _canRename, value);
        }

        private bool _canEdit;
        public bool CanEdit
        {
            get => _canEdit;
            set => SetAndNotify(ref _canEdit, value);
        }

        private bool _canDelete;
        public bool CanDelete
        {
            get => _canDelete;
            set => SetAndNotify(ref _canDelete, value);
        }

        public void CanAddComponent()
        {
            CanAdd = true;
            AddContentText = "Add";

            if (SelectedComponent is Scene)
                AddContentText = "Add Entity";
            else if (SelectedComponent is Layer)
                AddContentText = "Add Scene";
            else if (SelectedComponent is Composition)
                AddContentText = "Add Layer";
            else if(SelectedComponent is Project)
                AddContentText = "Add Composition";
            else
                CanAdd = false;
        }

        public void CanDuplicateComponent()
        {
            if (SelectedComponent is Composition || SelectedComponent is Layer || SelectedComponent is Scene || SelectedComponent is Entity)
                CanDuplicate = true;
            else
                CanDuplicate = false;
        }

        public void CanRenameComponent()
        {
            if (SelectedComponent is Project || SelectedComponent is Composition || SelectedComponent is Layer || SelectedComponent is Scene || SelectedComponent is Entity)
                CanRename = true;
            else
                CanRename = false;
        }

        public void CanEditComponent()
        {
            if (SelectedComponent != null)
                CanEdit = true;
            else
                CanEdit = false;
        }

        public void CanDeleteComponent()
        {
            if (SelectedComponent is Composition || SelectedComponent is Layer || SelectedComponent is Scene || SelectedComponent is Entity)
                CanDelete = true;
            else
                CanDelete = false;
        }

        public int FindItemParentIndex(ObservableCollection<Component> components, Component componentToFind)
        {
            int index = -1;

            if (components.Contains(componentToFind))
            {
                index = components.IndexOf(componentToFind);
            }
            else
            {
                foreach (var item in components)
                {
                    FindItemParentIndex(item.Components, componentToFind);
                }
            }
            return index;
        }




        public bool CanDropOnSameParent(IDropInfo dropInfo)
        {
            bool canDrop = false;

            //Cant drop on itself

                // NOT OVERRING THE CENTER PART
                if (!dropInfo.InsertPosition.HasFlag(RelativeInsertPosition.TargetItemCenter))
                {
                    // can't drop just next after
                    if (!InsertPositionAfterTargetItem && !InsertPositionBeforeTargetItem)
                    {
                        // parent can't be the target
                        if (SourceComponentParent != TargetComponent)
                        {
                            // parent source is parent target so it drop within
                            if (ParentVisualSource == ParentVisualTarget)
                            {
                                //drop only on same type
                                if (DataObject.GetType() == TargetComponent.GetType())
                                {
                                    canDrop = true;
                                    Console.WriteLine("CanDropOnSameParent");
                                }
                            }
                        }
                    }
                
            }

            return canDrop;
        }

        private void DropOnSameParent()
        {
            if (SourceIndex < TargetIndex)
                TargetIndex -= 1;

            TargetCollection.Move(SourceIndex, TargetIndex);
            Console.WriteLine("DropOnSameParent");

            TargetComponentParent.OnSendChange(TargetComponentParent.GetModel(), TargetComponentParent.GetMessageAddress());
        }


        public bool CanDropWithinDifferentParent(IDropInfo dropInfo)
        {
            bool canDrop = false;

            // drop on a different parent
            if (ParentVisualSource != ParentVisualTarget && !dropInfo.InsertPosition.HasFlag(RelativeInsertPosition.TargetItemCenter))
            {
                //dropping on a same type
                if (DataObject.GetType() == TargetComponent.GetType())
                {
                    canDrop = true;
                    Console.WriteLine("CanDropWithinDifferentParent");
                }
            }

            return canDrop;
        }
        private void DropWithinDifferentParent()
        {
            TargetCollection.Insert(TargetIndex, SourceComponent);
            SourceCollection.Remove(SourceComponent);
            Console.WriteLine("DropWithinDifferentParent");
            SourceComponentParent.OnSendChange(SourceComponentParent.GetModel(), SourceComponentParent.GetMessageAddress());
            TargetComponentParent.OnSendChange(TargetComponentParent.GetModel(), TargetComponentParent.GetMessageAddress());
        }



        public bool CanDropEntityOnScene(IDropInfo dropInfo)
        {
            bool canDrop = false;

            if (ParentVisualSource != VisualTarget)
            {
                if(DataObject is Entity && TargetComponent is Scene)
                {
                    canDrop = true;
                    Console.WriteLine("CanDropEntityOnScene");
                }

            }

            return canDrop;
        }

        private void DropEntityOnScene()
        {
            if (TargetComponent is Scene)
            {
                TargetComponent.Components.Insert(0, SourceComponent);
                TargetComponent.IsExpanded = true;
            }
            SourceCollection.Remove(SourceComponent);

            SourceComponentParent.OnSendChange(SourceComponentParent.GetModel(), SourceComponentParent.GetMessageAddress());
            TargetComponent.OnSendChange(TargetComponent.GetModel(), TargetComponent.GetMessageAddress());
        }




        public bool CanDropOnLastItem(IDropInfo dropInfo)
        {
            bool canDrop = false;
            
            if (dropInfo.InsertPosition.HasFlag(RelativeInsertPosition.AfterTargetItem) && !dropInfo.InsertPosition.HasFlag(RelativeInsertPosition.TargetItemCenter))
            {
                Console.WriteLine("YO!");
                if (!SourceComponent.Components.Contains(TargetComponent))
                {
                    
                    if (TargetComponent == TargetComponentParent.Components.Last() && TargetComponentParent == TargetComponentGrandParent.Components.Last())
                    {
                        canDrop = true;
                    }
                }
            }
            return canDrop;
        }

        private void DropOnLastItem()
        {
            if (TargetComponentGrandParent.GetType() == typeof(Composition))
            {
                if (SourceIndex > ParentComponentIndex)
                    ParentComponentIndex += 1;

                GrandParentCollectionSource.Move(SourceIndex, ParentComponentIndex);

                TargetComponentGrandParent.OnSendChange(TargetComponentGrandParent.GetModel(), TargetComponentGrandParent.GetMessageAddress());
            }

            else if (TargetComponentGrandParent.GetType() == typeof(Layer))
            {
                if (SourceIndex > GrandParentComponentIndex)
                    GrandParentComponentIndex += 1;

                GrandGrandParentCollectionSource.Move(SourceIndex, GrandParentComponentIndex);

                TargetComponentGrandGrandParent.OnSendChange(TargetComponentGrandGrandParent.GetModel(), TargetComponentGrandGrandParent.GetMessageAddress());
            }
        }





        public void Drop(IDropInfo dropInfo)
        {
            //if (CanDropOnSameParent(dropInfo))
            //    DropOnSameParent();

            //else if (CanDropWithinDifferentParent(dropInfo))
            //    DropWithinDifferentParent();

            //else if (CanDropEntityOnScene(dropInfo))
            //    DropEntityOnScene();

            //else if (CanDropOnLastItem(dropInfo))
            //    DropOnLastItem();
        }


        public void DragOver(IDropInfo dropInfo)
        {


            if (!dropInfo.Data.GetType().IsSubclassOf(typeof(Component)))
                return;

            GetInfo(dropInfo);

            if (TargetComponent == null || TargetComponentParent == null)
                return;

            if (SourceComponent == TargetComponent)
                return;

            Console.WriteLine(CheckItemIsLast(VisualTarget));

            if (SourceComponentParent != TargetComponent)
            {
                if (//TargetComponentParent.GetType() == SourceComponent.GetType() &&
                     TargetComponentParent != SourceComponent
                    && !InsertPositionTargetItemCenter 
                    && dropInfo.InsertPosition.HasFlag(RelativeInsertPosition.AfterTargetItem))
                {
                    if(TargetComponentParent.Components.Last() == TargetComponent )
                    {
                        dropInfo.Effects = DragDropEffects.Copy | DragDropEffects.Move;
                        dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
                    }
                    if (SourceComponentParent.Components.Last() == TargetComponentGrandParent)
                    {
                        dropInfo.Effects = DragDropEffects.Copy | DragDropEffects.Move;
                        dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
                    }

                }
                else if (SourceComponent.GetType() == typeof(Layer) && TargetComponent.GetType() == typeof(Composition)
                    || SourceComponent.GetType() == typeof(Scene) && TargetComponent.GetType() == typeof(Layer)
                    || SourceComponent.GetType() == typeof(Entity) && TargetComponent.GetType() == typeof(Scene))
                {
                    dropInfo.Effects = DragDropEffects.Copy | DragDropEffects.Move;
                    dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
                }

                else if (!InsertPositionAfterTargetItem && !InsertPositionBeforeTargetItem && !InsertPositionTargetItemCenter)
                {
                    if (SourceComponent.GetType() == TargetComponent.GetType())
                    {
                        dropInfo.Effects = DragDropEffects.Copy | DragDropEffects.Move;
                        dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
                    }
                }
            }
        }

        //else if (!SourceComponent.Components.Contains(TargetComponent))
        //{
        //    if (SourceComponent != TargetComponent)
        //    {
        //        if (TargetComponent == TargetComponentParent.Components.Last() && TargetComponentParent == TargetComponentGrandParent.Components.Last())
        //        {
        //            dropInfo.Effects = DragDropEffects.Copy | DragDropEffects.Move;
        //            dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
        //        }
        //    }
        //}


        //else if (TargetComponent == GetLastItem(Components.Last()) && dropInfo.InsertPosition.HasFlag(RelativeInsertPosition.AfterTargetItem) && !InsertPositionTargetItemCenter)
        //{
        //    dropInfo.Effects = DragDropEffects.Copy | DragDropEffects.Move;
        //    dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
        //}

        //if (SourceComponent.GetType() != TargetComponentParent.GetType())
        //{
        //    if (SourceComponent != TargetComponent && SourceComponent != TargetComponentParent && SourceComponent != TargetComponentGrandParent)
        //    {
        //        if (CanDropOnSameParent(dropInfo) || CanDropWithinDifferentParent(dropInfo) || CanDropOnLastItem(dropInfo))
        //        {
        //            dropInfo.Effects = DragDropEffects.Copy | DragDropEffects.Move;
        //            dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
        //        }
        //        else if (CanDropEntityOnScene(dropInfo))
        //        {
        //            dropInfo.Effects = DragDropEffects.Copy | DragDropEffects.Move;
        //            dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
        //        }
        //    }
        //}


        private bool CheckItemIsLast(TreeViewItem treeViewItem)
        {
            var parent = Utils.FindParent<TreeViewItem>(treeViewItem);
            if(parent == null)
            {
                return true;
            }
            else
            {
                var collection = (ObservableCollection<Component>)parent.ItemsSource;
                var targetComponent = treeViewItem.DataContext as Component;
                if (collection != null)
                {
                    if (collection.Last() == targetComponent)
                    {
                        Console.WriteLine(targetComponent.Name + targetComponent.Components.Any());
                        if (!targetComponent.IsExpanded)
                        {
                            return true;
                        }
                        else
                            return CheckItemIsLast(parent);
                    }

                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
        }

        public Component GetLastItem(Component component)
        {
            // Argument-checking omitted. You should possibly make this 
            // an instance-method on Node anyway.       

            return !component.Components.Any() ? component : GetLastItem(component.Components.Last());
        }

        private TreeViewItem ParentVisualSource;

        private TreeViewItem VisualTarget;
        private TreeViewItem VisualSource;

        private TreeViewItem ParentVisualTarget;
        private TreeViewItem GrandParentVisualTarget;
        private TreeViewItem GrandGrandParentVisualTarget;

        private Component SourceComponent;
        private Component SourceComponentParent;
        private Component SourceComponentGrandParent;
        private Component SourceComponentGrandGrandParent;

        private Component TargetComponent;
        private Component TargetComponentParent;
        private Component TargetComponentGrandParent;
        private Component TargetComponentGrandGrandParent;


        private int ParentComponentIndex = -1;
        private int GrandParentComponentIndex = -1;

        private ObservableCollection<Component> GrandParentCollectionSource;
        private ObservableCollection<Component> GrandGrandParentCollectionSource;

        private ObservableCollection<Component> TargetCollection;
        private ObservableCollection<Component> SourceCollection;

        private int TargetIndex = -1;
        private int SourceIndex = -1;

        private Component DataObject;
        private bool InsertPositionAfterTargetItem;
        private bool InsertPositionBeforeTargetItem;
        private bool InsertPositionTargetItemCenter;
        private void GetInfo(IDropInfo dropInfo)
        {
            DataObject = dropInfo.Data as Component;

            TargetIndex = dropInfo.InsertIndex;
            SourceIndex = dropInfo.DragInfo.SourceIndex;

            VisualTarget = dropInfo.VisualTargetItem as TreeViewItem;
            VisualSource = dropInfo.DragInfo.VisualSourceItem as TreeViewItem; ;

            SourceComponent = dropInfo.DragInfo.SourceItem as Component;
            TargetComponent = dropInfo.TargetItem as Component;

            VisualTarget = dropInfo.VisualTargetItem as TreeViewItem;
            VisualSource = dropInfo.DragInfo.VisualSourceItem as TreeViewItem;

            TargetCollection = dropInfo.TargetCollection as ObservableCollection<Component>;
            SourceCollection = dropInfo.DragInfo.SourceCollection as ObservableCollection<Component>;

            ParentVisualSource = Utils.FindParent<TreeViewItem>(VisualSource);
            ParentVisualTarget = Utils.FindParent<TreeViewItem>(VisualTarget);

            InsertPositionTargetItemCenter = dropInfo.InsertPosition.HasFlag(RelativeInsertPosition.TargetItemCenter);
            InsertPositionAfterTargetItem = dropInfo.InsertPosition == RelativeInsertPosition.AfterTargetItem && TargetIndex == SourceIndex;
            InsertPositionBeforeTargetItem = dropInfo.InsertPosition == RelativeInsertPosition.BeforeTargetItem && TargetIndex == SourceIndex + 1;

            if (ParentVisualTarget != null)
            {
                TargetComponentParent = ParentVisualTarget.DataContext as Component;
                SourceComponentParent = ParentVisualSource.DataContext as Component;

                GrandParentVisualTarget = Utils.FindParent<TreeViewItem>(ParentVisualTarget);
                if (GrandParentVisualTarget != null)
                {
                    TargetComponentGrandParent = GrandParentVisualTarget.DataContext as Component;
                    ParentComponentIndex = TargetComponentGrandParent.Components.IndexOf(TargetComponentParent);
                    GrandParentCollectionSource = (ObservableCollection<Component>)GrandParentVisualTarget.ItemsSource;

                    GrandGrandParentVisualTarget = Utils.FindParent<TreeViewItem>(GrandParentVisualTarget);
                    if (GrandGrandParentVisualTarget != null)
                    {
                        TargetComponentGrandGrandParent = GrandGrandParentVisualTarget.DataContext as Component;
                        GrandParentComponentIndex = TargetComponentGrandGrandParent.Components.IndexOf(TargetComponentGrandParent);
                        GrandGrandParentCollectionSource = (ObservableCollection<Component>)GrandGrandParentVisualTarget.ItemsSource;
                    }
                }
            }
        }

        private void NullInfo()
        {
            DataObject = null;
            ParentVisualTarget = null;
            GrandParentVisualTarget = null;
            GrandGrandParentVisualTarget = null;

            TargetComponentParent = null;
            TargetComponentGrandParent = null;
            TargetComponentGrandGrandParent = null;

            GrandParentCollectionSource = null;
            GrandGrandParentCollectionSource = null;
        }

        public void StartDrag(IDragInfo dragInfo)
        {
            dragInfo.Effects = DragDropEffects.Copy | DragDropEffects.Move;
            dragInfo.Data = dragInfo.SourceItem;
        }

        public bool CanStartDrag(IDragInfo dragInfo)
        {
            var dragged = dragInfo.SourceItem;
            if (dragged is Entity || dragged is Layer || dragged is Scene)
                return true;
            else
                return false;
        }

        public void Dropped(IDropInfo dropInfo)
        {
            //throw new NotImplementedException();
        }

        public void DragDropOperationFinished(DragDropEffects operationResult, IDragInfo dragInfo)
        {
            NullInfo();
        }

        public void DragCancelled()
        {
            NullInfo();
        }

        public bool TryCatchOccurredException(Exception exception)
        {
            throw new NotImplementedException();
        }
    }
}
