using Library.Application.Helpers;
using Library.Application.Implementations;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Tests {
    [TestFixture]
    public class ImageServiceTests {
        private Mock<IOptions<ImageOptions>> _mockOptions;
        private IMemoryCache _cache;
        private ImageService _imageService;
        private ImageOptions _imageOptions;
        private string pathToTestImage = "U:\\std\\PreInternshipModsen\\Library\\Library.Application.Tests\\images\\testImage.png";
        [TearDown]
        public void TearDown() {
            _cache.Dispose();
        }
        [ SetUp]

        public void SetUp() {
            _imageOptions = new ImageOptions {
                Width = 100,
                Height = 100,
                PathToImages = "images",
                PathToDefauldImage = "default.png"
            };
            _cache = new MemoryCache( new MemoryCacheOptions() );
            _mockOptions = new Mock<IOptions<ImageOptions>>();
            _mockOptions.Setup( o => o.Value ).Returns( _imageOptions );

            _imageService = new ImageService( _mockOptions.Object, _cache );
        }
        [Test]
        public async Task GetImageAsBase64_ShouldReturnImageFromCache_WhenImageIsCached() {
            // Arrange
            var bookId = Guid.NewGuid();
            var path = Path.Combine( Directory.GetCurrentDirectory(), _imageOptions.PathToImages, bookId.ToString() + ".png" );
            var cachedImage = "cachedImageBase64";
            _cache.Set( bookId, cachedImage, TimeSpan.FromHours( 1 ) );

            // Act
            var result = await _imageService.GetImageAsBase64( bookId );

            // Assert
            Assert.AreEqual( cachedImage, result );
        }

        [Test]
        public async Task GetImageAsBase64_ShouldReturnImageFromFile_WhenImageIsNotCached() {
            // Arrange
            var bookId = Guid.NewGuid();
            var path = Path.Combine( Directory.GetCurrentDirectory(), _imageOptions.PathToImages, bookId.ToString() + ".png" );
            var imageBytes = new byte[] { 1, 2, 3 };
            var imageBase64 = Convert.ToBase64String( imageBytes );


           
            File.WriteAllBytes( path, imageBytes );

            // Act
            var result = await _imageService.GetImageAsBase64( bookId );

            // Assert
            Assert.AreEqual( imageBase64, result );
        }

        [Test]
        public async Task GetImageAsBase64_ShouldReturnDefaultImage_WhenImageFileDoesNotExist() {
            // Arrange
            var bookId = Guid.NewGuid();
            var path = Path.Combine( Directory.GetCurrentDirectory(), _imageOptions.PathToImages, bookId.ToString() + ".png" );
            var defaultPath = Path.Combine( Directory.GetCurrentDirectory(), _imageOptions.PathToDefauldImage );

            var defaultImageBytes = new byte[] { 4, 5, 6 };
            var defaultImageBase64 = Convert.ToBase64String( defaultImageBytes );

            File.WriteAllBytes( defaultPath, defaultImageBytes );

            // Act
            var result = await _imageService.GetImageAsBase64( bookId );

            // Assert
            Assert.AreEqual( defaultImageBase64, result );
        }
        [Test]
        public async Task SaveImage_ShouldSaveImageAndReturnPath() {
            // Arrange
            var bookId = Guid.NewGuid();
            var buff = await File.ReadAllBytesAsync( pathToTestImage );           

            var imageStream = new MemoryStream(buff);

            // Act
            var path = await _imageService.SaveImage( imageStream, bookId );

            // Assert
            Assert.IsTrue( File.Exists( path ) );
            Assert.That( path, Is.EqualTo( Path.Combine( Directory.GetCurrentDirectory(), _imageOptions.PathToImages, bookId.ToString() + ".png" ) ) );
        }

        [Test]
        public void DeleteImage_ShouldDeleteImage() {
            // Arrange
            var bookId = Guid.NewGuid();
            var path = Path.Combine( Directory.GetCurrentDirectory(), _imageOptions.PathToImages, bookId.ToString() + ".png" );
            File.Create( path ).Close();

            // Act
            _imageService.DeleteImage( bookId );

            // Assert
            Assert.IsFalse( File.Exists( path ) );
        }

        [Test]
        public async Task GetImage_ShouldReturnImageBytes_WhenImageExists() {
            // Arrange
            var bookId = Guid.NewGuid();
            var path = Path.Combine( Directory.GetCurrentDirectory(), _imageOptions.PathToImages, bookId.ToString() + ".png" );
            File.WriteAllBytes( path, new byte[] { 1, 2, 3 } );

            // Act
            var result = await _imageService.GetImage( bookId );

            // Assert
            Assert.IsNotNull( result );
            Assert.AreEqual( new byte[] { 1, 2, 3 }, result );
        }

        [Test]
        public async Task GetImage_ShouldReturnDefaultImageBytes_WhenImageDoesNotExist() {
            // Arrange
            var bookId = Guid.NewGuid();
            var defaultPath = Path.Combine( Directory.GetCurrentDirectory(), _imageOptions.PathToDefauldImage );
            File.WriteAllBytes( defaultPath, new byte[] { 4, 5, 6 } );

            // Act
            var result = await _imageService.GetImage( bookId );

            // Assert
            Assert.IsNotNull( result );
            Assert.AreEqual( new byte[] { 4, 5, 6 }, result );
        }
    }
}
