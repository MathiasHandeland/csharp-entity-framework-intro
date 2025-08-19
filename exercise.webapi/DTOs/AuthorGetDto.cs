namespace exercise.webapi.DTOs
{
    // DTO used for returning author data from the API, including a list of books written by the author
    public class AuthorGetDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Each book in the list uses BookInAuthorDto (which do not include author info on the book, only id and title)
        public List<BookInAuthorDto> Books { get; set; } = new List<BookInAuthorDto>(); // This will hold the books written by the author, if any
    }
}
