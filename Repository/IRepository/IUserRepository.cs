using BlazorEcom.Data;

namespace BlazorEcom.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<ApplicationUser?> GetCurrentUserAsync();
        Task<string?> GetCurrentUserIdAsync();
    }
}
