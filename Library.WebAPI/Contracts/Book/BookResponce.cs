namespace Library.WebAPI.Contracts.Book {
    public class BookResponce {
        public Guid Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        #region Author
        public Guid AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        #endregion Author
        public string Image { get; set; }
        public bool IsTaken { get; set; }

        #region User
        public Guid? ClientId { get; set; }
        public string? Username { get; set; }
        #endregion
    }
}
