using AVS.Contacts.Contracts.Services;
using MediatR;

namespace AVS.Contacts.Application.Commands;

public record SignInCommand : IRequest<AuthResult>;
public record SignOutCommand : IRequest;

public class SignInHandler : IRequestHandler<SignInCommand, AuthResult>
{
    private readonly IAuthService _authService;

    public SignInHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<AuthResult> Handle(SignInCommand request, CancellationToken ct)
    {
        return await _authService.SignInAsync(ct);
    }
}

public class SignOutHandler : IRequestHandler<SignOutCommand>
{
    private readonly IAuthService _authService;

    public SignOutHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task Handle(SignOutCommand request, CancellationToken ct)
    {
        await _authService.SignOutAsync(ct);
    }
}