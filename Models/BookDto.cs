using System;

namespace LibraryManagement.Models
{
    public class BookDto
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public DateTime PublishedOn { get; set; }
        public string Language { get; set; }
        public string Genre { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
