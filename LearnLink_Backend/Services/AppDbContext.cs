using LearnLink_Backend.Models;
using LearnLink_Backend.Modules.Adminstration.Models;
using LearnLink_Backend.Modules.Courses.Models;
using LearnLink_Backend.Modules.Meeting;
using LearnLink_Backend.Modules.Announcement;
using LearnLink_Backend.Modules.Session;
using Microsoft.EntityFrameworkCore;
using LearnLink_Backend.Modules.Post;

namespace LearnLink_Backend.Services
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Student> Students {  get; set; }
        public DbSet<Admin> Admins {  get; set; }
        public DbSet<Instructor> Instructors {  get; set; }
        public DbSet<InstructorApplicationModel> InstructorApplications { get; set; }
        public DbSet<MeetingModel> Meetings {  get; set; }
        public DbSet<SessionModel> Sessions {  get; set; }
        public DbSet<CourseModel> Courses { get; set; }
        public DbSet<AnnouncementModel> Announcements { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<PostModel> Posts { get; set; }

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

            modelBuilder.Entity<MeetingModel>()
                .ToTable("Meetings")
                .HasKey(u => u.Id);

            modelBuilder.Entity<SessionModel>()
                .ToTable("Sessions")
                .HasKey(u => u.Id);

            modelBuilder.Entity<InstructorApplicationModel>()
                .ToTable("Applications")
                .HasKey(u => u.Id);

            modelBuilder.Entity<CourseModel>()
                .ToTable("Courses")
                .HasKey(u => u.Id);

            modelBuilder.Entity<AnnouncementModel>()
                .ToTable("Announcements")
                .HasKey(u => u.Id);

            modelBuilder.Entity<Schedule>()
                .ToTable("Schedules")
                .HasKey(u => u.Id);

            modelBuilder.Entity<PostModel>()
                .ToTable("Posts")
                .HasKey(u => u.Id);
        }
    }
}
