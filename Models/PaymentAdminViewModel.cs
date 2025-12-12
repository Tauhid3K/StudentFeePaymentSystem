namespace StudentFeePaymentSystem.Models
{
    public class PaymentAdminViewModel
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Roll { get; set; }
        public string Semester { get; set; }
        public string Email { get; set; }

        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
        public string? TransactionId { get; set; }
        public string Status { get; set; }
        public string PaymentType { get; set; }
    }
}
