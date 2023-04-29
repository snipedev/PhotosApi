using Microsoft.AspNetCore.Mvc;
using PhotosApi.Exceptions;
using PhotosApi.Services.FlickrServices;

namespace PhotosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {

        private readonly ILogger<PhotosController> _logger;
        private readonly IFlickerService _flickerService;
        public PhotosController(ILogger<PhotosController> logger,IFlickerService flickerService)
        {
            _logger = logger;
            _flickerService = flickerService;
        }

        [HttpGet("{Tag}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPhotos(string Tag)
        {
            try
            {
                var photos = await _flickerService.GetPhotos(Tag);
                return Ok(photos);
            }
            catch (EmptyKeyException ex)
            {
                _logger.LogError(ex, $"API key is empty");
                return StatusCode(500);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, $"Returned exception of type {ex.GetType()} while getting all photos for the tag {Tag}");
                return StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Returned exception of type {ex.GetType()} while getting all photos for the tag {Tag}");
                return StatusCode(500);
            }
        }
    }
}
