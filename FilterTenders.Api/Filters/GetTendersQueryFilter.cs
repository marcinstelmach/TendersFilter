using FilterTenders.Application;

namespace FilterTenders.Api.Filters;

public class GetTendersQueryFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var query = context.GetArgument<GetTendersQuery>(0);
        var validationError = query.Validate();
        if (!string.IsNullOrWhiteSpace(validationError))
        {
            return Results.Problem(validationError, statusCode: 400);
        }

        return await next(context);
    }
}