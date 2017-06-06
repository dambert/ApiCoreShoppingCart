using System;
using System.Collections.Generic;

namespace ShoppingCartFinal.Models
{
    public partial class OrderDetails
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int AlbumId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public virtual Albums Album { get; set; }
        public virtual Orders Order { get; set; }
    }
}
