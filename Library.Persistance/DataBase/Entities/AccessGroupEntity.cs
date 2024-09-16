using Library.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.Security;

namespace Library.DataAccess.DataBase.Entities {

    public class AccessGroupEntity {
        public int Id { get; set; }
        [MaxLength( 80 )]
        public string Name { get; set; } = string.Empty;
        public ICollection<PermissionEntity>? Permissions { get; set; } = [];
        public ICollection<UserEntity>? Users { get; set; } = [];
        public ICollection<UserAccessGroup>? UserGroups { get; set; } = [];
        public ICollection<AccessGroupPermission>? GroupPermissions { get; set; } = [];

    }
}
