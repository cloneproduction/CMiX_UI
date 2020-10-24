using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using GongSolutions.Wpf.DragDrop;
using CMiX.MVVM.Resources;

namespace CMiX.MVVM.ViewModels
{
    public static class DragDropCheck
    {
        public static void DropFromLastItem(TreeViewItem target, TreeViewItem source)
        {
            if (target != null && source != null)
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

        public static void DropOnSameTargetType(TreeViewItem target, TreeViewItem source)
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

        public static void DropInSameParentType(TreeViewItem target, TreeViewItem source)
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

                if (targetParentComponent.GetType() == sourceComponent.GetType())
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

        public static void DropInDifferentParent(TreeViewItem target, TreeViewItem source)
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

        public static Action<TreeViewItem, TreeViewItem> DoDropInParent { get; set; }


        public static bool CheckTargetIsSameType(IDropInfo dropInfo)
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

        public static bool CheckParentIsSameType(TreeViewItem target, TreeViewItem source, RelativeInsertPosition relativeInsertPosition)
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

                if (
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

        public static bool CheckItemIsLast(TreeViewItem target, TreeViewItem source, RelativeInsertPosition relativeInsertPosition)
        {
            var parent = Utils.FindParent<TreeViewItem>(target);
            if (parent == null)
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



        private static TreeViewItem ParentVisualSource;
        private static TreeViewItem VisualTarget;
        private static TreeViewItem VisualSource;
        private static TreeViewItem ParentVisualTarget;

        private static Component SourceComponent;
        private static Component SourceComponentParent;
        private static Component TargetComponent;
        private static Component TargetComponentParent;

        private static int TargetIndex = -1;
        private static int SourceIndex = -1;

        private static bool InsertPositionTargetItemCenter;


        private static void GetInfo(IDropInfo dropInfo)
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
    }
}
