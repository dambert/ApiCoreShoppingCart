using System;
using System.Collections.Generic;

namespace ShoppingCartFinal.Models
{
    public partial class Carts
    {
        public int RecordId { get; set; }
        public string CartId { get; set; }
        public int AlbumId { get; set; }
        public int Count { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual Albums Album { get; set; }
    }
}
