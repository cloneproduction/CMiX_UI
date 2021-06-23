using System;
using CMiX.Core.Models;
using CMiX.Core.Models.Scheduler;
using FluentScheduler;

namespace CMiX.Core.Presentation.ViewModels.Scheduler
{
    public class ToRunNow : ViewModel, IGetSetModel, IScheduleInterface<Schedule>
    {
        public ToRunNow()
        {
            Name = "ToRunNow";
            SetScheduler = new Action<Schedule>((s) => { SetToRunNow(s); });
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        public Action<Schedule> SetScheduler { get; set; }

        public void SetToRunNow(Schedule schedule)
        {
            schedule.ToRunNow();
        }

        public void SetViewModel(IModel model)
        {
            ToRunNowModel toRunNowModel = model as ToRunNowModel;
            this.Name = toRunNowModel.Name;
        }

        public IModel GetModel()
        {
            ToRunNowModel toRunNowModel = new ToRunNowModel();
            toRunNowModel.Name = this.Name;
            return toRunNowModel;
        }
    }
}
