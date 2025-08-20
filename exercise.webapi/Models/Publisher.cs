using System.ComponentModel.DataAnnotations;

namespace exercise.webapi.Models
{
    public class Publisher
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>(); // a one-to-many relationship with Book - an publisher can have many books

    }
}
