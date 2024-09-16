using System.ComponentModel.DataAnnotations;


namespace Library.DataAccess.DataBase.Entities {
    public class PermissionEntity {
        public int Id { get; set; }
        [MaxLength( 80 )]
        public string Name { get; set; }
        public IEnumerable<AccessGroupEntity>? Groups { get; set; }
        public IEnumerable<AccessGroupPermission>? GroupPermissions { get; set; } = [];
    }
}
