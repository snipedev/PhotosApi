using FlickrNet;
using PhotosApi.Exceptions;
using PhotosApi.Helper;
using PhotosApi.Models;

namespace PhotosApi.Services.FlickrServices
{
    public class FlickerService : IFlickerService
    {
        private readonly string? ApiKey;

        public FlickerService()
        {
            ApiKey = Environment.GetEnvironmentVariable("API_KEY");
        }

        /// <summary>
        /// returns photos
        /// </summary>
        /// <param name="Tag"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        /// <exception cref="EmptyKeyException"></exception>
        public async Task<IEnumerable<Photos>> GetPhotos(string Tag, int page, int perPage)
        {
            IList<Photos> listPhotos = new List<Photos>();
            if (string.IsNullOrWhiteSpace(ApiKey))
            {
                throw new EmptyKeyException();
            }
            Flickr flickr = new(ApiKey);

            PhotoSearchOptions options = new PhotoSearchOptions
            {
                Tags = Tag,
                PerPage = perPage,
                Page = page,
                Extras = PhotoSearchExtras.AllUrls // retrieve all available photo URLs
            };
            DTOHelper helper = new();

            PhotoCollection photos = await flickr.PhotosSearchAsync(options);

            listPhotos = helper.Converter(photos);
            return listPhotos;
        }


    }
}
