using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace KsiazkiOpinie.Models
{
    public class Comment
    {
        public int Id { get; set; }
        
        public string Content { get; set; }

        
        public string AuthorName { get; set; }

        public int BookId { get; set; }
        public Book? Book { get; set; }

    }
}
