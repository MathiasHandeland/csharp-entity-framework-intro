using exercise.webapi.DTOs;
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
            if (authors == null || !authors.Any()) { return TypedResults.NotFound("No authors found."); }

            var authorDtos = authors.Select(a => new AuthorGetDto
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Email = a.Email,
                Books = a.Books.Select(b => new BookInAuthorDto
                {
                    Id = b.Id,
                    Title = b.Title,
                }).ToList() // Include books written by the author
            });
            return TypedResults.Ok(authorDtos);

        }
    }
}
