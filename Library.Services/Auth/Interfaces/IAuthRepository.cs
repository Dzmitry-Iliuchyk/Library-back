using Library.Application.Auth.Enums;
using Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Auth.Interfaces {
    public interface IAuthRepository {
        Task AddUserToGroup( User user, AccessGroupEnum group );
        Task<string?> GetActiveRefreshToken( Guid userId );
        Task<HashSet<AccessGroupEnum>> GetUserGroups( Guid userId );
        Task<HashSet<PermissionEnum>> GetUserPermissions( Guid userId );
        Task RemoveOldRefreshTokens( Guid userId );
        Task RemoveRefreshToken( string refreshToken );
        Task RemoveUserFromGroup( User user, AccessGroupEnum group );
        Task SaveRefreshToken( Guid userId, string token );
    }
}
