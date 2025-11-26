using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BespokeBikesApi.Data.Models
{
    /// <summary>
    /// Representation of a Bespoke Bike's Customer.
    /// 
    /// Customers must be unique in the combination of Name and Contact value.
    /// </summary>
    [Index(nameof(Name), nameof(Contact), IsUnique = true)]
    public class Customer
    {
        /// <summary>
        /// ID of the Customer used to identify them.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Full name of the Customer
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Primary contact information. Can be a email or phone number.
        /// </summary>
        [Required]
        public string Contact { get; set; }

        /// <summary>
        /// Type of contact information stored in the Contact field.
        /// </summary>
        [Required]
        public ContactType ContactType { get; set; }
    }

    /// <summary>
    /// Enum representation of the type of contact stored in the Customer's contact field.
    /// 
    /// 0 - Email;
    /// 1 - Phone Number;
    /// </summary>
    public enum ContactType
    {
        /// <summary>
        /// Indicates the Contact of the Customer is an email.
        /// </summary>
        Email,

        /// <summary>
        /// Indicates the Contact of the Customer is a phone number.
        /// </summary>
        PhoneNumber
    }
}