using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCQRSApplication.Notify
{
    public class PublishTeamNoify : INotification
    {
        public string Message { get; set; }
    }

    public class PublishTeamMessageHandler : INotificationHandler<PublishTeamNoify>
    {
        public Task Handle(PublishTeamNoify notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"PublishTeamMessageHandler: {notification.Message}");

            return Task.CompletedTask;
        }
    }

    public class PublishTeamTextHandler : INotificationHandler<PublishTeamNoify>
    {
        public Task Handle(PublishTeamNoify notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"PublishTeamTextHandler: {notification.Message}");

            return Task.CompletedTask;
        }
    }
}
