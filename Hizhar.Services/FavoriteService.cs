using Hizhar.Data;
using Hizhar.Data.Models;
using Hizhar.Services.Contracts;

namespace Hizhar.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly HizharDbContext context;

        public FavoriteService(HizharDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> ToggleAsync(string userId, int hutId)
        {
            FavoriteHut? favorite = await context.FavoriteHuts.FindAsync(userId, hutId);

            if (favorite != null)
            {
                context.FavoriteHuts.Remove(favorite);
                await context.SaveChangesAsync();
                return false;
            }

            await context.FavoriteHuts.AddAsync(new FavoriteHut
            {
                UserId = userId,
                HutId = hutId,
                AddedOn = DateTime.UtcNow
            });

            await context.SaveChangesAsync();
            return true;
        }
    }
}
