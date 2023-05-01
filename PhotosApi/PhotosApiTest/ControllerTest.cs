
namespace PhotosApiTest
{
    [TestClass]
    public class PhotosControllerTest
    {
        private Mock<IFlickerService> _mockFlickerService;
        private PhotosController _photosController;
        [TestInitialize]
        public void TestInitialize()
        {
            _mockFlickerService = new Mock<IFlickerService>();
            _photosController = new PhotosController(Mock.Of<ILogger<PhotosController>>(), _mockFlickerService.Object);

        }

        [TestMethod]
        public async Task GetPhotos_ReturnsSuccess()
        {
            _mockFlickerService
                .Setup(x => x.GetPhotos(It.IsAny<String>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(GetPhotos()));
            var result = await _photosController.GetPhotos("");
            Assert.AreEqual(HttpStatusCode.OK, GetHttpStatusCode(result));
        }

        [TestMethod]
        public async Task GetPhotos_ReturnsEmptyKeyException()
        {
            _mockFlickerService
                .Setup(x => x.GetPhotos(It.IsAny<String>(), It.IsAny<int>(), It.IsAny<int>()))
                .Throws(new EmptyKeyException());
            var result = await _photosController.GetPhotos("");
            Assert.AreEqual(HttpStatusCode.InternalServerError, GetHttpStatusCode(result));
        }

        [TestMethod]
        public async Task GetPhotos_ReturnsBadHttpRequestException()
        {
            _mockFlickerService
                .Setup(x => x.GetPhotos(It.IsAny<String>(), It.IsAny<int>(), It.IsAny<int>()))
                .Throws(new BadHttpRequestException(""));
            var result = await _photosController.GetPhotos("");
            Assert.AreEqual(HttpStatusCode.InternalServerError, GetHttpStatusCode(result));
        }

        private IEnumerable<Photos> GetPhotos() 
        {
            IList<Photos> Photos = new List<Photos>()
            {
                new Photos()
                {
                    OriginalUrl = "some original Url",
                    ThumbnailUrl = "some Thumbnail Url"
                }
            };
            return Photos;
        }

        private HttpStatusCode GetHttpStatusCode(IActionResult functionResult)
        {
            try
            {
                return (HttpStatusCode)(functionResult
                    .GetType()
                    .GetProperty("StatusCode")
                    ?.GetValue(functionResult, null) ?? "");
            }
            catch
            {
                return HttpStatusCode.InternalServerError;
            }
        }
    }
}