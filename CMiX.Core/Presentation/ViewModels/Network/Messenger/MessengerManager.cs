// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System.Collections.ObjectModel;
using System.Windows.Input;
using CMiX.Core.Presentation.ViewModels.Components;
using CMiX.Core.Presentation.Views;
using MvvmDialogs;

namespace CMiX.Core.Presentation.ViewModels.Network
{
    public class MessengerManager : ViewModel
    {
        public MessengerManager(Project project)
        {
            Project = project;
            MessengerFactory = new MessengerFactory();
            DialogService = new DialogService(new CustomFrameworkDialogFactory(), new CustomTypeLocator());

            AddMessengerCommand = new RelayCommand(p => AddMessenger());
            DeleteMessengerCommand = new RelayCommand(p => DeleteMessenger(p as Messenger));
            RenameMessengerCommand = new RelayCommand(p => RenameServer(p as Server));
            EditMessengerSettingsCommand = new RelayCommand(p => EditMessengerSettings(p as Messenger));
        }


        public ICommand EditMessengerSettingsCommand { get; }
        public ICommand AddMessengerCommand { get; set; }
        public ICommand DeleteMessengerCommand { get; set; }
        public ICommand RenameMessengerCommand { get; set; }
        private IDialogService DialogService { get; set; }


        private Project Project { get; set; }
        private MessengerFactory MessengerFactory { get; set; }


        public ObservableCollection<Messenger> Messengers
        {
            get => Project.Messengers;
            //set => SetAndNotify(ref _messengers, value);
        }



        private Messenger _selectedMessenger;
        public Messenger SelectedMessenger
        {
            get => _selectedMessenger;
            set => SetAndNotify(ref _selectedMessenger, value);
        }


        public void EditMessengerSettings(Messenger messenger)
        {
            Settings settings = messenger.GetSettings();
            bool? success = DialogService.ShowDialog<MessengerSettingsWindow>(this, settings);
            if (success == true)
                messenger.SetSettings(settings);
        }

        public void AddMessenger()
        {
            var messenger = MessengerFactory.CreateMessenger();
            Messengers.Add(messenger);
        }

        private void DeleteMessenger(Messenger messenger)
        {
            if (messenger != null)
            {
                messenger.StopServer();
                Project.Messengers.Remove(messenger);

                if (Project.Messengers.Count > 0)
                {
                    SelectedMessenger = Project.Messengers[0];
                    return;
                }

                SelectedMessenger = null;
            }
        }

        private void RenameServer(Server obj)
        {
            if (obj != null)
                obj.IsRenaming = true;
        }
    }
}
