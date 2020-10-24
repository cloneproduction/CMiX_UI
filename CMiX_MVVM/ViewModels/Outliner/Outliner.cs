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
            CanRename = true;
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


        


        private void DropFromLastItem(TreeViewItem target, TreeViewItem source)
        {
            if(target != null && source != null)
            {
                TreeViewItem targetParent = Utils.FindParent<TreeViewItem>(target);
                TreeViewItem sourceParent = Utils.FindParent<TreeViewItem>(source);

                Component sourceComponent = source.DataContext as Component;
                Component targetParentComponent = targetParent.DataContext as Component;
                Component sourceParentComponent = sourceParent.DataContext as Component;

                if (targetParentComponent.GetType() == sourceComponent.GetType())
                {
                    Component targetGrandParentComponent = Utils.FindParent<TreeViewItem>(targetParent).DataContext as Component;
                    sourceParentComponent.Components.Remove(sourceComponent);
                    targetGrandParentComponent.Components.Add(sourceComponent);

                    sourceParentComponent.OnSendChange(sourceParentComponent.GetModel(), sourceParentComponent.GetMessageAddress());
                    targetGrandParentComponent.OnSendChange(targetGrandParentComponent.GetModel(), targetGrandParentComponent.GetMessageAddress());
                }
                else
                {
                    DropFromLastItem(targetParent, source);
                }
            }
        }

        private void DropOnSameTargetType(TreeViewItem target, TreeViewItem source)
        {
            if (target != null && source != null)
            {
                TreeViewItem targetParent = Utils.FindParent<TreeViewItem>(target);
                TreeViewItem sourceParent = Utils.FindParent<TreeViewItem>(source);

                Component sourceComponent = source.DataContext as Component;
                Component targetParentComponent = targetParent.DataContext as Component;
                Component sourceParentComponent = sourceParent.DataContext as Component;

                if (SourceIndex < TargetIndex && targetParentComponent == sourceParentComponent)
                    TargetIndex -= 1;

                sourceParentComponent.Components.Remove(sourceComponent);
                targetParentComponent.Components.Insert(TargetIndex, sourceComponent);

                sourceParentComponent.OnSendChange(SourceComponentParent.GetModel(), sourceParentComponent.GetMessageAddress());
                targetParentComponent.OnSendChange(targetParentComponent.GetModel(), targetParentComponent.GetMessageAddress());
            }
        }

        private void DropInSameParentType(TreeViewItem target, TreeViewItem source)
        {
            if (target != null && source != null)
            {
                TreeViewItem targetParent = Utils.FindParent<TreeViewItem>(target);
                TreeViewItem sourceParent = Utils.FindParent<TreeViewItem>(source);
                TreeViewItem targetGrandParent = Utils.FindParent<TreeViewItem>(targetParent);

                Component sourceComponent = source.DataContext as Component;
                Component targetParentComponent = targetParent.DataContext as Component;
                Component targetGrandParentComponent = targetGrandParent.DataContext as Component;
                Component sourceParentComponent = sourceParent.DataContext as Component;

                var targetParentIndex = targetGrandParentComponent.Components.IndexOf(targetParentComponent);

                if(targetParentComponent.GetType() == sourceComponent.GetType())
                {
                    if (sourceParentComponent == targetGrandParentComponent)
                    {
                        if (SourceIndex > targetParentIndex)
                            targetParentIndex += 1;

                        targetGrandParentComponent.Components.Move(SourceIndex, targetParentIndex);
                        targetGrandParentComponent.OnSendChange(targetGrandParentComponent.GetModel(), targetGrandParentComponent.GetMessageAddress());
                    }
                    else
                    {
                        sourceParentComponent.Components.Remove(sourceComponent);
                        targetGrandParentComponent.Components.Insert(targetParentIndex + 1, sourceComponent);

                        sourceParentComponent.OnSendChange(sourceParentComponent.GetModel(), sourceParentComponent.GetMessageAddress());
                        targetGrandParentComponent.OnSendChange(targetGrandParentComponent.GetModel(), targetGrandParentComponent.GetMessageAddress());
                    }

                }
                else
                {
                    DropInSameParentType(targetParent, source);
                }
            }
        }

        private void DropInDifferentParent(TreeViewItem target, TreeViewItem source)
        {
            if (target != null && source != null)
            {
                TreeViewItem targetParent = Utils.FindParent<TreeViewItem>(target);
                TreeViewItem sourceParent = Utils.FindParent<TreeViewItem>(source);

                Component sourceComponent = source.DataContext as Component;
                Component targetComponent = target.DataContext as Component;
                Component sourceParentComponent = sourceParent.DataContext as Component;

                sourceParentComponent.Components.Remove(sourceComponent);
                targetComponent.Components.Insert(0, sourceComponent);

                sourceParentComponent.OnSendChange(sourceParentComponent.GetModel(), sourceParentComponent.GetMessageAddress());
                targetComponent.OnSendChange(targetComponent.GetModel(), targetComponent.GetMessageAddress());
            }
        }

        private Action<TreeViewItem, TreeViewItem> DoDropInParent { get; set; }


        private bool CheckTargetIsSameType(IDropInfo dropInfo)
        {
            if (SourceComponent.GetType() == TargetComponent.GetType())
            {
                if (dropInfo.InsertIndex == dropInfo.DragInfo.SourceIndex && dropInfo.InsertPosition == RelativeInsertPosition.AfterTargetItem
                    && SourceComponentParent != TargetComponentParent)
                {
                    return true;
                }
                else if (dropInfo.InsertIndex - 1 == dropInfo.DragInfo.SourceIndex && dropInfo.InsertPosition == RelativeInsertPosition.BeforeTargetItem
                    && SourceComponentParent != TargetComponentParent)
                {
                    return true;
                }
                else if (dropInfo.InsertIndex - 1 == dropInfo.DragInfo.SourceIndex && dropInfo.InsertPosition == RelativeInsertPosition.BeforeTargetItem)
                {
                    return false;
                }
                else if (dropInfo.InsertIndex == dropInfo.DragInfo.SourceIndex && dropInfo.InsertPosition == RelativeInsertPosition.AfterTargetItem)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
                return false;
        }

        private bool CheckParentIsSameType(TreeViewItem target, TreeViewItem source, RelativeInsertPosition relativeInsertPosition)
        {
            var parent = Utils.FindParent<TreeViewItem>(target);
            if (parent == null)
            {
                return false;
            }
            else
            {
                var parentComponent = parent.DataContext as Component;
                var parentCollection = parentComponent.Components;
                var sourceComponent = source.DataContext as Component;
                var targetComponent = target.DataContext as Component;


                if (parentComponent.GetType() != sourceComponent.GetType()) 
                {
                    return false;
                }

                if(
                    parentCollection.Last() == targetComponent 
                    && relativeInsertPosition == RelativeInsertPosition.AfterTargetItem
                    && parentComponent != sourceComponent)
                {
                    return true;
                }
                else
                {
                    return CheckParentIsSameType(parent, source, relativeInsertPosition);
                }
            }
        }

        private bool CheckItemIsLast(TreeViewItem target, TreeViewItem source, RelativeInsertPosition relativeInsertPosition)
        {
            var parent = Utils.FindParent<TreeViewItem>(target);
            if(parent == null)
            {
                return true;
            }
            else
            {
                var collection = (ObservableCollection<Component>)parent.ItemsSource;
                var parentComponent = parent.DataContext as Component;
                var targetComponent = target.DataContext as Component;
                var sourceComponent = source.DataContext as Component;
                if (collection != null 
                    && collection.Last() == targetComponent 
                    && parentComponent != sourceComponent 
                    && relativeInsertPosition == RelativeInsertPosition.AfterTargetItem)
                {
                    return CheckItemIsLast(parent, source, relativeInsertPosition);
                }
                else
                {
                    return false;
                }
            }
        }



        private TreeViewItem ParentVisualSource;
        private TreeViewItem VisualTarget;
        private TreeViewItem VisualSource;
        private TreeViewItem ParentVisualTarget;

        private Component SourceComponent;
        private Component SourceComponentParent;
        private Component TargetComponent;
        private Component TargetComponentParent;

        private int TargetIndex = -1;
        private int SourceIndex = -1;

        private bool InsertPositionTargetItemCenter;


        private void GetInfo(IDropInfo dropInfo)
        {
            TargetIndex = dropInfo.InsertIndex;
            SourceIndex = dropInfo.DragInfo.SourceIndex;

            VisualTarget = dropInfo.VisualTargetItem as TreeViewItem;
            VisualSource = dropInfo.DragInfo.VisualSourceItem as TreeViewItem;

            SourceComponent = dropInfo.DragInfo.SourceItem as Component;
            TargetComponent = dropInfo.TargetItem as Component;

            ParentVisualSource = Utils.FindParent<TreeViewItem>(VisualSource);
            ParentVisualTarget = Utils.FindParent<TreeViewItem>(VisualTarget);

            InsertPositionTargetItemCenter = dropInfo.InsertPosition.HasFlag(RelativeInsertPosition.TargetItemCenter);

            if (ParentVisualTarget != null)
            {
                TargetComponentParent = ParentVisualTarget.DataContext as Component;
                SourceComponentParent = ParentVisualSource.DataContext as Component;
            }
        }



        public void Drop(IDropInfo dropInfo)
        {
            DoDropInParent.Invoke(VisualTarget, VisualSource);
        }

        public void DragOver(IDropInfo dropInfo)
        {
            if (!dropInfo.Data.GetType().IsSubclassOf(typeof(Component)))
                return;

            GetInfo(dropInfo);

            if (TargetComponent == null || TargetComponentParent == null)
                return;

            if (VisualTarget == VisualSource)
                return;

            if (ParentVisualTarget == null)
                return;

            if (SourceComponentParent != TargetComponent && SourceComponentParent.GetType() != typeof(Project)) // can't over it's own parent
            {
                if (SourceComponentParent.GetType() == TargetComponent.GetType())
                {
                    DoDropInParent = DropInDifferentParent;
                    dropInfo.Effects = DragDropEffects.Copy | DragDropEffects.Move;
                    dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
                }
                else if (!InsertPositionTargetItemCenter)
                {
                    if (CheckTargetIsSameType(dropInfo))
                    {
                        DoDropInParent = DropOnSameTargetType;
                        dropInfo.Effects = DragDropEffects.Copy | DragDropEffects.Move;
                        dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
                    }

                    else if (CheckParentIsSameType(VisualTarget, VisualSource, dropInfo.InsertPosition))
                    {
                        DoDropInParent = DropInSameParentType;
                        dropInfo.Effects = DragDropEffects.Copy | DragDropEffects.Move;
                        dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
                    }
                    else if (CheckItemIsLast(VisualTarget, VisualSource, dropInfo.InsertPosition))
                    {
                        DoDropInParent = DropFromLastItem;
                        dropInfo.Effects = DragDropEffects.Copy | DragDropEffects.Move;
                        dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
                    }
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
            if (dragged is Entity || dragged is Layer || dragged is Scene || dragged is Composition)
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
            //NullInfo();
        }

        public void DragCancelled()
        {
           //NullInfo();
        }

        public bool TryCatchOccurredException(Exception exception)
        {
            throw new NotImplementedException();
        }
    }
}
