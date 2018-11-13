using KorepetycjeNaJuz.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KorepetycjeNaJuz.Infrastructure
{
    public class KorepetycjeContext : IdentityDbContext<User,IdentityRole<int>,int>
    {
        public static string ConnectionString { get; set; }

        public KorepetycjeContext()
            : base()
        {

        }

        public KorepetycjeContext(DbContextOptions<KorepetycjeContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Address>().ToTable("Addresses");
            builder.Entity<CoachLesson>().ToTable("CoachLessons");
            builder.Entity<LessonLevel>().ToTable("LessonLevels");
            builder.Entity<Lesson>().ToTable("Lessons");
            builder.Entity<LessonStatus>().ToTable("LessonStatuses");
            builder.Entity<LessonSubject>().ToTable("LessonSubjects");
            builder.Entity<Message>().ToTable("Messages");
            builder.Entity<User>().ToTable("Users");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(ConnectionString);
        }

        public DbSet<Address> CoachAddresses { get; set; }
        public DbSet<CoachLesson> CoachLessons { get; set; }
        public DbSet<LessonLevel> LessonLevels { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonStatus> LessonStatuses { get; set; }
        public DbSet<LessonSubject> LessonSubjects { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
