using System;
using System.Collections.Generic;

namespace ShoppingCartFinal.Models
{
    public partial class Genres
    {
        public Genres()
        {
            Albums = new HashSet<Albums>();
        }

        public int GenreId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Albums> Albums { get; set; }
    }
}
