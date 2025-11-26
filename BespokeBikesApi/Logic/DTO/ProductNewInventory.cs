using BespokeBikesApi.Data.Models;

namespace BespokeBikesApi.Logic.DTO
{
    /// <summary>
    /// Representation of a New Product or New Product Purchased from the Manufacturer.
    /// </summary>
    public class ProductNewInventory : Product
    {
        /// <summary>
        /// Quantity Purchased from the Manufacturer.
        /// </summary>
        public int QuantityPurchased { get; set; }

        /// <summary>
        /// Price the Product was purchased from the Manufacturer at.
        /// </summary>
        public decimal PurchasePrice { get; set; }

        /// <summary>
        /// Date of the purchase from the manufacturer in ISO 8601 format like YYYY-MM-DD.
        /// </summary>
        public DateTime PurchaseDate { get; set; }
    }
}