namespace Hizhar.Services.Contracts
{
    public interface IClimbedPeakService
    {
        Task<bool> ToggleAsync(string userId, int peakId);
    }
}
