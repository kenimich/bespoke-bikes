using BespokeBikesApi.Data.Models;

namespace BespokeBikesApi.Logic.DTO
{
    public class ProductCurrentInventory : Product
    {
        public int QuantityInStock { get; set; }

        public List<Inventory> PurchasePrices { get; set; }
    }
}