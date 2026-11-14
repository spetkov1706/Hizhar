using Hizhar.Web.ViewModels.Map;

namespace Hizhar.Services.Contracts
{
    public interface IMapService
    {
        Task<MapDataViewModel> GetMapDataAsync();
    }
}
