using exercise.webapi.Models;

namespace exercise.webapi.DTOs
{
    // DTO used for creating a new book containing only the data the client needs to provide when adding a new book
    public class BookPostDto
    {
        public string Title { get; set; } 
        public int AuthorId { get; set; } 
    }
}
