using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BespokeBikesApi.Data.Models
{
    /// <summary>
    /// Representation of a Salesperson who works at Bespoke Bikes.
    /// 
    /// Salesperson must be unique across Name and EmployeeId.
    /// </summary>
    [Index(nameof(Name), nameof(EmployeeId), IsUnique = true)]
    public class Salesperson
    {
        /// <summary>
        /// ID of a salesperson to identify them in the system.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Name of the Salesperson.
        /// </summary>
        /// 
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Identifier of the employee within Bespoke Bikes for the Salesperson.
        /// 
        /// This field has no valid format in order to allow for any type of ID.
        /// </summary>
        [Required]
        public string EmployeeId { get; set; }
    }
}