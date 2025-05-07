using KsiazkiOpinie.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace KsiazkiOpinie.ViewModels
{
    public class BookCreateViewModel
    {
        public Book? Book { get; set; }

        // Listy ID wybrane przez checkboxy (bindowane z formularza)
        public List<int> SelectedAuthorIds { get; set; } = new List<int>();
        public List<int> SelectedCategoryIds { get; set; } = new List<int>();

        // Listy do wyświetlenia w widoku
        public MultiSelectList? Authors { get; set; }
        public MultiSelectList? Categories { get; set; }
        public SelectList? Users { get; set; }
    }
}
