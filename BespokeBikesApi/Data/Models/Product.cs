using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BespokeBikesApi.Data.Models
{
    /// <summary>
    /// Representation of a Product sold at Bespoke Bikes.
    /// 
    /// Products must be unique across Type and Name. 
    /// </summary>
    [Index(nameof(Type), nameof(Name), IsUnique = true)]
    public class Product
    {
        /// <summary>
        /// ID of the Product used to identify it. 
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The Type of Product. Currently, all items are "Bicycle", but this is not enforced in order to be extensible.
        /// </summary>
        [Required]
        public string Type { get; set; }

        /// <summary>
        /// The Name of the Product being sold.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// The commission a Salesperson recieves for each sale of this Product.
        /// </summary>
        public decimal CommissionPercentage { get; set; }
    }
}