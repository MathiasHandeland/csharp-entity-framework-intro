using exercise.webapi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace exercise.webapi.Endpoints
{
    public static class AuthorEndpoints
    {
        public static void ConfigureAuthorApi(this WebApplication app)
        {
            var authors = app.MapGroup("authors");

            authors.MapGet("/", GetAuthors);
            
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetAuthors(IAuthorRepository authorRepository)
        {
            var authors = await authorRepository.GetAllAuthors();
            if (authors == null || !authors.Any()) {  return TypedResults.NotFound("No authors found."); }
            
        }
    }
}
