using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GongSolutions.Wpf.DragDrop;
using CMiX.MVVM.Resources;
using CMiX.MVVM.ViewModels;

namespace CMiX.Studio.ViewModels
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
            else
            {
                AddContentText = "Add Composition";
                CanAdd = true;
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
            //var targetIndex = dropInfo.InsertIndex;
            //var sourceIndex = dropInfo.DragInfo.SourceIndex;

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
            var targetItem = dropInfo.TargetItem as Component;
            var sourceItem = dropInfo.DragInfo.SourceItem as Component;
            var visualTarget = dropInfo.VisualTargetItem;
            var parentTargetItem = Utils.FindParent<TreeViewItem>(visualTarget);

            bool canDrop = false;

            if (targetItem != null)
            {
                if (sourceItem != targetItem)
                {
                    if (!dropInfo.InsertPosition.HasFlag(RelativeInsertPosition.TargetItemCenter))
                    {
                        if (!sourceItem.Components.Contains(targetItem))
                        {
                            if (targetItem == ((Component)parentTargetItem.DataContext).Components.Last())
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
            if(dropInfo.DragInfo != null && dropInfo.DragInfo.SourceItem is Component)
            {
                var dataObject = dropInfo.Data as Component;
                var targetItem = dropInfo.TargetItem as Component;
                var sourceItem = dropInfo.DragInfo.SourceItem as Component;

                var targetIndex = dropInfo.InsertIndex;
                var sourceIndex = dropInfo.DragInfo.SourceIndex;

                var visualSource = dropInfo.DragInfo.VisualSourceItem;
                var visualTarget = dropInfo.VisualTargetItem;

                var parentVisualSource = Utils.FindParent<TreeViewItem>(visualSource);
                var parentVisualTarget = Utils.FindParent<TreeViewItem>(visualTarget);
                var grandParentVisualTarget = Utils.FindParent<TreeViewItem>(parentVisualTarget);

                if (parentVisualTarget != null)
                {
                    if (dataObject != null)
                        dropInfo.Effects = DragDropEffects.Copy | DragDropEffects.Move;

                    if (targetItem != null && sourceItem is Component)
                    {
                        //IS  Not OVER ITSELF
                        if (sourceItem != targetItem)
                        {
                            if (!dropInfo.InsertPosition.HasFlag(RelativeInsertPosition.TargetItemCenter))
                            {
                                if (parentVisualSource.DataContext != targetItem)
                                {
                                    if (dataObject.GetType() == targetItem.GetType())
                                    {
                                        bool InsertPositionAfterTargetItem = dropInfo.InsertPosition == RelativeInsertPosition.AfterTargetItem && targetIndex == sourceIndex;
                                        bool InsertPositionBeforeTargetItem = dropInfo.InsertPosition == RelativeInsertPosition.BeforeTargetItem && targetIndex == sourceIndex + 1;
                                        if ((!InsertPositionAfterTargetItem && !InsertPositionBeforeTargetItem) || (parentVisualTarget != parentVisualSource))
                                        {
                                            dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
                                        }
                                    }
                                    else if (grandParentVisualTarget != null)
                                    {
                                        var parentIndex = ((ObservableCollection<Component>)grandParentVisualTarget.ItemsSource).IndexOf((Component)parentVisualTarget.DataContext);

                                        if (parentIndex != sourceIndex - 1)
                                        {
                                            if (!sourceItem.Components.Contains(targetItem))
                                            {
                                                if (targetItem == ((Component)parentVisualTarget.DataContext).Components.Last())
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

                    if (dataObject is Entity && (targetItem is Mask || targetItem is Scene))
                    {
                        if (parentVisualSource != visualTarget)
                        {
                            dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
                        }
                    }
                }
            }

        }

        public void Drop(IDropInfo dropInfo)
        {
            var targetCollection = dropInfo.TargetCollection as ObservableCollection<Component>;
            var sourceCollection = dropInfo.DragInfo.SourceCollection as ObservableCollection<Component>;
            var insertIndex = dropInfo.InsertIndex;
            var sourceIndex = dropInfo.DragInfo.SourceIndex;

            if (targetCollection == null)
                return;

            if (CanDropOnSameParent(dropInfo))
            {
                if (sourceIndex < insertIndex)
                {
                    targetCollection.Move(sourceIndex, insertIndex - 1);
                }
                else
                {
                    targetCollection.Move(sourceIndex, insertIndex);
                }
                return;
            }
            else if(CanDropWithinDifferentParent(dropInfo))
            {
                targetCollection.Insert(insertIndex, dropInfo.DragInfo.SourceItem as Component);
                sourceCollection.Remove(dropInfo.DragInfo.SourceItem as Component);
                return;
            }
            else if (CanDropEntityOnMask(dropInfo))
            {
                var sourceItem = dropInfo.DragInfo.SourceItem as Component;
                if (dropInfo.TargetItem is Mask)
                {
                    var targetItem = dropInfo.TargetItem as Mask;
                    targetItem.Components.Insert(0, sourceItem);
                    targetItem.IsExpanded = true;
                }
                else if (dropInfo.TargetItem is Scene)
                {
                    var targetItem = dropInfo.TargetItem as Scene;
                    targetItem.Components.Insert(0, sourceItem);
                    targetItem.IsExpanded = true;
                }
                sourceCollection.Remove(sourceItem);
                return;
            }
            else if (CanDropOnLastItem(dropInfo))
            {
                var sourceItem = dropInfo.DragInfo.SourceItem as Component;
                var visualTarget = dropInfo.VisualTargetItem;

                var parentVisualTarget = Utils.FindParent<TreeViewItem>(visualTarget);
                var grandParentVisualTarget = Utils.FindParent<TreeViewItem>(parentVisualTarget);
                var grandGrandParentVisualTarget = Utils.FindParent<TreeViewItem>(grandParentVisualTarget);

                var parentTargetDataContext = parentVisualTarget.DataContext as Component;
                var grandParentTargetDataContext = grandParentVisualTarget.DataContext as Component;
                var grandGrandParentTargetDataContext = grandGrandParentVisualTarget.DataContext as Component;

                var parentIndex = grandParentTargetDataContext.Components.IndexOf(parentTargetDataContext);
                var grandParentIndex = grandGrandParentTargetDataContext.Components.IndexOf(grandParentTargetDataContext);

                var grandParentCollectionSource = (ObservableCollection<Component>)grandParentVisualTarget.ItemsSource;
                var grandGrandParentCollectionSource = (ObservableCollection<Component>)grandGrandParentVisualTarget.ItemsSource;

                Console.WriteLine("grandParentTargetDataContext TYPE " + grandParentTargetDataContext.GetType());

                if (grandParentTargetDataContext.GetType() == typeof(Composition))
                {
                    if (sourceIndex < parentIndex)
                        grandParentCollectionSource.Move(sourceIndex, parentIndex);
                    else
                        grandParentCollectionSource.Move(sourceIndex, parentIndex + 1);
                }
                else if (grandParentTargetDataContext.GetType() == typeof(Layer))
                {
                    if (sourceIndex < grandParentIndex)
                        grandGrandParentCollectionSource.Move(sourceIndex, grandParentIndex);
                    else
                        grandGrandParentCollectionSource.Move(sourceIndex, grandParentIndex + 1);
                }
            }
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
            //throw new NotImplementedException();
        }

        public void DragCancelled()
        {
            //throw new NotImplementedException();
        }

        public bool TryCatchOccurredException(Exception exception)
        {
            throw new NotImplementedException();
        }
    }
}
