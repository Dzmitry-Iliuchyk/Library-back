using Library.Application.Exceptions;
using Library.Application.Helpers;
using Library.Application.Interfaces.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.FileSystemGlobbing;
using SixLabors.ImageSharp.Processing;

namespace Library.Application.Implementations {
    public class ImageService: IImageService {
        private readonly ImageOptions _options;
        private readonly IMemoryCache _cache;
        public ImageService( IOptions<ImageOptions> imageOptions, IMemoryCache cache ) {
            _options = imageOptions.Value;
            _cache = cache;
        }
        public async Task<string> SaveImage( Stream imageStream, Guid bookId, string format = "png" ) {
            try {
                var path = GetImagePath( bookId, format );
                Directory.CreateDirectory( path: Path.GetDirectoryName( path ) );

                using var image = await SixLabors.ImageSharp.Image.LoadAsync( imageStream );

                this.DeleteImage(bookId);

                image.Mutate( x => x.Resize( _options.Width, _options.Height ) );
                using var outputStream = new FileStream( path, FileMode.Create );
                switch (format.ToLower()) {
                    case "png":
                        await image.SaveAsync( outputStream, new SixLabors.ImageSharp.Formats.Png.PngEncoder() );
                        break;
                    case "jpeg":
                    case "jpg":
                        await image.SaveAsync( outputStream, new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder() );
                        break;
                    default:
                        await image.SaveAsync( outputStream, new SixLabors.ImageSharp.Formats.Png.PngEncoder() );
                        break;
                }
                outputStream.Close();
                return path;
            }
            catch (Exception e) {
                throw new FileWriteException( "Failed to save image", e );
            }
        }
        public void DeleteImage( Guid bookId ) {
            _cache.Remove( bookId );
            Matcher matcher = new();
            var path = GetPathToImagesDirectory( );
            matcher.AddIncludePatterns(new[] { $"{bookId}.png",$"{bookId}.jpeg"} );
            foreach (var file in matcher.GetResultsInFullPath(path))
            {
                FileInfo fileInfo = new FileInfo( file );
                fileInfo.Delete();
            }
        }
        public async Task<byte[]> GetImage( Guid bookId ) {
            Matcher matcher = new();
            var path = GetPathToImagesDirectory();
            matcher.AddIncludePatterns( new[] { $"{bookId}.png", $"{bookId}.jpeg" } );
            var absPathToImage = matcher.GetResultsInFullPath( path );
           
            if (absPathToImage.Any()) {
                return await File.ReadAllBytesAsync( absPathToImage.First() );
            } else {
                path = GetDefaultImagePath();
                return await File.ReadAllBytesAsync( path );
            }
        }
        public async Task<string> GetImageAsBase64( Guid bookId ) {
            {
                if (_cache.TryGetValue( bookId, out string image )) {
                    return image;
                } else if (_cache.TryGetValue( "default", out string defaultImage )) {
                    return defaultImage;
                }
            }
            Matcher matcher = new();
            var path = GetPathToImagesDirectory();
            matcher.AddIncludePatterns( new[] { $"{bookId}.png", $"{bookId}.jpeg" } );
            var absPathToImage = matcher.GetResultsInFullPath( path );

            if (absPathToImage.Any()) {
                var image = Convert.ToBase64String( await File.ReadAllBytesAsync( absPathToImage.First()) );
                _cache.Set( bookId, image, TimeSpan.FromHours( 1 ) );
                return image;
            } else {
                path = GetDefaultImagePath();
                var defaultImage = Convert.ToBase64String( await File.ReadAllBytesAsync( path ) );
                _cache.Set( "default", defaultImage, TimeSpan.FromHours( 1 ) );
                return defaultImage;
            }
        }

        private string GetImagePath( Guid bookId, string format ) {
            return Path.Combine( Directory.GetCurrentDirectory(), _options.PathToImages, bookId.ToString() + $".{format}" );
        }
        private string GetPathToImagesDirectory() {
            return Path.Combine( Directory.GetCurrentDirectory(), _options.PathToImages);
        }
        private string GetDefaultImagePath() {
            return Path.Combine( Directory.GetCurrentDirectory(), _options.PathToDefauldImage );
        }
    }
}
