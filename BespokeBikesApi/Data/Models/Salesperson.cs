using System.ComponentModel.DataAnnotations;

namespace BespokeBikesApi.Data.Models
{
    public class Salesperson
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmployeeId { get; set; }
    }
}