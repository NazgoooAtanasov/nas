using System.Collections.Generic;
using System.Threading.Tasks;
using Nas.Data;
using Nas.Models;

namespace Nas.Services
{
    public interface IUriService
    {
        Task<string> CreatedAsync(CreateUriModel model);
        Task<string> GetUriBySlugAsync(RedirectUriModel model);
        Task<List<Uri>> GetAllCreatedShortLinks();
    }
}