using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using CMiX.MVVM.Commands;

using Memento;

namespace CMiX.MVVM.ViewModels
{
    public class ViewModel : INotifyPropertyChanged, ICloneable
    {
        #region CONSTRUCTORS
        public ViewModel()
        {

        }

        public ViewModel( ObservableCollection<ServerValidation> servervalidation, Mementor mementor)
        {
            ServerValidation = servervalidation;
            Mementor = mementor;
        }
        #endregion

        #region PROPERTIES
        public Mementor Mementor { get; set; }

        public string MessageAddress { get; set; }

        public ObservableCollection<ServerValidation> ServerValidation { get; set; }
        #endregion

        #region MESSENGERS
        public void SendMessages(string topic, MessageCommand command, object parameter,  object payload)
        {
            if (ServerValidation != null)
            {
                foreach (var servervalidation in ServerValidation)
                {
                    if (servervalidation.SendEnabled && servervalidation.Server.Enabled)
                    {
                        servervalidation.Server.Send(topic, command, parameter, payload);
                        Console.WriteLine("ServerValidation Sent with topic " + topic);
                    }
                }
            }
        }

        public void SendMessagesWithoutValidation(string topic, MessageCommand command, object parameter, object payload)
        {
            if (ServerValidation != null)
            {
                foreach (var servervalidation in ServerValidation)
                {
                    if (servervalidation.Server.Enabled)
                    {
                        servervalidation.Server.Send(topic, command, parameter, payload);
                    }
                }
            }
        }

        public void DisabledMessages()
        {
            if(ServerValidation != null)
            {
                foreach (var serverValidation in ServerValidation)
                {
                    serverValidation.Server.Enabled = false;
                }
            }
        }

        public void EnabledMessages()
        {
            if (ServerValidation != null)
            {
                foreach (var serverValidation in ServerValidation)
                {
                    serverValidation.Server.Enabled = true;
                }
            }
        }

        //public void QueueMessages(string address, params object[] args)
        //{
        //    if (OSCValidation != null)
        //    {
        //        foreach (var oscvalidation in OSCValidation)
        //        {
        //            if (oscvalidation.SendEnabled && oscvalidation.OSCMessenger.Enabled)
        //            {
        //                oscvalidation.OSCMessenger.QueueMessage(address, args);
        //            }

        //        }
        //    }
        //}

        //public void QueueObjects(object obj)
        //{
        //    if (OSCValidation != null)
        //    {
        //        foreach (var oscvalidation in OSCValidation)
        //        {
        //            if (oscvalidation.SendEnabled && oscvalidation.OSCMessenger.Enabled)
        //            {
        //                oscvalidation.OSCMessenger.QueueObject(obj);
        //            }
        //        }
        //    }
        //}

        //public void SendQueues()
        //{
        //    if (OSCValidation != null)
        //    {
        //        foreach (var oscvalidation in OSCValidation)
        //        {
        //            if (oscvalidation.SendEnabled && oscvalidation.OSCMessenger.Enabled)
        //            {
        //                oscvalidation.OSCMessenger.SendQueue();
        //            }
        //        }
        //    }
        //}
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