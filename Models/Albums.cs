using System;
using System.Collections.Generic;

namespace ShoppingCartFinal.Models
{
    public partial class Albums
    {
        public Albums()
        {
            Carts = new HashSet<Carts>();
            OrderDetails = new HashSet<OrderDetails>();
        }

        public int AlbumId { get; set; }
        public int GenreId { get; set; }
        public int ArtistId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string AlbumArtUrl { get; set; }

        public virtual ICollection<Carts> Carts { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
        public virtual Artists Artist { get; set; }
        public virtual Genres Genre { get; set; }
    }
}
