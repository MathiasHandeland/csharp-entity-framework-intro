using exercise.webapi.Models;

namespace exercise.webapi.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        public async Task<IEnumerable<Author>> GetAllAuthors()
        {
            throw new NotImplementedException();
        }

        public async Task<Author> GetAuthorById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
