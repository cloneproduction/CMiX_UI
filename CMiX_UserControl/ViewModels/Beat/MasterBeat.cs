﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using CMiX.Services;
using CMiX.Models;
using GuiLabs.Undo;

namespace CMiX.ViewModels
{
    public class MasterBeat : Beat
    {
        #region CONSTRUCTORS
        public MasterBeat(OSCMessenger messenger, ActionManager actionmanager)
        : this
        (
            actionmanager: actionmanager,
            messenger: messenger,
            period: 0.0,
            multiplier: 1
        )
        { }

        public MasterBeat
            (
                ActionManager actionmanager,
                OSCMessenger messenger,
                double period,
                int multiplier
            )
            : base(actionmanager, messenger)
        {
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            Period = period;
            Multiplier = multiplier;
            ResyncCommand = new RelayCommand(p => Resync());
            TapCommand = new RelayCommand(p => Tap());
            MessageAddress = "/MasterBeat/";
            tapPeriods = new List<double>();
            tapTime = new List<double>();
        }
        #endregion

        #region PROPERTIES
        private double _period;
        [OSC]
        public override double Period
        {
            get => _period;
            set
            {
                SetAndNotify(ref _period, value);
                OnPeriodChanged(Period);
                Notify(nameof(BPM));
                Messenger.SendMessage(MessageAddress + nameof(Period), Period);
            }
        }

        public ICommand ResyncCommand { get; }
        public ICommand TapCommand { get; }


        private readonly List<double> tapPeriods;
        private readonly List<double> tapTime;
        #endregion

        private void Resync()
        {
            Messenger.SendMessage(MessageAddress + nameof(Resync), CurrentTime + Period);
        }

        protected override void Multiply()
        {
            Period /= 2.0;
        }

        protected override void Divide()
        {
            Period *= 2.0;
        }

        private void Tap()
        {
            Period = GetMasterPeriod();
        }

        private double GetMasterPeriod()
        {
            double ms = CurrentTime;

            if (tapTime.Count > 1 && ms - tapTime[tapTime.Count - 1] > 5000)
                tapTime.Clear();

            tapTime.Add(ms);

            if (tapTime.Count > 1)
            {
                tapPeriods.Clear();
                for (int i = 1; i < tapTime.Count; i++)
                {
                    double average = tapTime[i] - tapTime[i - 1];
                    tapPeriods.Add(average);
                }
            }
            return tapPeriods.Sum() / tapPeriods.Count;
        }

        private double CurrentTime => (DateTime.Now - DateTime.MinValue).TotalMilliseconds;


        public void Copy(MasterBeatDTO masterbeatdto)
        {
            masterbeatdto.Period = Period;
            masterbeatdto.MessageAddress = MessageAddress;
        }

        public void Paste(MasterBeatDTO masterbeatdto)
        {
            Messenger.SendEnabled = false;
            MessageAddress = masterbeatdto.MessageAddress;
            Period = masterbeatdto.Period;
            Messenger.SendEnabled = true;
        }
    }
}