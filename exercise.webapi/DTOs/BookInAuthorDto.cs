namespace exercise.webapi.DTOs
{
    // DTO used in AuthorGetDto to represent a book without author details
    public class BookInAuthorDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}