using exercise.webapi.Models;

namespace exercise.webapi.DTOs
{
    // DTO for returning Book data and basic author and publisher details (without the collection of books the author and publisher has)
    public class BookGetDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public AuthorInBookDto Author { get; set; }

    }
}
