namespace Library.DataAccess.DataBase.Entities {
    public class UserEntity {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public ICollection<BookEntity>? Books { get; set; } = new List<BookEntity>();
        public ICollection<AccessGroupEntity>? Groups { get; set; } = new List<AccessGroupEntity>();
        public ICollection<UserAccessGroup>? UserGroups { get; set; } = new List<UserAccessGroup>();
    }
}
