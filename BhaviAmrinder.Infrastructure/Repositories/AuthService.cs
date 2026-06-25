
using Microsoft.AspNetCore.Identity;
using BhaviAmrinder.Domain.Interfaces;
using BhaviAmrinder.Infrastructure.Identity;
using BhaviAmrinder.Application.IServices;

namespace BhaviAmrinder.Infrastructure.Repositories;
public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenService _tokenService;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task<string?> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
            return null;

        var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

        if (!result.Succeeded)
            return null;

        var roles = await _userManager.GetRolesAsync(user);
        var role = roles.FirstOrDefault();

        var token = _tokenService.CreateToken(user.Id.ToString(), user.Email, role);

        return token;
    }
}