using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace exercise.webapi.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        [JsonIgnore] // Todo: replace this with DTO approach
        public ICollection<Book> Books { get; set; } = new List<Book>(); // a one-to-many relationship with Book - an author can have many books
    }
}
