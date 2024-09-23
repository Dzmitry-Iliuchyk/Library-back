using Library.Application.Exceptions;
using Library.Application.Helpers;
using Library.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp.Processing;

namespace Library.Application.Implementations {
    public class ImageService: IImageService {
        private readonly ImageOptions _options;
        private readonly IMemoryCache _cache;
        public ImageService( IOptions<ImageOptions> imageOptions, IMemoryCache cache ) {
            _options = imageOptions.Value;
            _cache = cache;
        }
        public async Task<string> SaveImage( Stream imageStream, Guid bookId ) {
            try {
                var path = GetImagePath( bookId );
                Directory.CreateDirectory( path: Path.GetDirectoryName( path ) );

                using var image = await SixLabors.ImageSharp.Image.LoadAsync( imageStream );

                image.Mutate( x => x.Resize( _options.Width, _options.Height ) );
                using var outputStream = new FileStream( path, FileMode.Create );
                await image.SaveAsync( outputStream, new SixLabors.ImageSharp.Formats.Png.PngEncoder() );
                outputStream.Close();
                return path;
            }
            catch (Exception e) {
                throw new FileWriteException( "Failed to save image", e );
            }
        }
        public void DeleteImage( Guid bookId ) {
            var path = GetImagePath( bookId );
            FileInfo fileInfo = new FileInfo( path );
            fileInfo.Delete();
        }
        public async Task<byte[]> GetImage( Guid bookId ) {

            string path = GetImagePath( bookId );
            if (File.Exists( path )) {
                return await File.ReadAllBytesAsync( path );
            } else {
                path = GetDefaultImagePath();
                return await File.ReadAllBytesAsync( path );
            }
        }
        public async Task<string> GetImageAsBase64( Guid bookId ) {
            if (_cache.TryGetValue( bookId, out string image )) {
                return image;
            }
            string path = GetImagePath( bookId );
            if (File.Exists( path )) {
                _cache.Set( bookId, image, TimeSpan.FromHours( 1 ) );
                return Convert.ToBase64String( await File.ReadAllBytesAsync( path ) );
            } else {
                _cache.Set( bookId, image, TimeSpan.FromHours( 1 ) );
                path = GetDefaultImagePath();
                return Convert.ToBase64String( await File.ReadAllBytesAsync( path ) );
            }
        }

        private string GetImagePath( Guid bookId ) {
            return Path.Combine( Directory.GetCurrentDirectory(), _options.PathToImages, bookId.ToString() + ".png" );
        }
        private string GetDefaultImagePath() {
            return Path.Combine( Directory.GetCurrentDirectory(), _options.PathToDefauldImage );
        }
    }
}
