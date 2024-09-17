using Library.Application.Auth.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Library.Infrastracture.Auth {
    public class GroupRequirement : IAuthorizationRequirement
    {
        public GroupRequirement(AccessGroupEnum[] group)
        {
            Group = group;
        }

        public AccessGroupEnum[] Group { get; set; } = [];
    }
}
