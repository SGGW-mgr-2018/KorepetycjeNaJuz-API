using KorepetycjeNaJuz.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace KorepetycjeNaJuz.Infrastructure
{
    public class KorepetycjeContext : DbContext
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
            builder.Entity<CoachAddresses>().ToTable("CoachAddresses");
            builder.Entity<CoachLessons>().ToTable("CoachLessons");
            builder.Entity<LessonLevels>().ToTable("LessonLevels");
            builder.Entity<Lessons>().ToTable("Lessons");
            builder.Entity<LessonStatuses>().ToTable("LessonStatuses");
            builder.Entity<LessonSubjects>().ToTable("LessonSubjects");
            builder.Entity<Messages>().ToTable("Messages");
            builder.Entity<Users>().ToTable("Users");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        public DbSet<CoachAddresses> CoachAddresses { get; set; }
        public DbSet<CoachLessons> CoachLessons { get; set; }
        public DbSet<LessonLevels> LessonLevels { get; set; }
        public DbSet<Lessons> Lessons { get; set; }
        public DbSet<LessonStatuses> LessonStatuses { get; set; }
        public DbSet<LessonSubjects> LessonSubjects { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
