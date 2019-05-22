using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using CMiX.Services;
using System.Collections.ObjectModel;
using Memento;
using CMiX.Models;

namespace CMiX.ViewModels
{
    public class ViewModel : INotifyPropertyChanged, ICloneable
    {
        #region CONSTRUCTORS
        public ViewModel()
        {

        }

        public ViewModel(ObservableCollection<OSCMessenger> oscmessengers)
        {
            Messengers = oscmessengers ?? throw new ArgumentNullException(nameof(oscmessengers));
        }

        public ViewModel(ObservableCollection<OSCMessenger> oscmessengers, Mementor mementor)
        {
            Messengers = oscmessengers ?? throw new ArgumentNullException(nameof(oscmessengers));
            Mementor = mementor;
        }
        #endregion

        #region PROPERTIES
        public Mementor Mementor { get; set; }

        public ObservableCollection<OSCMessenger> Messengers { get; set; }

        public string MessageAddress { get; set; }
        #endregion

        #region MESSENGERS
        public void SendMessages(string address, params object[] args)
        {
            if (Messengers != null)
            {
                foreach (var Message in Messengers)
                {
                    if (Message.SendEnabled)
                    {
                        Message.SendMessage(address, args);
                    }
                }
            }
        }

        public void QueueMessages(string address, params object[] args)
        {
            foreach (var Message in Messengers)
            {
                Message.QueueMessage(address, args);
            }
        }

        public void QueueObjects(object obj)
        {
            foreach (var Message in Messengers)
            {
                Message.QueueObject(obj);
            }
        }

        public void SendQueues()
        {
            foreach (var Message in Messengers)
            {
                Message.SendQueue();
            }
        }

        public void DisabledMessages()
        {
            if(Messengers != null)
            {
                foreach (var Message in Messengers)
                {
                    Message.SendEnabled = false;
                }
            }
        }

        public void EnabledMessages()
        {
            if (Messengers != null)
            {
                foreach (var Message in Messengers)
                {
                    Message.SendEnabled = true;
                }
            }
        }
        #endregion

        #region INOTIFYPROPERTYCHANGED
        public event PropertyChangedEventHandler PropertyChanged;

        public void Notify([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SetAndNotify<TRet>(ref TRet backingField, TRet newValue, [CallerMemberName] string propertyName = null)
        {
            if (Equals(backingField, newValue))
            {
                return;
            }

            backingField = newValue;
            Notify(propertyName);
        }

        /*public void SetAndRecord<TRet>(Expression<Func<TRet>> backingField, TRet newValue, [CallerMemberName] string propertyName = null)
        {
            var action = new SetAndNotifyPropertyAction<TRet>(this, propertyName, backingField, newValue);
        }*/

        public static void AssertNotNegative(double value, string parameterName)
        {
            if (value < 0)
            {
                throw new ArgumentException("Value must not be negative.", parameterName);
            }
        }

        public static void AssertNotNegative(Expression<Func<double>> parameterExpression)
        {
            double value = parameterExpression.Compile().Invoke();

            if (value < 0)
            {
                throw new ArgumentException("Value must not be negative.", ((MemberExpression)parameterExpression.Body).Member.Name);
            }
        }

        public static double CoerceNotNegative(double value)
        {
            return value < 0 ? 0 : value;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion
    }
}