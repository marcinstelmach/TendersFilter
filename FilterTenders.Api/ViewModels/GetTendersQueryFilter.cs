using FilterTenders.Application;

namespace FilterTenders.Api.ViewModels;

public class GetTendersQueryFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var request = context.GetArgument<GetTendersQuery>(0);

        if (request.OrderType is not null && request.OrderBy is null)
        {
            return Results.Problem("OrderType parameter require OrderBy parameter to be present", statusCode: 400);
        }

        return await next(context);
    }
}