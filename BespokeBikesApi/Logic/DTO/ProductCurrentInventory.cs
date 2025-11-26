using BespokeBikesApi.Data.Models;

namespace BespokeBikesApi.Logic.DTO
{
    /// <summary>
    /// Representation of the connection between a Product and Inventory, as it's current level.
    /// </summary>
    public class ProductCurrentInventory : Product
    {
        /// <summary>
        /// The current stock of the Product, adding Product Purchased from Manufacturer and subtracting Product sold to customers.
        /// </summary>
        public int QuantityInStock { get; set; }

        /// <summary>
        /// List of Prices at which the Product was purchased from the Manufacturer.
        /// </summary>
        public List<Inventory> PurchasePrices { get; set; }

        /// <summary>
        /// Default construct to initialize lists.
        /// </summary>
        public ProductCurrentInventory() : base()
        {
            this.PurchasePrices = new List<Inventory>();            
        }
    }
}