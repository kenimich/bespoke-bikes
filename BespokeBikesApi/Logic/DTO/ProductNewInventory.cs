using BespokeBikesApi.Data.Models;

namespace BespokeBikesApi.Logic.DTO
{
    public class ProductNewInventory : Product
    {
        public int QuantityPurchased { get; set; }
        public decimal PurchasePrice { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}