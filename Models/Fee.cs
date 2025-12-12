using System.ComponentModel.DataAnnotations;

namespace StudentFeePaymentSystem.Models
{
    public class Fee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Semester { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}
