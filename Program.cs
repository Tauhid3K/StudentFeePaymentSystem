using Microsoft.EntityFrameworkCore;
using StudentFeePaymentSystem.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add MVC support
builder.Services.AddControllersWithViews();

// Configure MySQL (XAMPP)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 30))
    )
);

// Enable Session (optional, can still keep session)
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// --- Add Cookie Authentication ---
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Admin/Login";          // redirect if not logged in
        options.ExpireTimeSpan = TimeSpan.FromDays(7); // cookie valid for 7 days
        options.SlidingExpiration = true;           // extend cookie on activity
    });

var app = builder.Build();

// Error handling
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

// Middleware pipeline
app.UseStaticFiles();

app.UseRouting();

app.UseSession();               // session middleware
app.UseAuthentication();        // must be before UseAuthorization
app.UseAuthorization();

// Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
