using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BespokeBikesApi.Data.Models
{
    [Index(nameof(Name), nameof(Contact), IsUnique = true)]
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Contact { get; set; }
        [Required]
        public ContactType ContactType { get; set; }
    }

    public enum ContactType
    {
        Email,
        PhoneNumber
    }
}