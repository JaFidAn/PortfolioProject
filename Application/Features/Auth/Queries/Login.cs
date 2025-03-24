using Application.Core;
using Application.Features.Auth.DTOs;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Application.Services;
using Domain.Entities;

namespace Application.Features.Auth.Queries;

public class Login
{
    public class Query : IRequest<Result<AuthResultDto>>
    {
        public LoginDto LoginDto { get; set; } = null!;
    }

    public class Handler : IRequestHandler<Query, Result<AuthResultDto>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtService _jwtService;

        public Handler(
            UserManager<AppUser> userManager,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<Result<AuthResultDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.Email == request.LoginDto.Email, cancellationToken);

            if (user is null)
            {
                return Result<AuthResultDto>.Failure("Invalid email or password", 401);
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, request.LoginDto.Password);

            if (!passwordValid)
            {
                return Result<AuthResultDto>.Failure("Invalid email or password", 401);
            }

            var token = await _jwtService.GenerateToken(user);

            return Result<AuthResultDto>.Success(new AuthResultDto { Token = token }, "Login successful");
        }
    }
}
