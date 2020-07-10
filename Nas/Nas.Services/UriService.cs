using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Nas.Data;
using Nas.Models;

namespace Nas.Services
{
    public class UriService : IUriService
    {
        private NasDbContext Context { get; set; }
        private IValidator<CreateUriModel> CreateModelValidator { get; set; }

        private IValidator<RedirectUriModel> RedirectUriModelValidator { get; set; }

        public UriService(NasDbContext context ,IValidator<CreateUriModel> createModelValidator,
            IValidator<RedirectUriModel> redirectUriModelValidator)
        {
            this.Context = context;
            this.CreateModelValidator = createModelValidator;
            this.RedirectUriModelValidator = redirectUriModelValidator;
        }

        public async Task<string> CreatedAsync(CreateUriModel model)
        {
            var validationResult = await this.CreateModelValidator.ValidateAsync(model);
            if (validationResult.IsValid == false)
            {
                return string.Join("\n", validationResult.Errors);
            }

            if (await this.IsSlugFreeAsync(model.Slug) == false)
            {
                return $"{model.Slug} is already in use.";
            }
            
            var uri = new Uri
            {
                Link = model.Link,
                Slug = model.Slug
            };
            await this.Context.Uris.AddAsync(uri);
            await this.Context.SaveChangesAsync();
            return "Successfully created.";
        }

        public async Task<string> GetUriBySlugAsync(RedirectUriModel model)
        {
            var validationResult = await this.RedirectUriModelValidator.ValidateAsync(model);
            if (validationResult.IsValid == false)
            {
                return "Model not valid";
            }

            var uri = await this.Context.Uris.FirstOrDefaultAsync(x => x.Slug == model.Slug).ConfigureAwait(false);
            if (uri != null)
            {
                return uri.Link;
            }

            return "No shortened link with that slug";
        }

        public async Task<List<Uri>> GetAllCreatedShortLinks()
        {
            var links = await this.Context.Uris.ToListAsync().ConfigureAwait(false);
            return links;
        }

        private async Task<bool> IsSlugFreeAsync(string slug)
        {
            var uri = await this.Context.Uris.FirstOrDefaultAsync(x => x.Slug == slug).ConfigureAwait(false);
            if (uri != null)
            {
                return false;
            }

            return true;
        }
    }
}