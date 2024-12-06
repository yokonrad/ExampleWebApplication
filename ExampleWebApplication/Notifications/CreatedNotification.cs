using ExampleWebApplication.Dtos;
using MediatR;

namespace ExampleWebApplication.Notifications
{
    public record CreatedNotification(ExampleDto? ExampleDto) : INotification;

    public class FirstCreatedNotificationHandler(ILogger<FirstCreatedNotificationHandler> logger) : INotificationHandler<CreatedNotification>
    {
        public async Task Handle(CreatedNotification createdNotification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling first notification with name: {Request}", typeof(CreatedNotification).Name);
        }
    }

    public class SecondCreatedNotificationHandler(ILogger<SecondCreatedNotificationHandler> logger) : INotificationHandler<CreatedNotification>
    {
        public async Task Handle(CreatedNotification createdNotification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling second notification with name: {Request}", typeof(CreatedNotification).Name);
        }
    }
}