using Microsoft.EntityFrameworkCore;
using SchoolManagementWebAPI.Models;

namespace SchoolManagementWebAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { }
        
        public DbSet<Event> Events { get; set; }
        public DbSet<AcademicCalendar> AcademicCalendars { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
