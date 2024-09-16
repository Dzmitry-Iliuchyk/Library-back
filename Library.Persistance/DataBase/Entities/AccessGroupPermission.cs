
namespace Library.DataAccess.DataBase.Entities {
    public class AccessGroupPermission {
        public int GroupId { get; set; }
        public AccessGroupEntity? Group { get; set; }
        public int PermissionId { get; set; }
        public PermissionEntity? Permission { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
