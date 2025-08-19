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
    }
}
