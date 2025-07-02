using LearnLink_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnLink_Backend.Entities
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<ApplicationModel> InstructorApplications { get; set; }
        public DbSet<MeetingModel> Meetings { get; set; }
        public DbSet<SessionModel> Sessions { get; set; }
        public DbSet<CourseModel> Courses { get; set; }
        public DbSet<AnnouncementModel> Announcements { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<PostModel> Posts { get; set; }
        public DbSet<NotificationModel> Notifications { get; set; }
        public DbSet<Comment> Comments { get; set; }

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

            modelBuilder.Entity<ApplicationModel>()
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
            
            modelBuilder.Entity<NotificationModel>()
                .ToTable("Notifications")
                .HasKey(u => u.Id);

            modelBuilder.Entity<Comment>()
                .ToTable("Comments")
                .HasKey(u => u.Id);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Commenter)
                .WithMany(s => s.Comments)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
