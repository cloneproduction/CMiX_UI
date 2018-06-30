using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace CMiX.ViewModels
{
    [Serializable]
    public class ViewModel : INotifyPropertyChanged, ICloneable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void Notify([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SetAndNotify<TRet>(ref TRet backingField, TRet newValue, [CallerMemberName] string propertyName = null)
        {
            backingField = newValue;
            Notify(propertyName);
        }

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
    }
}