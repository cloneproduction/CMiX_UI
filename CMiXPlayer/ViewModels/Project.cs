﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;
using Ceras;
using CMiX.MVVM.ViewModels;
using Memento;
using Quartz;
using Quartz.Impl;

namespace CMiXPlayer.ViewModels
{
    public class Project : ViewModel
    {
        #region CONSTRUCTORS
        public Project()
        {
            Clients = new ObservableCollection<Client>();
            Serializer = new CerasSerializer();
            CompoSelector = new FileSelector(string.Empty, "Single", new List<string>() { ".COMPMIX" }, new ObservableCollection<OSCValidation>(), new Mementor());

            AddClientCommand = new RelayCommand(p => AddClient());
            DeleteClientCommand = new RelayCommand(p => DeleteClient(p));
            SendAllCommand = new RelayCommand(p => SendAllClient());
            ResetAllClientCommand = new RelayCommand(p => ResetAllClient());
            MakeJobCommand = new RelayCommand(p => MakeJob());
        }
        #endregion

        #region PROPERTIES
        public ICommand AddClientCommand { get; }
        public ICommand DeleteClientCommand { get; }
        public ICommand SendAllCommand { get; }
        public ICommand ResetAllClientCommand { get; }
        public ICommand MakeJobCommand { get; }

        public CerasSerializer Serializer { get; set; }
        public ObservableCollection<Client> Clients { get; set; }
        public FileSelector CompoSelector { get; set; }
        #endregion

        public async void MakeJob()
        {
            // construct a scheduler factory
            NameValueCollection props = new NameValueCollection{{ "quartz.serializer.type", "binary" }};
            StdSchedulerFactory factory = new StdSchedulerFactory(props);

            // get a scheduler
            IScheduler sched = await factory.GetScheduler();
            await sched.Start();

            JobDataMap jobdatamap = new JobDataMap();
            jobdatamap.Add("CompoSelector", Clients[0].CompoSelector);
            jobdatamap.Add("OSCMessenger", Clients[0].OSCMessenger);
            jobdatamap.Add("Serializer", Clients[0].Serializer);

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<Jobs.JobSendComposition>()
                .SetJobData(jobdatamap)
                .WithIdentity("myJob", "group1")
                .Build();

            // Trigger the job to run now, and then every 40 seconds
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("myTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)
                    .WithRepeatCount(2))
            .Build();

            await sched.ScheduleJob(job, trigger);
        }

        #region METHODS
        private void AddClient()
        {
            var client = new Client(Serializer) { Name = "pouet" };
            Clients.Add(client);
        }

        private void DeleteClient(object client)
        {
            Clients.Remove(client as Client);
        }

        private void SendAllClient()
        {
            foreach (var client in Clients)
            {
                client.SendComposition();
            }
        }

        private void ResetAllClient()
        {
            foreach(var client in Clients)
            {
                client.ResetClient();
            }
        }
        #endregion
    }
}
