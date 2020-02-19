using CMiX.MVVM.ViewModels;
using CMiX.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.Studio.ViewModels
{
    public class Outliner : ViewModel
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
            else if(SelectedComponent is Composition)
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
                CanAdd = false;
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
    }
}
