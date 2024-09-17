using Library.Application.Auth.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Library.Infrastracture.Auth {
    public class PermissionRequirement: IAuthorizationRequirement {
        public PermissionRequirement( PermissionEnum[] permissions ) {
            Permissions = permissions;
        }

        public PermissionEnum[] Permissions { get; set; } = [];
    }
}
