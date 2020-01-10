using CMiX.MVVM.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CMiX.MVVM.Services
{
    public class MessageValidationManager : ViewModel
    {
        public MessageValidationManager(MessageService messageService)
        {
            AddMessageValidationCommand = new RelayCommand(p => AddMessageValidation());
            RemoveMessageValidationCommand = new RelayCommand(p => RemoveMessageValidation());
        }

        public ICommand AddMessageValidationCommand { get; set; }
        public ICommand RemoveMessageValidationCommand { get; set; }

        public MessageService MessageService { get; set; }

        private Server _selectedServer;
        public Server SelectedServer
        {
            get { return _selectedServer; }
            set { _selectedServer = value; }
        }

        private MessageValidation _selectedMessageValidation;
        public MessageValidation SelectedMessageValidation
        {
            get => _selectedMessageValidation;
            set => SetAndNotify(ref _selectedMessageValidation, value);
        }

        public void AddMessageValidation()
        {
            if (SelectedServer != null)
            {
                MessageValidation mv = new MessageValidation(SelectedServer);
                MessageService.MessageValidations.Add(mv);
            }
        }

        public void RemoveMessageValidation()
        {
            if (SelectedMessageValidation != null) { }
            MessageService.MessageValidations.Remove(SelectedMessageValidation);
        }
    }
}
