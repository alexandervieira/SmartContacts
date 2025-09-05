using AVS.Contacts.Contracts.Services;
using MediatR;

namespace AVS.Contacts.Application.Behaviors;

public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IAuthService _authService;

    public AuthorizationBehavior(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
    {
        if (!await _authService.IsAuthenticatedAsync(ct))
            throw new UnauthorizedAccessException("Usuário não autenticado");

        return await next();
    }
}