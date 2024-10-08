﻿using Auth.Application.Common.Constants;
using Auth.Application.Common.Interfaces;
using Auth.Application.Common.Models.Token;
using Auth.Application.Common.Models.User;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Auth.Application.Users.Commands.SignUp;

public record SignUpCommand : IRequest<TokenDto>
{
    public required string Identifier { get; init; }
    public required string Password { get; init; }
}

public sealed class SignUpHandler(
    IAuthService authService,
    IMapper mapper,
    ILogger<SignUpHandler> logger)
    : IRequestHandler<SignUpCommand, TokenDto>
{
    private readonly IAuthService _authService = authService;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<SignUpHandler> _logger = logger;

    public async Task<TokenDto> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var signUpDto = _mapper.Map<SignUpDto>(request);

        TokenDto token = await _authService.SignUpUser(signUpDto, cancellationToken);

        _logger.LogInformation(LoggingTemplates.UserSignedUp, request.Identifier);

        return token;
    }
}
