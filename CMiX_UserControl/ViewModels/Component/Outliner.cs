using CMiX.MVVM.Resources;
using CMiX.MVVM.ViewModels;
using CMiX.ViewModels;
using GongSolutions.Wpf.DragDrop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CMiX.Studio.ViewModels
{
    public class Outliner : ViewModel, IDropTarget, IDragSource
    {
        public Outliner(ObservableCollection<IComponent> components)
        {
            Components = components;
        }

        private ObservableCollection<IComponent> _components;
        public ObservableCollection<IComponent> Components
        {
            get => _components;
            set => SetAndNotify(ref _components, value);
        }

        private IComponent _selectedComponent;
        public IComponent SelectedComponent
        {
            get => _selectedComponent;
            set
            {
                SetAndNotify(ref _selectedComponent, value);
                CanAddComponent();
                CanRenameComponent();
                CanEditComponent();
                CanDuplicateComponent();
                CanDeleteComponent();
            }
        }

        private string _isEnabled;
        public string IsEnalbed
        {
            get => _isEnabled;
            set => SetAndNotify(ref _isEnabled, value);
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
            if (SelectedComponent is Layer)
            {
                AddContentText = "Add Entity";
                CanAdd = true;
            }
            else if (SelectedComponent is Composition)
            {
                AddContentText = "Add Layer";
                CanAdd = true;
            }
            else if (SelectedComponent is Project)
            {
                AddContentText = "Add Composition";
                CanAdd = true;
            }
            else if (SelectedComponent is Entity)
            {
                AddContentText = "Add";
                CanAdd = false;
            }

        }

        public void CanDuplicateComponent()
        {
            if (SelectedComponent is Project)
                CanDuplicate = false;
            else
                CanDuplicate = true;
        }

        public void CanRenameComponent()
        {
            if (SelectedComponent is Project)
                CanRename = false;
            else
                CanRename = true;
        }

        public void CanEditComponent()
        {
            CanEdit = true;
        }

        public void CanDeleteComponent()
        {
            if (SelectedComponent is Project)
                CanDelete = false;
            else
                CanDelete = true;
        }

        public bool CanDropOnSameParent(IDropInfo dropInfo)
        {
            var dataObject = dropInfo.Data as IComponent;
            var targetItem = dropInfo.TargetItem as IComponent;
            var sourceItem = dropInfo.DragInfo.SourceItem as IComponent;
            var targetIndex = dropInfo.InsertIndex;
            var sourceIndex = dropInfo.DragInfo.SourceIndex;

            var visualTarget = dropInfo.VisualTargetItem;
            var visualSource = dropInfo.DragInfo.VisualSourceItem;

            var parentVisualSource = Utils.FindParent<TreeViewItem>(visualSource);
            var parentTargetItem = Utils.FindParent<TreeViewItem>(visualTarget);

            var parentCollectionSource = (ObservableCollection<IComponent>)parentTargetItem.ItemsSource;

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
                            if (parentVisualSource != targetItem)
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
            var dataObject = dropInfo.Data as IComponent;
            var targetItem = dropInfo.TargetItem as IComponent;
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

        public bool CanDropEntityOnLayer(IDropInfo dropInfo)
        {
            var dataObject = dropInfo.Data as IComponent;
            var dropTarget = dropInfo.TargetItem as IComponent;
            var targetItem = dropInfo.TargetItem as IComponent;
           
            var visualTarget = dropInfo.VisualTargetItem;
            var visualSource = dropInfo.DragInfo.VisualSourceItem;

            var parentSourceItem = Utils.FindParent<TreeViewItem>(visualSource);
            var parentTargetItem = Utils.FindParent<TreeViewItem>(visualTarget);

            bool canDrop = false;
            if (parentSourceItem != visualTarget)
            {
                if (dataObject is Entity && dropTarget is Layer)
                {
                    canDrop = true;
                    Console.WriteLine("CanDropEntityOnLayer");
                }
            }
            return canDrop;
        }

        public bool CanDropOnLastItem(IDropInfo dropInfo)
        {
            var targetItem = dropInfo.TargetItem as IComponent;
            var sourceItem = dropInfo.DragInfo.SourceItem as IComponent;
            var visualTarget = dropInfo.VisualTargetItem;
            var parentTargetItem = Utils.FindParent<TreeViewItem>(visualTarget);

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
                        if (!sourceItem.Components.Contains(targetItem))
                        {
                            if (targetItem == ((IComponent)parentTargetItem.DataContext).Components.Last())
                            {
                                if (dropInfo.InsertPosition.HasFlag(RelativeInsertPosition.AfterTargetItem))
                                {
                                    canDrop = true;
                                    Console.WriteLine("CanDropOnLastItem");
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
            var dataObject = dropInfo.Data as IComponent;
            var targetItem = dropInfo.TargetItem as IComponent;
            var sourceItem = dropInfo.DragInfo.SourceItem as IComponent;
            var targetIndex = dropInfo.InsertIndex;
            var sourceIndex = dropInfo.DragInfo.SourceIndex;

            var visualSource = dropInfo.DragInfo.VisualSourceItem;

            var parentVisualSource = Utils.FindParent<TreeViewItem>(visualSource);
            var visualTarget = dropInfo.VisualTargetItem;
            var parentVisualTarget = Utils.FindParent<TreeViewItem>(visualTarget);
            var grandParentVisualTarget = Utils.FindParent<TreeViewItem>(parentVisualTarget);

            if (parentVisualTarget != null)
            {
                if (dataObject != null)
                    dropInfo.Effects = DragDropEffects.Copy | DragDropEffects.Move;

                if (targetItem != null)
                {
                    //IS  Not OVER ITSELF
                    if (sourceItem != targetItem)
                    {
                        //Console.WriteLine("SourceIsDifferentFromTarget");
                        if (!dropInfo.InsertPosition.HasFlag(RelativeInsertPosition.TargetItemCenter))
                        {
                            //Console.WriteLine("NotOverringTheCenterPart");
                            if (parentVisualSource != targetItem)
                            {
                                //Console.WriteLine("ParentVisualSource is different from Target");
                                if (dataObject.GetType() == targetItem.GetType())
                                {
                                    bool InsertPositionAfterTargetItem = dropInfo.InsertPosition == RelativeInsertPosition.AfterTargetItem && targetIndex == sourceIndex;
                                    bool InsertPositionBeforeTargetItem = dropInfo.InsertPosition == RelativeInsertPosition.BeforeTargetItem && targetIndex == sourceIndex + 1;
                                    if ((!InsertPositionAfterTargetItem && !InsertPositionBeforeTargetItem) || (parentVisualTarget != parentVisualSource))
                                    {
                                        dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
                                    }
                                }
                                else
                                {
                                    //Console.WriteLine("Target and source are different type");
                                    if(grandParentVisualTarget != null)
                                    {
                                        var parentIndex = ((ObservableCollection<IComponent>)grandParentVisualTarget.ItemsSource).IndexOf((IComponent)parentVisualTarget.DataContext);

                                        if (parentIndex != sourceIndex - 1)
                                        {
                                            Console.WriteLine("parentIndex " + parentIndex);
                                            if (!sourceItem.Components.Contains(targetItem))
                                            {
                                                //Console.WriteLine("Source doesnt contains targets");
                                                if (targetItem == ((IComponent)parentVisualTarget.DataContext).Components.Last())
                                                {
                                                    //Console.WriteLine("targetItem is last element of collection");
                                                    if (dropInfo.InsertPosition.HasFlag(RelativeInsertPosition.AfterTargetItem))
                                                    {
                                                        //Console.WriteLine("dropInfo.InsertPosition.HasFlag(RelativeInsertPosition.AfterTargetItem)");
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
                }

                if (dataObject is Entity && targetItem is Layer)
                {
                    if (parentVisualSource != visualTarget)
                    {
                        dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
                    }
                }
            }

        }

        public void Drop(IDropInfo dropInfo)
        {
            var targetCollection = dropInfo.TargetCollection as ObservableCollection<IComponent>;
            var sourceCollection = dropInfo.DragInfo.SourceCollection as ObservableCollection<IComponent>;
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
                targetCollection.Insert(insertIndex, dropInfo.DragInfo.SourceItem as IComponent);
                sourceCollection.Remove(dropInfo.DragInfo.SourceItem as IComponent);
                return;
            }
            else if (CanDropEntityOnLayer(dropInfo))
            {
                var targetItem = dropInfo.TargetItem as Layer;
                var sourceItem = dropInfo.DragInfo.SourceItem as IComponent;
                targetItem.Components.Insert(0, sourceItem);
                sourceCollection.Remove(sourceItem);
                targetItem.IsExpanded = true;
                
                return;
            }
            else if (CanDropOnLastItem(dropInfo))
            {
                var sourceItem = dropInfo.DragInfo.SourceItem as IComponent;
                var visualTarget = dropInfo.VisualTargetItem;

                var parentVisualTarget = Utils.FindParent<TreeViewItem>(visualTarget);
                var grandParentVisualTarget = Utils.FindParent<TreeViewItem>(parentVisualTarget);

                var parentTargetDataContext = parentVisualTarget.DataContext as IComponent;
                var grandParentTargetDataContext = grandParentVisualTarget.DataContext as IComponent;

                var parentIndex = grandParentTargetDataContext.Components.IndexOf(parentTargetDataContext);

                var grandParentCollectionSource = (ObservableCollection<IComponent>)grandParentVisualTarget.ItemsSource;

                if (sourceIndex < parentIndex)
                {
                    grandParentCollectionSource.Move(sourceIndex, parentIndex);
                }
                else
                {
                    grandParentCollectionSource.Move(sourceIndex, parentIndex + 1);
                }
            }
        }


        public int FindItemParentIndex(ObservableCollection<IComponent> components, IComponent componentToFind)
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
            Console.WriteLine("index " + index);
            return index;
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
