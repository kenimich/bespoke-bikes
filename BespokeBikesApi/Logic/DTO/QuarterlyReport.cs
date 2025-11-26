using BespokeBikesApi.Data.Models;

namespace BespokeBikesApi.Logic.DTO
{
    public class QuarterlyReport
    {
        public Salesperson Salesperson { get; set; }
        public int Year { get; set; }
        public int Quarter { get; set; }
        public decimal TotalCommission { get; set; }
    }
}