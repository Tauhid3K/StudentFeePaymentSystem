using Microsoft.EntityFrameworkCore;
using StudentFeePaymentSystem.Models;
using StudentFeePaymentSystem.Models;

namespace StudentFeePaymentSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Admin> Admins { get; set; }  // <-- Add this line
        public DbSet<Fee> Fees { get; set; }

    }
}
