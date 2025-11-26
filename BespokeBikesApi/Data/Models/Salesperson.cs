using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BespokeBikesApi.Data.Models
{
    [Index(nameof(Name), nameof(EmployeeId), IsUnique = true)]
    public class Salesperson
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmployeeId { get; set; }
    }
}