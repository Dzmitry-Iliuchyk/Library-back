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
        Task<HashSet<AccessGroupEnum>> GetUserGroups( Guid userId );
        Task<HashSet<PermissionEnum>> GetUserPermissions( Guid userId );
        Task RemoveUserFromGroup( User user, AccessGroupEnum group );
    }
}
