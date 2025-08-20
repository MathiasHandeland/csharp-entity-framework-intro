using exercise.webapi.DTOs;
using exercise.webapi.Models;
using exercise.webapi.Repository;
using Microsoft.AspNetCore.Mvc;


namespace exercise.webapi.Endpoints
{
    public static class AuthorApi
    {
        public static void ConfigureAuthorApi(this WebApplication app)
        {
            var authors = app.MapGroup("authors");

            authors.MapGet("/", GetAuthors);
            authors.MapGet("/{id}", GetAuthorById);

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetAuthors(IRepository<Author> authorRepository)
        {
            var authors = await authorRepository.GetWithIncludes(a => a.Books); // Include books written by the author
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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetAuthorById(int id, IRepository<Author> authorRepository)
        {
            var author = (await authorRepository.GetWithIncludes(a => a.Books)).FirstOrDefault(a => a.Id == id);
            if (author == null) return TypedResults.NotFound($"Author with ID {id} not found.");
            var authorDto = new AuthorGetDto
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Email = author.Email,
                Books = author.Books.Select(b => new BookInAuthorDto
                {
                    Id = b.Id,
                    Title = b.Title,
                }).ToList() // Include books written by the author
            };
            return TypedResults.Ok(authorDto);
        }
    }
}
