using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BespokeBikesApi.Data.Models
{
    [Index(nameof(Type), nameof(Name), IsUnique = true)]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public decimal CommissionPercentage { get; set; }
    }
}