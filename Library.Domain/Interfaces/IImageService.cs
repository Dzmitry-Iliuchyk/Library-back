using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Interfaces {
    public interface IImageService {
        Task<string> SaveImage(Stream imageStream, Guid bookId );
        void DeleteImage(Guid bookId);
        Task<byte[]> GetImage(Guid bookId);
        Task<string> GetImageAsBase64( Guid bookId );
    }
}
