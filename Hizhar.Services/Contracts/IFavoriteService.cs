namespace Hizhar.Services.Contracts
{
    public interface IFavoriteService
    {
        Task<bool> ToggleAsync(string userId, int hutId);
    }
}
