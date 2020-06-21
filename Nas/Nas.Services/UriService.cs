using System.Threading.Tasks;
using FluentValidation;
using MongoDB.Driver;
using Nas.API.Utils;
using Nas.Data;
using Nas.Models;

namespace Nas.Services
{
    public class UriService : IUriService
    {
        public IMongoCollection<Uri> Uris { get; set; }

        public IValidator<CreateUriModel> CreateModelValidator { get; set; }

        public IValidator<RedirectUriModel> RedirectUriModelValidator { get; set; }

        public UriService(IDatabaseSettings settings, IValidator<CreateUriModel> createModelValidator,
            IValidator<RedirectUriModel> redirectUriModelValidator)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.Database);

            this.Uris = database.GetCollection<Uri>(settings.Collection);

            this.CreateModelValidator = createModelValidator;
            this.RedirectUriModelValidator = redirectUriModelValidator;
        }

        public async Task<bool> CreatedAsync(CreateUriModel model)
        {
            var validationResult = await this.CreateModelValidator.ValidateAsync(model);
            if (validationResult.IsValid == false)
            {
                return false;
            }

            if (await this.IsSlugFreeAsync(model.Slug) == false)
            {
                return false;
            }
            
            var uri = new Uri
            {
                Link = model.Link,
                Slug = model.Slug
            };
            await this.Uris.InsertOneAsync(uri).ConfigureAwait(true);

            return true;
        }

        public async Task<string> GetUriBySlugAsync(RedirectUriModel model)
        {
            var validationResult = await this.RedirectUriModelValidator.ValidateAsync(model);
            if (validationResult.IsValid == false)
            {
                return "Model not valid";
            }

            var uri = await this.Uris.Find(x => x.Slug == model.Slug).FirstOrDefaultAsync().ConfigureAwait(false);
            if (uri != null)
            {
                return uri.Link;
            }

            return "No shortened link with that slug";
        }

        private async Task<bool> IsSlugFreeAsync(string slug)
        {
            var uri = await this.Uris.Find(x => x.Slug == slug).FirstOrDefaultAsync().ConfigureAwait(false);
            if (uri != null)
            {
                return false;
            }

            return true;
        }
    }
}