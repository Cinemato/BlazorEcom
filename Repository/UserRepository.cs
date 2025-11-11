using BlazorEcom.Data;
using BlazorEcom.Repository.IRepository;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlazorEcom.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(AuthenticationStateProvider authStateProvider, UserManager<ApplicationUser> userManager)
        {
            _authStateProvider = authStateProvider;
            _userManager = userManager;
        }

        public async Task<ApplicationUser?> GetCurrentUserAsync()
        {
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            var principal = authState.User;

            if (principal?.Identity == null || !principal.Identity.IsAuthenticated)
                return null;

            var userId = GetUserId(principal);
            if (string.IsNullOrEmpty(userId))
                return null;

            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                user.Roles = roles.ToList();
            }

            return user;
        }

        public async Task<string?> GetCurrentUserIdAsync()
        {
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            return GetUserId(user);
        }

        private static string? GetUserId(ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? user.FindFirst("sub")?.Value;
        }
    }
}
