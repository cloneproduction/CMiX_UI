using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace CMiX.MVVM.ViewModels
{
    public abstract class ViewModel : INotifyPropertyChanged, ICloneable
    {
        public ViewModel()
        {
            Enabled = true;
        }

        private bool _enabled;
        public bool Enabled
        {
            get => _enabled;
            set => SetAndNotify(ref _enabled, value);
        }

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