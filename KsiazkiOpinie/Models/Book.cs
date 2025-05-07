using System.Xml.Linq;

namespace KsiazkiOpinie.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Rating>? Ratings { get; set; }
        public ICollection<Author>? Authors { get; set; }
        public ICollection<Category>? Categories { get; set; }
        public ICollection<AuthorBook>? AuthorBooks { get; set; }
        public ICollection<CategoryBook>? CategoryBooks { get; set; }

    }
}
