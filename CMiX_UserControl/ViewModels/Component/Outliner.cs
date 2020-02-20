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

            if (dataObject != null)
            {
                if (dataObject is Entity && dropTarget is Entity)
                    if (sourceIndex != targetIndex && sourceIndex != targetIndex - 1)
                    {
                        if (parentSourceItem == parentTargetItem)
                        {
                            canDrop = true;
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
                    if (dataObject is Entity && dropTarget is Layer)
                    {
                        canDrop = true;
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

            if(dataObject != null)
                dropInfo.Effects = DragDropEffects.Copy | DragDropEffects.Move;


            if (dataObject.GetType() == targetItem.GetType() && !dropInfo.InsertPosition.HasFlag(RelativeInsertPosition.TargetItemCenter))
            {
                if(!(targetItem == sourceItem))
                    dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
            }
            else if (dataObject is Entity && targetItem is Layer)
            {
                dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
            }
        }

        public void Drop(IDropInfo dropInfo)
        {
            var targetCollection = dropInfo.TargetCollection as ObservableCollection<IComponent>;
            var sourceCollection = dropInfo.DragInfo.SourceCollection as ObservableCollection<IComponent>;

            if (CanDropOnSameParent(dropInfo))
            {
                if (dropInfo.DragInfo.SourceIndex < dropInfo.InsertIndex)
                    targetCollection.Move(dropInfo.DragInfo.SourceIndex, dropInfo.InsertIndex - 1);
                else
                    targetCollection.Move(dropInfo.DragInfo.SourceIndex, dropInfo.InsertIndex);
            }
            else if(CanDropWithinDifferentParent(dropInfo))
            {
                targetCollection.Insert(dropInfo.InsertIndex, dropInfo.DragInfo.SourceItem as IComponent);
                sourceCollection.Remove(dropInfo.DragInfo.SourceItem as IComponent);
            }
            else if (CanDropEntityOnLayer(dropInfo))
            {
                var targetItem = dropInfo.TargetItem as Layer;
                var sourceItem = dropInfo.DragInfo.SourceItem as IComponent;
                targetItem.Components.Insert(0, sourceItem);
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
