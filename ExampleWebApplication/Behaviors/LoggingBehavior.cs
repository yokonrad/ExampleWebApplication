using MediatR;

namespace ExampleWebApplication.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling request with name: {Request}", typeof(TRequest).Name);

        var response = await next();

        logger.LogInformation("Handled request with name: {Request} with response: {Response}", typeof(TRequest).Name, typeof(TResponse).Name);

        return response;
    }
}