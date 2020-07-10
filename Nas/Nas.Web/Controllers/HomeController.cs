using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nas.Models;
using Nas.Services;

namespace Nas.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUriService _uriService;

        public HomeController(IUriService uriService)
        {
            this._uriService = uriService;
        }

        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create(CreateUriModel model)
        {
            if (model.Link == null || model.Slug == null)
            {
                return View("Error", "The input is empty!");
            }
            
            try
            {
                var opertaion = await this._uriService.CreatedAsync(model).ConfigureAwait(false);

                if (opertaion != "Successfully created.")
                    return View("Error", opertaion);
                
                return View("Index");
            }
            catch (Exception e)
            {
                return View("Error", e.Message);
            }
        }

        [Route("nv/{slug}")]
        public async Task<IActionResult> Navigate(string slug)
        {
            var operation = await this._uriService.GetUriBySlugAsync(
                    new RedirectUriModel
                    {
                        Slug = slug,
                    })
                .ConfigureAwait(false);

            Regex reg = new Regex(@"^http(s)?:\/\/([\w-]+.)+[\w-]+(\/[\w- .\/?%&=])?$");

            if (reg.IsMatch(operation))
            {
                return this.Redirect(operation);
            }

            return View("Error", operation);
        }

        [Route("/av")]
        public async Task<IActionResult> AvailableUris()
        {
            var operation = await this._uriService.GetAllCreatedShortLinks();
            return View(operation);
        }
    }
}