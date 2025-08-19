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
            books.MapPut("/{id}", UpdateBook);
            books.MapDelete("/{id}", DeleteBook);
            books.MapPost("/", AddBook);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        private static async Task<IResult> GetBooks(IBookRepository bookRepository)
        {
            var books = await bookRepository.GetAllBooks();
            var bookDtos = books.Select(b => new BookGetDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author == null ? null : new AuthorInBookDto { Id = b.Author.Id, FirstName = b.Author.FirstName, LastName = b.Author.LastName, Email = b.Author.Email }
            });

            return TypedResults.Ok(bookDtos);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetBookById(int id, IBookRepository bookRepository)
        {
            var targetBook = await bookRepository.GetBookById(id);
            if (targetBook == null) return TypedResults.NotFound($"Book with ID {id} not found.");
            var bookDto = new BookGetDto
            {
                Id = targetBook.Id,
                Title = targetBook.Title,
                Author = targetBook.Author == null ? null : new AuthorInBookDto { Id = targetBook.Author.Id, FirstName = targetBook.Author.FirstName, LastName = targetBook.Author.LastName, Email = targetBook.Author.Email }
            };
            return TypedResults.Ok(bookDto);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> UpdateBook(int id, IBookRepository bookRepository, HttpRequest request, [FromBody] BookPutDto model)
        {
            if (model == null) return TypedResults.BadRequest("Invalid book data provided.");
            if (model.AuthorId <= 0) return TypedResults.BadRequest("Author ID must be a positive integer.");

            var bookToUpdate = await bookRepository.GetBookById(id);
            if (bookToUpdate == null) return TypedResults.NotFound($"Book with ID {id} not found.");

            bookToUpdate.AuthorId = model.AuthorId; // Update the book's author id if provided

            var updatedBook = await bookRepository.UpdateBook(id, bookToUpdate); // sends the updated product object to your repository method.

            // configure the response to return the updated book with its author
            var bookDto = new BookGetDto
            {
                Id = updatedBook.Id,
                Title = updatedBook.Title,
                Author = updatedBook.Author == null ? null : new AuthorInBookDto { Id = updatedBook.Author.Id, FirstName = updatedBook.Author.FirstName, LastName = updatedBook.Author.LastName, Email = updatedBook.Author.Email }

            };

            // send back the url of the product just updated
            var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";
            var location = $"{baseUrl}/products/{updatedBook.Id}";
            return TypedResults.Created(location, bookDto);
        
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> DeleteBook(int id, IBookRepository bookRepository)
        {
            var targetBook = await bookRepository.DeleteBook(id);
            if (targetBook == null) return TypedResults.NotFound($"Book with ID {id} not found.");
            return TypedResults.Ok(new BookGetDto
            {
                Id = targetBook.Id,
                Title = targetBook.Title,
                Author = targetBook.Author == null ? null : new AuthorInBookDto { Id = targetBook.Author.Id, FirstName = targetBook.Author.FirstName, LastName = targetBook.Author.LastName, Email = targetBook.Author.Email }

            });

        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> AddBook(IBookRepository bookRepository, HttpRequest request, [FromBody] BookPostDto model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Title)) { return TypedResults.BadRequest("Book object not valid"); }
            // TODO add check for if the author exists
            if (model.AuthorId <= 0) { return TypedResults.NotFound("Author ID not found. Author ID must be a positive integer."); }

            // check if the author exists
            var newBook = new Book
            {
                Title = model.Title,
                AuthorId = model.AuthorId
            };

            // add book to the repository
            var addedBook = await bookRepository.AddBook(newBook);

            // dto mapping for response
            var bookDto = new BookGetDto
            {
                Id = addedBook.Id,
                Title = addedBook.Title,
                Author = addedBook.Author == null ? null : new AuthorInBookDto { Id = addedBook.Author.Id, FirstName = addedBook.Author.FirstName, LastName = addedBook.Author.LastName, Email = addedBook.Author.Email }

            };

            return TypedResults.Created($"{request.Scheme}://{request.Host}{request.PathBase}/books/{addedBook.Id}", bookDto);

        }
    }
}
