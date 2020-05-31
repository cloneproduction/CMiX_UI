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
            if (SelectedComponent is Scene || SelectedComponent is Mask)
            {
                AddContentText = "Add Entity";
                CanAdd = true;
            }
            else if (SelectedComponent is Composition)
            {
                AddContentText = "Add Layer";
                CanAdd = true;
            }
            else if(SelectedComponent is Project)
            {
                AddContentText = "Add Composition";
                CanAdd = true;
            }
            else
            {
                AddContentText = "Add";
                CanAdd = false;
            }
        }

        public void CanDuplicateComponent()
        {
            if (SelectedComponent is Composition || SelectedComponent is Layer || SelectedComponent is Entity)
                CanDuplicate = true;
            else
                CanDuplicate = false;
        }

        public void CanRenameComponent()
        {
            if (SelectedComponent is Project || SelectedComponent is Composition || SelectedComponent is Layer || SelectedComponent is Entity)
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
            if (SelectedComponent is Composition || SelectedComponent is Layer || SelectedComponent is Entity)
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
            var dataObject = dropInfo.Data as Component;
            var targetItem = dropInfo.TargetItem as Component;
            var sourceItem = dropInfo.DragInfo.SourceItem as Component;
            var targetIndex = dropInfo.InsertIndex;
            var sourceIndex = dropInfo.DragInfo.SourceIndex;

            var visualTarget = dropInfo.VisualTargetItem;
            var visualSource = dropInfo.DragInfo.VisualSourceItem;

            var parentVisualSource = Utils.FindParent<TreeViewItem>(visualSource);
            var parentTargetItem = Utils.FindParent<TreeViewItem>(visualTarget);

            var parentCollectionSource = (ObservableCollection<Component>)parentTargetItem.ItemsSource;

            var parentTargetItemIndex = parentCollectionSource.IndexOf(targetItem);

            bool InsertPositionAfterTargetItem = dropInfo.InsertPosition == RelativeInsertPosition.AfterTargetItem && targetIndex == sourceIndex;
            bool InsertPositionBeforeTargetItem = dropInfo.InsertPosition == RelativeInsertPosition.BeforeTargetItem && targetIndex == sourceIndex + 1;


            bool canDrop = false;

            if (targetItem != null)
            {
                //IS  Not OVER ITSELF
                if (sourceItem != targetItem)
                {
                    // NOT OVERRING THE CENTER PART
                    if (!dropInfo.InsertPosition.HasFlag(RelativeInsertPosition.TargetItemCenter))
                    {
                        // can't drop just next after
                        if (!InsertPositionAfterTargetItem && !InsertPositionBeforeTargetItem)
                        {
                            // parent can't be the target
                            if (parentVisualSource.DataContext != targetItem)
                            {
                                // parent source is parent target so it drop within
                                if (parentVisualSource == parentTargetItem)
                                {
                                    //drop only on same type
                                    if (dataObject.GetType() == targetItem.GetType())
                                    {
                                        canDrop = true;
                                        Console.WriteLine("CanDropOnSameParent");
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return canDrop;
        }

        public bool CanDropWithinDifferentParent(IDropInfo dropInfo)
        {
            var dataObject = dropInfo.Data as Component;
            var targetItem = dropInfo.TargetItem as Component;

            var visualTarget = dropInfo.VisualTargetItem;
            var visualSource = dropInfo.DragInfo.VisualSourceItem;

            var parentVisualSource = Utils.FindParent<TreeViewItem>(visualSource);
            var parentVisualTarget = Utils.FindParent<TreeViewItem>(visualTarget);

            bool canDrop = false;

            if (dataObject != null)
            {
                // drop on a different parent
                if (parentVisualSource != parentVisualTarget)
                {
                    //dropping on a same type
                    if (dataObject.GetType() == targetItem.GetType())
                    {
                        if (parentVisualTarget != parentVisualSource)
                        {
                            Console.WriteLine("CanDropWithinDifferentParent");
                            canDrop = true;
                            //dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
                        }
                    }
                }
            }
            return canDrop;
        }

        public bool CanDropEntityOnMask(IDropInfo dropInfo)
        {
            var dataObject = dropInfo.Data as Component;
            var dropTarget = dropInfo.TargetItem as Component;
            var targetItem = dropInfo.TargetItem as Component;

            var visualTarget = dropInfo.VisualTargetItem;
            var visualSource = dropInfo.DragInfo.VisualSourceItem;

            var parentSourceItem = Utils.FindParent<TreeViewItem>(visualSource);
            var parentTargetItem = Utils.FindParent<TreeViewItem>(visualTarget);

            bool canDrop = false;
            if (parentSourceItem != visualTarget && dataObject is Entity && (dropTarget is Mask || dropTarget is Scene))
            {
                canDrop = true;
            }
            return canDrop;
        }

        public bool CanDropOnLastItem(IDropInfo dropInfo)
        {
            bool canDrop = false;

            if (TargetComponent != null)
            {
                if (SourceComponent != TargetComponent)
                {
                    if (!dropInfo.InsertPosition.HasFlag(RelativeInsertPosition.TargetItemCenter))
                    {
                        if (!SourceComponent.Components.Contains(TargetComponent))
                        {
                            if (TargetComponent == TargetComponentParent.Components.Last())
                            {
                                if (dropInfo.InsertPosition.HasFlag(RelativeInsertPosition.AfterTargetItem))
                                {
                                    canDrop = true;
                                }
                            }
                        }
                    }
                }
            }
            return canDrop;
        }

        public void DragOver(IDropInfo dropInfo)
        {
            GetInfo(dropInfo);

            if(dropInfo.DragInfo != null && dropInfo.DragInfo.SourceItem is Component)
            {
                var dataObject = dropInfo.Data as Component;

                if (ParentVisualTarget != null)
                {
                    if (dataObject != null)
                        dropInfo.Effects = DragDropEffects.Copy | DragDropEffects.Move;

                    if (TargetComponent != null && SourceComponent is Component)
                    {
                        //IS  Not OVER ITSELF
                        if (SourceComponent != TargetComponent)
                        {
                            if (!dropInfo.InsertPosition.HasFlag(RelativeInsertPosition.TargetItemCenter))
                            {
                                if (ParentVisualSource.DataContext != TargetComponent)
                                {
                                    if (dataObject.GetType() == TargetComponent.GetType())
                                    {
                                        bool InsertPositionAfterTargetItem = dropInfo.InsertPosition == RelativeInsertPosition.AfterTargetItem && TargetIndex == SourceIndex;
                                        bool InsertPositionBeforeTargetItem = dropInfo.InsertPosition == RelativeInsertPosition.BeforeTargetItem && TargetIndex == SourceIndex + 1;
                                        if ((!InsertPositionAfterTargetItem && !InsertPositionBeforeTargetItem) || (ParentVisualTarget != ParentVisualSource))
                                        {
                                            dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
                                        }
                                    }
                                    else if (GrandParentVisualTarget != null)
                                    {
                                        if (ParentComponentIndex != SourceIndex - 1)
                                        {
                                            if (!SourceComponent.Components.Contains(TargetComponent))
                                            {
                                                if (TargetComponent == TargetComponentParent.Components.Last())
                                                {
                                                    if (dropInfo.InsertPosition.HasFlag(RelativeInsertPosition.AfterTargetItem))
                                                    {
                                                        dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (dataObject is Entity && (TargetComponent is Mask || TargetComponent is Scene))
                    {
                        if (ParentVisualSource != VisualTarget)
                        {
                            dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
                        }
                    }
                }
            }
        }

        public void Drop(IDropInfo dropInfo)
        {


            var insertIndex = dropInfo.InsertIndex;

            if (TargetCollection == null)
                return;

            if (CanDropOnSameParent(dropInfo))
            {
                if (SourceIndex < insertIndex)
                    insertIndex -= 1;

                TargetCollection.Move(SourceIndex, insertIndex);
            }
            else if(CanDropWithinDifferentParent(dropInfo))
            {
                TargetCollection.Insert(insertIndex, SourceComponent);
                SourceCollection.Remove(SourceComponent);
            }
            else if (CanDropEntityOnMask(dropInfo))
            {
                if (dropInfo.TargetItem is Mask || dropInfo.TargetItem is Scene)
                {
                    TargetComponent.Components.Insert(0, SourceComponent);
                    TargetComponent.IsExpanded = true;
                }
                SourceCollection.Remove(SourceComponent);
                return;
            }
            else if (CanDropOnLastItem(dropInfo))
            {
                if (TargetComponentGrandParent.GetType() == typeof(Composition))
                {
                    if (SourceIndex > ParentComponentIndex)
                        ParentComponentIndex += 1;

                    GrandParentCollectionSource.Move(SourceIndex, ParentComponentIndex);
                }
                else if (TargetComponentGrandParent.GetType() == typeof(Layer))
                {
                    if (SourceIndex > GrandParentComponentIndex)
                        GrandParentComponentIndex += 1;

                    GrandGrandParentCollectionSource.Move(SourceIndex, GrandParentComponentIndex);
                }
            }
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

        private void GetInfo(IDropInfo dropInfo)
        {
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
            if(ParentVisualTarget != null)
            {
                TargetComponentParent = ParentVisualTarget.DataContext as Component;

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
            if (dragged is Entity || dragged is Layer)
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
