namespace exercise.webapi.DTOs
{
    public class AuthorGetDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public List<BookInAuthorDto> Books { get; set; } = new List<BookInAuthorDto>(); // This will hold the books written by the author, if any
    }
}
