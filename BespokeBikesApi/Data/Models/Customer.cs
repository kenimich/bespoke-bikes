using System.ComponentModel.DataAnnotations;

namespace BespokeBikesApi.Data.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public ContactType ContactType { get; set; }
    }

    public enum ContactType
    {
        Email,
        PhoneNumber
    }
}