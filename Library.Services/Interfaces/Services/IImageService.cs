using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Interfaces.Services {
    public interface IImageService {
        Task<string> SaveImage( Stream imageStream, Guid bookId, string format = "png" );
        void DeleteImage(Guid bookId);
        Task<byte[]> GetImage(Guid bookId);
        Task<string> GetImageAsBase64( Guid bookId );
    }
}
