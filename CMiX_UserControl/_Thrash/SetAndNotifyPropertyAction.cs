﻿using System;
using CMiX.ViewModels;
using System.Linq.Expressions;
using System.Reflection;
using System.Collections.Generic;

namespace CMiX.Services
{
    /*public class SetAndNotifyPropertyAction<TRet> : AbstractAction
    {

        private ViewModel ParentObject { get; set; }
        private TRet Value { get; set; }
        private TRet OldValue { get; set; }

        private string PropertyName { get; }
        private MemberExpression BackingFieldExpression { get; }
        private Func<TRet> GetBackingField { get; }
        private Action<TRet> SetBackingField { get; }

        public SetAndNotifyPropertyAction(ViewModel parent, string propertyName, Expression<Func<TRet>> backingField, TRet newValue)
        {
            BackingFieldExpression = (MemberExpression)backingField.Body;
            var valueExpr = Expression.Parameter(typeof(TRet), "value");
            var assignExpr = Expression.Assign(BackingFieldExpression, valueExpr);

            SetBackingField = Expression.Lambda<Action<TRet>>(
                body: assignExpr,
                parameters: new[] { valueExpr })
                .Compile();

            GetBackingField = Expression.Lambda<Func<TRet>>(BackingFieldExpression)
                .Compile();

            PropertyName = propertyName;
            ParentObject = parent;
            Value = newValue;
        }

        protected override void ExecuteCore()
        {
            OldValue = GetBackingField();
            SetBackingField(Value);
            ParentObject.Notify(PropertyName);
        }

        protected override void UnExecuteCore()
        {
            SetBackingField(OldValue);
            ParentObject.Notify(PropertyName);
        }

        public override bool TryToMerge(IAction followingAction)
        {
            SetAndNotifyPropertyAction<TRet> next = followingAction as SetAndNotifyPropertyAction<TRet>;
            if (followingAction is SetAndNotifyPropertyAction<TRet>
                && next.ParentObject == ParentObject
                && next.BackingFieldExpression.ToString() == BackingFieldExpression.ToString()) //UGLY HACK WITH TOSTRING()
            {
                Value = next.Value;
                SetBackingField(Value);
                return true;
            }
            else
            {
                return false;
            }
        }
    }*/
}
