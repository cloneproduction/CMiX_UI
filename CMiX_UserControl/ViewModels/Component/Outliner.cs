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
            var dropTarget = dropInfo.TargetItem as IComponent;
            var targetIndex = dropInfo.InsertIndex;
            var sourceIndex = dropInfo.DragInfo.SourceIndex;

            var visualTarget = dropInfo.VisualTargetItem;
            var visualSource = dropInfo.DragInfo.VisualSourceItem;

            var parentSourceItem = Utils.FindParent<TreeViewItem>(visualSource);
            var parentTargetItem = Utils.FindParent<TreeViewItem>(visualTarget);

            bool canDrop = false;

                

            if (dataObject != null && dropTarget != null)
            {
                if (!dropInfo.InsertPosition.HasFlag(RelativeInsertPosition.TargetItemCenter))
                    if (sourceIndex >= 0 && targetIndex >= 0)
                    {
                    
                        if (visualTarget != visualSource)
                            if (dataObject.GetType() == dropTarget.GetType())
                                if (sourceIndex != targetIndex && sourceIndex != targetIndex - 1)
                                {
                                    if (parentSourceItem == parentTargetItem)
                                    {
                                        canDrop = true;
                                    }
                                }
                }

            }
            return canDrop;
        }

        public bool CanDropWithinDifferentParent(IDropInfo dropInfo)
        {
            var dataObject = dropInfo.Data as IComponent;
            var dropTarget = dropInfo.TargetItem as IComponent;
            var targetIndex = dropInfo.InsertIndex;
            var sourceIndex = dropInfo.DragInfo.SourceIndex;

            var visualTarget = dropInfo.VisualTargetItem;
            var visualSource = dropInfo.DragInfo.VisualSourceItem;

            var parentSourceItem = Utils.FindParent<TreeViewItem>(visualSource);
            var parentTargetItem = Utils.FindParent<TreeViewItem>(visualTarget);

            bool canDrop = false;

            if (dataObject != null)
            {
                if (parentSourceItem != parentTargetItem)
                {
                    if (dataObject is Entity && dropTarget is Entity)
                    {
                        canDrop = true;
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

            var visualTarget = dropInfo.VisualTargetItem;
            var visualSource = dropInfo.DragInfo.VisualSourceItem;

            var parentSourceItem = Utils.FindParent<TreeViewItem>(visualSource);
            var parentTargetItem = Utils.FindParent<TreeViewItem>(visualTarget);

            var grandParentTargetItem = Utils.FindParent<TreeViewItem>(parentTargetItem);

            var parentCollectionSource = (ObservableCollection<IComponent>)parentTargetItem.ItemsSource;
            var grandParentCollectionSource = (ObservableCollection<IComponent>)grandParentTargetItem.ItemsSource;

            var parentTargetItemIndex = parentCollectionSource.IndexOf(targetItem);

            bool InsertPositionAfterTargetItem = dropInfo.InsertPosition == RelativeInsertPosition.AfterTargetItem;
            bool InsertPositionBeforeTargetItem = dropInfo.InsertPosition == RelativeInsertPosition.BeforeTargetItem;

            //bool InsertJustBefore = InsertPositionIsBefore && visualSource == visualTarget;
            //bool InsertJustAfter = InsertPositionIsAfter && visualSource == visualTarget;

            if (dataObject != null)
                dropInfo.Effects = DragDropEffects.Copy | DragDropEffects.Move;

            if (targetItem != null)
            {
                if (sourceItem != targetItem)//IS  Not OVER ITSELF
                {
                    if (!dropInfo.InsertPosition.HasFlag(RelativeInsertPosition.TargetItemCenter))// NOT OVERRING THE CENTER PART
                    {
                        if (!(InsertPositionAfterTargetItem && targetIndex == sourceIndex) && !(InsertPositionBeforeTargetItem && targetIndex == sourceIndex + 1))// can't drop just next after
                        {
                            if (parentSourceItem != targetItem) // can't drop on it's own parent
                            {
                                if (dataObject.GetType() == targetItem.GetType()) //can drop on same type
                                {
                                    dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
                                }     
                                else if (parentTargetItemIndex == parentCollectionSource.Count - 1 && dropInfo.InsertPosition == RelativeInsertPosition.AfterTargetItem)
                                {
                                    dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
                                }
                            }
                        }
                    }
                }

            }
            if (dataObject is Entity && targetItem is Layer)
            {
                if (parentSourceItem != visualTarget)
                {
                    dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
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
                Console.WriteLine("CanDropOnSameParent");
                if (sourceIndex < insertIndex)
                {
                    targetCollection.Move(sourceIndex, insertIndex - 1);
                }
                else
                {
                    targetCollection.Move(sourceIndex, insertIndex);
                }
                    
            }
            else if(CanDropWithinDifferentParent(dropInfo))
            {
                Console.WriteLine("CanDropWithinDifferentParent");
                targetCollection.Insert(insertIndex, dropInfo.DragInfo.SourceItem as IComponent);
                sourceCollection.Remove(dropInfo.DragInfo.SourceItem as IComponent);
            }
            else if (CanDropEntityOnLayer(dropInfo))
            {
                Console.WriteLine("CanDropEntityOnLayer");
                var targetItem = dropInfo.TargetItem as Layer;
                var sourceItem = dropInfo.DragInfo.SourceItem as IComponent;
                targetItem.Components.Insert(0, sourceItem);
                targetItem.IsExpanded = true;
                sourceCollection.Remove(sourceItem);
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
