using FlickrNet;
using PhotosApi.Exceptions;
using PhotosApi.Helper;
using PhotosApi.Models;

namespace PhotosApi.Services.FlickrServices
{
    public class FlickerService : IFlickerService
    {
        private readonly ILogger<FlickerService> _logger;
        private readonly string? ApiKey;

        public FlickerService(ILogger<FlickerService> Logger) 
        {
            _logger = Logger;
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
        public async Task<IEnumerable<Photos>> GetPhotos(string Tag, int page = 1, int perPage = 20)
        {
            IList<Photos> listPhotos = new List<Photos>();
            try
            {
                if (string.IsNullOrWhiteSpace(ApiKey))
                {
                    throw new EmptyKeyException();
                }
                Flickr flickr = new(ApiKey);

                PhotoSearchOptions options = new PhotoSearchOptions
                {
                    Tags = Tag,
                    PerPage = 50, // number of photos per page (default is 100)
                    Extras = PhotoSearchExtras.AllUrls // retrieve all available photo URLs
                };
                DTOHelper helper = new();

                PhotoCollection photos = await flickr.PhotosSearchAsync(options);

                listPhotos = helper.Converter(photos);


                return listPhotos;
            }
            catch (Exception ex) 
            {
                
            }
            return listPhotos;
        }
 

    }
}
