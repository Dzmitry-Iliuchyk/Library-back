

namespace Library.DataAccess.DataBase.Entities {
    public class UserAccessGroup {
        public Guid UserId { get; set; }
        public UserEntity? User { get; set; }
        public int GroupId { get; set; }
        public AccessGroupEntity? Group { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
