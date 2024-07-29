using FilterTenders.Application;
using FilterTenders.Application.Dtos;

namespace FilterTenders.Api.Filters;

public class GetTendersRequestFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var query = context.GetArgument<GetTendersRequest>(0);
        var validationError = query.Validate();
        if (!string.IsNullOrWhiteSpace(validationError))
        {
            return Results.Problem(validationError, statusCode: 400);
        }

        return await next(context);
    }
}