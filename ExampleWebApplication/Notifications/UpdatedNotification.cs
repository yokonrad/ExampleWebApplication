using ExampleWebApplication.Dtos;
using MediatR;

namespace ExampleWebApplication.Notifications
{
    public record UpdatedNotification(ExampleDto? ExampleDto) : INotification;

    public class FirstUpdatedNotificationHandler(ILogger<FirstUpdatedNotificationHandler> logger) : INotificationHandler<UpdatedNotification>
    {
        public async Task Handle(UpdatedNotification updatedNotification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling first notification with name: {Request}", typeof(UpdatedNotification).Name);
        }
    }

    public class SecondUpdatedNotificationHandler(ILogger<SecondUpdatedNotificationHandler> logger) : INotificationHandler<UpdatedNotification>
    {
        public async Task Handle(UpdatedNotification updatedNotification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling second notification with name: {Request}", typeof(UpdatedNotification).Name);
        }
    }
}