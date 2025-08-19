using exercise.webapi.Models;

namespace exercise.webapi.Repository
{
    public interface IAuthorRepository
    {
        public Task<IEnumerable<Author>> GetAllAuthors();

        public Task<Author> GetAuthorById(int id);
    }
}
