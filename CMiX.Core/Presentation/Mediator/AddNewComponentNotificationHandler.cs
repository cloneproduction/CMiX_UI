// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System.Threading;
using System.Threading.Tasks;
using CMiX.Core.Network.Messages;
using CMiX.Core.Presentation.ViewModels.Network;
using MediatR;

namespace CMiX.Core.Presentation.Mediator
{
    public class AddNewComponentNotificationHandler : INotificationHandler<AddNewComponentNotification>
    {
        public AddNewComponentNotificationHandler(IDataSender dataSender)
        {
            _dataSender = dataSender;
        }

        private readonly IDataSender _dataSender;

        public Task Handle(AddNewComponentNotification notification, CancellationToken cancellationToken)
        {
            _dataSender.SendMessage(new MessageAddComponent(notification.Component));
            return Task.CompletedTask;
        }
    }
}
