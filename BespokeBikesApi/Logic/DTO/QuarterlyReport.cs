using BespokeBikesApi.Data.Models;

namespace BespokeBikesApi.Logic.DTO
{
    /// <summary>
    /// Representation of a Quarterly Commissions report for a given Salesperson.
    /// </summary>
    public class QuarterlyReport
    {
        /// <summary>
        /// The Salesperson the report is for.
        /// </summary>
        public Salesperson Salesperson { get; set; }

        /// <summary>
        /// The year the report covers.
        /// 
        /// Must be between 0 and the current year.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// The quarter of the year the report covers (1, 2, 3, or 4).
        /// </summary>
        public int Quarter { get; set; }

        /// <summary>
        /// The Total Commission earned by the Salesperson during the given year quarter.
        /// </summary>
        public decimal TotalCommission { get; set; }
    }
}