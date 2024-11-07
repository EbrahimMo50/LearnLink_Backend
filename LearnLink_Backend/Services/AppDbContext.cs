using LearnLink_Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace LearnLink_Backend.Services
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Student> Students {  get; set; }
        public DbSet<Admin> Admins {  get; set; }
        public DbSet<Instructor> Instructors {  get; set; }
        public DbSet<InstructorApplication> InstructorApplications { get; set; }
        public DbSet<Meeting> Meetings {  get; set; }
        public DbSet<Schedule> Schedules {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>()
                .ToTable("Students")
                .HasKey(u => u.Id);
            
            modelBuilder.Entity<Admin>()
                .ToTable("Admins")
                .HasKey(u => u.Id);
            
            modelBuilder.Entity<Instructor>()
                .ToTable("Instructors")
                .HasKey(u => u.Id);
            
            modelBuilder.Entity<Meeting>()
                .ToTable("Meetings")
                .HasKey(u => u.Id);
            
            modelBuilder.Entity<Schedule>()
                .ToTable("Schedules")
                .HasKey(u => u.Id);

            modelBuilder.Entity<InstructorApplication>()
                .ToTable("Applications")
                .HasKey(u => u.Id);

        }
    }
}
