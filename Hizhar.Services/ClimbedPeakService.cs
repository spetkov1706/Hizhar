using Hizhar.Data;
using Hizhar.Data.Models;
using Hizhar.Services.Contracts;

namespace Hizhar.Services
{
    public class ClimbedPeakService : IClimbedPeakService
    {
        private readonly HizharDbContext context;

        public ClimbedPeakService(HizharDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> ToggleAsync(string userId, int peakId)
        {
            ClimbedPeak? climbed = await context.ClimbedPeaks.FindAsync(userId, peakId);

            if (climbed != null)
            {
                context.ClimbedPeaks.Remove(climbed);
                await context.SaveChangesAsync();
                return false;
            }

            await context.ClimbedPeaks.AddAsync(new ClimbedPeak
            {
                UserId = userId,
                PeakId = peakId,
                ClimbedOn = DateTime.Today
            });

            await context.SaveChangesAsync();
            return true;
        }
    }
}
