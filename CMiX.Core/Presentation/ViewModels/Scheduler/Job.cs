// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using CMiX.Core.Models;
using FluentScheduler;

namespace CMiX.Core.Presentation.ViewModels.Scheduler
{
    public abstract class Job : ViewModel, IGetSetModel, IJob
    {
        public Action<Schedule> Action { get; set; }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        private DateTime _nextRun;
        public DateTime NextRun
        {
            get => _nextRun;
            set => SetAndNotify(ref _nextRun, value);
        }

        private bool _disabled;
        public bool Disabled
        {
            get => _disabled;
            set => SetAndNotify(ref _disabled, value);
        }

        public abstract void Execute();
        public abstract void SetViewModel(IModel model);
        public abstract IModel GetModel();
    }
}
