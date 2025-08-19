namespace exercise.webapi.DTOs
{
    // DTO used in put endpoint - representing that a client only can update the author via the author id
    public class BookPutDto
    {
        public int AuthorId { get; set; } // the new author id we want to assign to the book
    }
}
