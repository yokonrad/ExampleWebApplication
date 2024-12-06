using ExampleWebApplication.Dtos;
using MediatR;

namespace ExampleWebApplication.Notifications
{
    public record DeletedNotification(ExampleDto? ExampleDto) : INotification;

    public class FirstDeletedNotificationHandler(ILogger<FirstDeletedNotificationHandler> logger) : INotificationHandler<DeletedNotification>
    {
        public async Task Handle(DeletedNotification deletedNotification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling first notification with name: {Request}", typeof(DeletedNotification).Name);
        }
    }

    public class SecondDeletedNotificationHandler(ILogger<SecondDeletedNotificationHandler> logger) : INotificationHandler<DeletedNotification>
    {
        public async Task Handle(DeletedNotification deletedNotification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling second notification with name: {Request}", typeof(DeletedNotification).Name);
        }
    }
}