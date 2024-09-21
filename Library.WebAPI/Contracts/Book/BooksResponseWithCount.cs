namespace Library.WebAPI.Contracts.Book {
    public class BooksResponseWithCount {
        public IList<BooksResponce> Books {  get; set; }
        public int Count { get; set; }

    }
}
