using Application.Core;
using Application.Features.Auth.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Application.Services;

namespace Application.Features.Auth.Commands;

public class Register
{
    public class Command : IRequest<Result<AuthResultDto>>
    {
        public RegisterDto RegisterDto { get; set; } = null!;
    }

    public class Handler : IRequestHandler<Command, Result<AuthResultDto>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;

        public Handler(
            UserManager<AppUser> userManager,
            IMapper mapper,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        public async Task<Result<AuthResultDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var existingEmail = await _userManager.FindByEmailAsync(request.RegisterDto.Email);
            if (existingEmail is not null)
            {
                return Result<AuthResultDto>.Failure("Email is already taken", 400);
            }

            var existingUserName = await _userManager.FindByNameAsync(request.RegisterDto.UserName);
            if (existingUserName is not null)
            {
                return Result<AuthResultDto>.Failure("Username is already taken", 400);
            }

            var user = _mapper.Map<AppUser>(request.RegisterDto);
            user.UserName = request.RegisterDto.UserName;
            user.Email = request.RegisterDto.Email;

            var result = await _userManager.CreateAsync(user, request.RegisterDto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return Result<AuthResultDto>.Failure($"User registration failed: {errors}", 400);
            }

            await _userManager.AddToRoleAsync(user, "User");

            var token = await _jwtService.GenerateToken(user);

            return Result<AuthResultDto>.Success(new AuthResultDto { Token = token }, "Registration successful");
        }
    }
}
