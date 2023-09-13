using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Web.Entities;
using PMS.WEB.DAL.Entities;
using System;

namespace PMS.Web.Data
{
    public class ApplicationDbContext: IdentityDbContext<AppUser,AppUserRoles,string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new EmployeeConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.Entity<Allergy>(entity => 
            {
                entity.HasIndex(e => new { e.AllergyId, e.Isoforms }).IsUnique();
            });
        }
        public DbSet<HospitalUser> HospitalUser { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientAllergy> PatientAllergies { get; set; }
        public DbSet<Allergy> Allergies { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<PatientVisit> PatientVisits { get; set; }
        public DbSet<PatientMedication> PatientMedications { get; set; }
        public DbSet<PatientProcedure> PatientProcedures { get; set; }
        public DbSet<PatientDiagnosis> PatientDiagnoses { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Scheduling> Schedulings { get; set; }
        public DbSet<SchedulingHistory> SchedulingHistory { get; set; }
        public DbSet<TimeSlot> TimeSlot { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ReplyMessage> ReplyMessages { get; set; }

    }

    // Class to Seed Test Data for Employees
    public class EmployeeConfiguration : IEntityTypeConfiguration<HospitalUser>
    {
        public void Configure(EntityTypeBuilder<HospitalUser> builder)
        {
            builder.HasData(
                new HospitalUser
                {
                    EmployeeId = Guid.Parse("228ef8b1-e6db-4163-aeac-c3689a2b1dec"),
                    //AppUserId = Guid.Parse("c2076993-ba3c-4c47-8657-fb9ef1af0c5a")
                    //FirstName = "David",
                    //LastName="Jones",
                    //BirthDate=new DateTime(1985,4,4)                    
                },
                new HospitalUser
                {
                    EmployeeId = Guid.Parse("afa8d15c-623f-406b-8c1d-7911e6614cf7"),
                    //AppUserId = Guid.Parse("6886bb5e-07ec-4110-9533-30db833a22c7")
                    //FirstName = "John",
                    //LastName = "Doe"
                }
            );
        }
    }

    public class UserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasData(
                new AppUser
                {
                    Id = "c0d82ea7-c286-429a-9d30-37a0dcfb250c",
                    ConcurrencyStamp = "89ef8d55-023c-41ec-93cc-cb79ff8fee8a",
                    SecurityStamp = "e74d3943-c822-4dbd-919a-582b12ec7262",
                    UserName = "User123",
                    FirstName = "David",
                    LastName = "Jones",
                    BirthDate = new DateTime(1985, 4, 4)
                },
                new AppUser
                {
                    Id = "478d4720-53d3-48be-a383-990207507b73",
                    ConcurrencyStamp = "3eacfc6e-4baa-4d4e-936e-80d07170cc93",
                    SecurityStamp = "b42120de-15a7-4b68-9732-bdbed0b87a6c",
                    UserName = "User1234",
                    FirstName = "",
                    LastName = "Doe",
                    BirthDate = new DateTime(1987, 9, 7),
                    RegistrationDate = new DateTime(2018, 10, 10),
                }
            );
        }
    }
}
