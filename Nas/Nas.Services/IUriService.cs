using System.Threading.Tasks;
using Nas.Data;
using Nas.Models;

namespace Nas.Services
{
    public interface IUriService
    {
        Task<bool> CreatedAsync(CreateUriModel model);
        Task<string> GetUriBySlugAsync(RedirectUriModel model);
    }
}