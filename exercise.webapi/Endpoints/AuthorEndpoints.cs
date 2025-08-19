namespace exercise.webapi.Endpoints
{
    public static class AuthorEndpoints
    {
        public static void ConfigureAuthorApi(this WebApplication app)
        {
            var authors = app.MapGroup("authors");

            authors.MapGet("/", GetAuthors);
            
        }
    }
}
