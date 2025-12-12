using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentFeePaymentSystem.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Session is required")]
        [StringLength(20, ErrorMessage = "Session cannot exceed 20 characters")]
        [Display(Name = "Session")]
        public string Session { get; set; }

        [Required(ErrorMessage = "Roll number is required")]
        [StringLength(20, ErrorMessage = "Roll cannot exceed 20 characters")]
        [Display(Name = "Roll Number")]
        public string Roll { get; set; }

        [Required(ErrorMessage = "Semester is required")]
        [StringLength(10, ErrorMessage = "Semester cannot exceed 10 characters")]
        [Display(Name = "Semester")]
        public string Semester { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(100)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Pending Fees")]
        [DataType(DataType.Currency)]
        public decimal PendingFees { get; set; }  // optional: can track outstanding balance

        // Navigation property
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
