using exercise.webapi.DTOs;
using exercise.webapi.Models;
using exercise.webapi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace exercise.webapi.Endpoints
{
    public static class BookApi
    {
        public static void ConfigureBooksApi(this WebApplication app)
        {
            var books = app.MapGroup("books");

            books.MapGet("/", GetBooks);
            books.MapGet("/{id}", GetBookById);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        private static async Task<IResult> GetBooks(IBookRepository bookRepository)
        {
            var books = await bookRepository.GetAllBooks();
            var bookDtos = books.Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author == null ? null : new Author { Id = b.Author.Id, FirstName = b.Author.FirstName, LastName = b.Author.LastName, Email = b.Author.Email }  
            });

            return TypedResults.Ok(bookDtos);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetBookById(int id, IBookRepository bookRepository)
        {
            var targetBook = await bookRepository.GetBookById(id);
            if (targetBook == null) return TypedResults.NotFound($"Book with ID {id} not found.");
            var bookDto = new BookDto
            {
                Id = targetBook.Id,
                Title = targetBook.Title,
                Author = targetBook.Author == null ? null : new Author { Id = targetBook.Author.Id, FirstName = targetBook.Author.FirstName, LastName = targetBook.Author.LastName, Email = targetBook.Author.Email }  
            };
            return TypedResults.Ok(bookDto);
        }
    }
}
