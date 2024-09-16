namespace Library.DataAccess.DataBase.Entities {
    public class UserEntity {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public IEnumerable<BookEntity>? Books { get; set; }
        public IEnumerable<AccessGroupEntity>? Groups { get; set; }
        public IEnumerable<UserAccessGroup>? UserGroups { get; set; }
    }
}
