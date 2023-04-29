using PhotosApi.Models;

namespace PhotosApi.Services.FlickrServices
{
    public interface IFlickerService
    {
        public Task<IEnumerable<Photos>> GetPhotos(string Tag,int page = 1,int perPage = 20);
    }
}
