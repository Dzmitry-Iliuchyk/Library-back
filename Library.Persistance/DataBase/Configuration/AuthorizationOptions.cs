namespace Library.DataAccess.DataBase.Configuration {
    public class AuthorizationOptions {
        public AccessGroupPermission[] GroupPermissions { get; set; } = [];
    }
    public class AccessGroupPermission {
        public string Group { get; set; } = string.Empty;
        public string[] Permissions { get; set; } = [];
    }
}
