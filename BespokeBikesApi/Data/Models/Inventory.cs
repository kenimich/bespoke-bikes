using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BespokeBikesApi.Data.Models
{
    public class Inventory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Product.Id))]
        public int ProductId { get; set; }

        public int Quantity { get; set; }
        public decimal PurchasePrice { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}