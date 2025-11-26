using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BespokeBikesApi.Data.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        [ForeignKey("Salesperson")]
        public int SalespersonId { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal SalePrice { get; set; }
        public decimal Commission { get; set; }
    }
}