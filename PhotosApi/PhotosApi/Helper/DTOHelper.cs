using FlickrNet;
using PhotosApi.Models;

namespace PhotosApi.Helper
{
    public class DTOHelper
    {

        public IList<Photos> Converter(PhotoCollection photoCollection)
        {
            IList<Photos> photos = new List<Photos>();

            foreach (var photo in photoCollection)
            {
                var image = new Photos()
                {
                    OriginalUrl = photo.OriginalUrl,
                    LargeUrl = photo.LargeUrl,
                    ThumbnailUrl = photo.ThumbnailUrl,
                    Title = photo.Title,
                    PhotoId = photo.PhotoId
                };
                photos.Add(image);
            }

            return photos;
        }
    }
}
