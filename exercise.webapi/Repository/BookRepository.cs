using exercise.webapi.Data;
using exercise.webapi.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.webapi.Repository
{
    public class BookRepository: IBookRepository
    {
        DataContext _db;
        
        public BookRepository(DataContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _db.Books.Include(b => b.Author).ToListAsync(); // makes sure author data is included in each book

        }

        public async Task<Book> GetBookById(int id)
        {
            return await _db.Books
                .Include(b => b.Author) // include author data
                .FirstOrDefaultAsync(b => b.Id == id); // find the target book by id
        }

        public async Task<Book> UpdateBook(int id, Book model)
        {
            var targetBook = await _db.Books.FindAsync(id); // finds the book by id
            if (targetBook == null) return null; // if book not found, return null
            targetBook.AuthorId = model.AuthorId; // Updates the book's author id
            await _db.SaveChangesAsync(); // Saves changes to the database asynchronously
            return await _db.Books
                .Include(b => b.Author) // include author data
                .FirstOrDefaultAsync(b => b.Id == id); // return the updated book with its author
        }

        public async Task<Book> DeleteBook(int id)
        {
            var targetBook = await _db.Books
                .Include(b => b.Author) // include author data
                .FirstOrDefaultAsync(b => b.Id == id); // find the book by id
            if (targetBook == null) return null; // if book not found, return null
            _db.Books.Remove(targetBook); // remove the book from the database
            await _db.SaveChangesAsync(); // save changes to the database asynchronously
            return targetBook; // return the deleted book
        }

        public async Task<Book> AddBook(Book book)
        {
            await _db.Books.AddAsync(book); // add the new book to the database
            await _db.SaveChangesAsync(); // save changes to the database asynchronously
            return await _db.Books
                .Include(b => b.Author) // include author data
                .FirstOrDefaultAsync(b => b.Id == book.Id); // return the newly added book with its author
        }
    }
}
