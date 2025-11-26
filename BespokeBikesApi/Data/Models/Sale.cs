using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BespokeBikesApi.Data.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Product.Id))]
        public int ProductId { get; set; }

        [Required]
        [ForeignKey(nameof(Customer.Id))]
        public int CustomerId { get; set; }

        [Required]
        [ForeignKey(nameof(Salesperson.Id))]
        public int SalespersonId { get; set; }
        
        [Required]
        public DateTime SaleDate { get; set; }

        [Required]
        public decimal SalePrice { get; set; }

        public decimal Commission { get; set; }
    }
}