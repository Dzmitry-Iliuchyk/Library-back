namespace Library.WebAPI.Contracts.Middleware {
    public record ErrorResponse(
        string Name,
    int Status,
    string[] Messages );
}
