// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using WatsonTcp;

namespace CMiX.Core.Presentation.ViewModels.Network
{
    public class ServerStatistics : ViewModel
    {
        public ServerStatistics()
        {
            SentMessages = 0;
        }



        private long _sentMessages;
        public long SentMessages
        {
            get => _sentMessages;
            set => SetAndNotify(ref _sentMessages, value);
        }

        private long _sentBytes;
        public long SentBytes
        {
            get => _sentBytes;
            set => SetAndNotify(ref _sentBytes, value);
        }

        private DateTime _startTime;
        public DateTime StartTime
        {
            get => _startTime;
            set => SetAndNotify(ref _startTime, value);
        }

        private TimeSpan _upTime;
        public TimeSpan UpTime
        {
            get => _upTime;
            set => SetAndNotify(ref _upTime, value);
        }

        private decimal _sentMessagesAverageSize;
        public decimal SentMessagesAverageSize
        {
            get => _sentMessagesAverageSize;
            set => SetAndNotify(ref _sentMessagesAverageSize, value);
        }

        public void Update(WatsonTcpServer watsonTcpServer)
        {
            SentMessages = watsonTcpServer.Statistics.SentMessages;
            SentMessagesAverageSize = watsonTcpServer.Statistics.SentMessageSizeAverage;
            StartTime = watsonTcpServer.Statistics.StartTime;
            UpTime = watsonTcpServer.Statistics.UpTime;
        }
    }
}
