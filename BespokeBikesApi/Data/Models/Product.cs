using System.ComponentModel.DataAnnotations;

namespace BespokeBikesApi.Data.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal CommissionPercentage { get; set; }
    }
}