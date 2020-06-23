using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nas.Models;
using Nas.Services;

namespace Nas.API.Controllers
{
    public class ShortenerController : BaseController
    {
        public IUriService UriService { get; set; }

        public ShortenerController(IUriService uriService)
        {
            this.UriService = uriService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetShortUris()
        {
            var links = await this.UriService.GetAllCreatedShortLinks().ConfigureAwait(false);

            if (links != null)
                return this.Ok(links);
            return this.BadRequest("No links");
        }
        

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(CreateUriModel model)
        {
            if (model != null)
            {
                var operation = await this.UriService.CreatedAsync(model).ConfigureAwait(false);
                if (operation)
                {
                    return this.Ok("created");
                }
                else
                {
                    return this.BadRequest("not created");
                }
            }

            return this.BadRequest();
        }

        [HttpGet("[action]/{slug}")]
        public async Task<IActionResult> Navigate(string slug)
        {
            if (slug != null)
            {
                var redirectUriModel = new RedirectUriModel
                {
                    Slug = slug
                };
                var operation = await this.UriService.GetUriBySlugAsync(redirectUriModel).ConfigureAwait(false);
                return this.Ok(operation);
            }

            return this.BadRequest();
        }
    }
}