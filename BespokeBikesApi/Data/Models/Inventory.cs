using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BespokeBikesApi.Data.Models
{
    /// <summary>
    /// Representation of a Product's Purchased Inventory. This does not take into account how much has been sold, only what has been purchased.
    /// 
    /// Inventory must be tied to a specific Product thourgh the Product's ID.
    /// </summary>
    public class Inventory
    {
        /// <summary>
        /// The ID of the Purchase to identify it in the system.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The ID of the Product that was purchased.
        /// </summary>
        [Required]
        [ForeignKey(nameof(Product.Id))]
        public int ProductId { get; set; }

        /// <summary>
        /// Amount purchased from the manufacturer.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Price at which the inventory was purchased from the manufacturer. This should be per unit.
        /// </summary>
        public decimal PurchasePrice { get; set; }

        /// <summary>
        /// Date of the Product's purchase in ISO 8601 format like YYYY-MM-DD.
        /// </summary>
        public DateTime PurchaseDate { get; set; }
    }
}