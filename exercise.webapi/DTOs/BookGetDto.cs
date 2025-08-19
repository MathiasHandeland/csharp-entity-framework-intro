using exercise.webapi.Models;

namespace exercise.webapi.DTOs
{
    // DTO for returning Book data and basic author details (without the collection of books the author has)
    public class BookGetDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public AuthorInBookDto Author { get; set; }
    }
}
