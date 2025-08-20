using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exercise.webapi.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty; // Title of the book, cannot be null or empty

        [ForeignKey("Author")]
        public int AuthorId { get; set; } // Foreign key to Author - each book is written by ONE author
        public Author Author { get; set; } = null!; // Navigation property to Author

        
    }
}
