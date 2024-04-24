using System.ComponentModel.DataAnnotations;

namespace BookShoppingCartMvcUI.Models.DTOs
{
    public class StockDTO
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }

    }
}
