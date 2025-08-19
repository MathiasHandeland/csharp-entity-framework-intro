namespace exercise.webapi.DTOs
{
    // DTO used inside BookGetDto to represent a author of a book with basic author details and without the collection of books a author has
    public class AuthorInBookDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}