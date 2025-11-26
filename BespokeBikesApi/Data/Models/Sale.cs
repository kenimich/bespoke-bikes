using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BespokeBikesApi.Data.Models
{
    /// <summary>
    /// Representation of sales information at Bespoke Bikes.
    /// 
    /// This represents a single instance of the product being sold.
    /// </summary>
    public class Sale
    {
        /// <summary>
        /// ID of the sales information to identify it.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// ID of the Product which was sold.
        /// </summary>
        [Required]
        [ForeignKey(nameof(Product.Id))]
        public int ProductId { get; set; }

        /// <summary>
        /// ID of the Customer who purchased the Product.
        /// </summary>
        [Required]
        [ForeignKey(nameof(Customer.Id))]
        public int CustomerId { get; set; }

        /// <summary>
        /// ID of the Salesperson who sold the Product.
        /// </summary>
        [Required]
        [ForeignKey(nameof(Salesperson.Id))]
        public int SalespersonId { get; set; }
        
        /// <summary>
        /// Date of the sale in ISO 8601 format like YYYY-MM-DD.
        /// </summary>
        [Required]
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// Price point at which the Product was sold.
        /// </summary>
        [Required]
        public decimal SalePrice { get; set; }

        /// <summary>
        /// The comission given to the salesperson for this sale. 
        /// 
        /// Calculated on the backend at the time the sale is made.
        /// </summary>
        public decimal Commission { get; set; }
    }
}