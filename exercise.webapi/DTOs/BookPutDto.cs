namespace exercise.webapi.DTOs
{
    public class BookPutDto
    {
        // the client can only update author via id
        public int AuthorId { get; set; } // the new author id we want to assign to the book
    }
}
