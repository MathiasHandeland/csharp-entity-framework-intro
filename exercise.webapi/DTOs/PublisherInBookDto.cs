namespace exercise.webapi.DTOs
{
    // DTO used inside BookGetDto to represent a publisher of a book with basic publisher details and without the collection of books a publisher has
    public class PublisherInBookDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
