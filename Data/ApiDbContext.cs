using Microsoft.EntityFrameworkCore;
using Project_Apis.Models;

namespace Project_Apis.Data
{
    public class ApiDbContext:DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options):base(options) 
        { }

        public DbSet<Appointments> Appointments { get; set; }
        
        public DbSet<AppointmentData> AppointmentDatas { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Hospital> Hospitals { get; set; }

        public DbSet<Tickets> Tickets { get; set; }
    }
}
