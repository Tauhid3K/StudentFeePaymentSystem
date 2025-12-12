using System.ComponentModel.DataAnnotations;

namespace StudentFeePaymentSystem.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        public int StudentId { get; set; }

        public string Semester { get; set; }

        public string? PaymentType { get; set; } // bKash, Nagad, DBBL

        public decimal Amount { get; set; }

        public string? TransactionId { get; set; }

        public string? Status { get; set; } = "Pending";// Pending, Approved

        // Navigation property
        public virtual Student Student { get; set; }
    }
}
