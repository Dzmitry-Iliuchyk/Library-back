namespace Library.Application.Helpers {
    public class BookValidationOptions {
        public int ISBNMinimumLength { get; set; }
        public int ISBNMaximumLength { get; set; }
        public int TitleMinimumLength { get; set; }
        public int DescriptionMinimumLength { get; set; }
        public int GenreMinimumLength { get; set; }
        public int TakenBookTakenAtMaximumAgeFromNow { get; set; }
        public int TakenBookReturnToMaximumAgeFromNow { get; set; }
    }
}

