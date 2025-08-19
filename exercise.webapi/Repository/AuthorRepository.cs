using exercise.webapi.Data;
using exercise.webapi.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.webapi.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private DataContext _db;

        public AuthorRepository(DataContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Author>> GetAllAuthors()
        {
            return await _db.Authors
                .Include(a => a.Books) // Include books written by the author
                .ToListAsync(); // Fetch all authors with their books
        }

        public async Task<Author> GetAuthorById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
