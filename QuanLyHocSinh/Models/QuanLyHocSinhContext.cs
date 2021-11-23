using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyHocSinh.Models
{
    public class QuanLyHocSinhContext: DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<ScheduleDetail> ScheduleDetails { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<TestType> testTypes { get; set; }
        public QuanLyHocSinhContext(DbContextOptions<QuanLyHocSinhContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(e =>
            {
                e.HasKey(e => e.Id);
                e.Property(e => e.Name).IsRequired(true).HasMaxLength(50);
                e.Property(e => e.Address).IsRequired(true).HasMaxLength(50);
                e.Property(e => e.Sex).IsRequired(true);
                e.Property(e => e.PhoneNumber).HasMaxLength(10);
                e.HasOne(b => b.ClassNavigation).WithMany(ac => ac.Students).HasForeignKey(b => b.ClassId).HasConstraintName("FK_Student_Class");

            });
            modelBuilder.Entity<Class>(e =>
            {
                e.HasKey(e => e.Id);
                e.Property(e => e.Name).IsRequired(true).HasMaxLength(10);
                e.Property(e => e.SchoolYear).IsRequired(true).HasMaxLength(20);
                e.HasOne(b => b.TeacherNavigation).WithMany(ac => ac.Classes).HasForeignKey(b => b.TeacherId).HasConstraintName("FK_Class_Teacher");



            });
            modelBuilder.Entity<Teacher>(e =>
            {
                e.HasKey(e => e.Id);
                e.Property(e => e.Name).IsRequired(true).HasMaxLength(50);
                e.Property(e => e.Address).IsRequired(true).HasMaxLength(50);
                e.Property(e => e.Sex).IsRequired(true);
                e.Property(e => e.PhoneNumber).HasMaxLength(10);
                e.HasOne(b => b.SubjectNavigation).WithMany(ac => ac.Teachers).HasForeignKey(b => b.SubjectId).HasConstraintName("FK_Teacher_Subject");

            });
            modelBuilder.Entity<Lesson>(e =>
            {
                e.HasKey(e => e.Id);
                e.Property(e => e.TimeStart).IsRequired(true);


            });
            modelBuilder.Entity<Schedule>(e =>
            {
                e.HasKey(e => e.Id);
                e.Property(e => e.CreateAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                e.Property(e => e.Week).IsRequired(true);




            });
            modelBuilder.Entity<ScheduleDetail>(e =>
            {
                e.HasKey(e => new
                {
                    e.LessonId,
                    e.ScheduleId,
                    e.SubjectId
                });
                e.HasOne(b => b.LessonNavigation).WithMany(ac => ac.ScheduleDetails).HasForeignKey(b => b.LessonId).HasConstraintName("FK_ScheduleDetail_Lesson");
                e.HasOne(b => b.ScheduleNavigation).WithMany(ac => ac.ScheduleDetails).HasForeignKey(b => b.ScheduleId).HasConstraintName("FK_ScheduleDetail_Schedule");
                e.HasOne(b => b.SubjectNavigation).WithMany(ac => ac.ScheduleDetails).HasForeignKey(b => b.SubjectId).HasConstraintName("FK_ScheduleDetail_Subject");


            });
            modelBuilder.Entity<Score>(entity =>
            {
                entity.HasKey(e => new { e.SemesterId, e.StudentId, e.TestTypeId, e.SubjectId })
                    .HasName("pk_score");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne<Semester>(e => e.SemesterNavigation)
                    .WithMany(e => e.Scores)
                    .HasForeignKey(e => e.SemesterId)
                    .HasConstraintName("fk_score_semester");
                entity.HasOne<Subject>(e => e.SubjectNavigation)
                    .WithMany(e => e.Scores)
                    .HasForeignKey(e => e.SubjectId)
                    .HasConstraintName("fk_score_subject");
                entity.HasOne<TestType>(e => e.TestTypeNavigation)
                    .WithMany(e => e.Scores)
                    .HasForeignKey(e => e.TestTypeId)
                    .HasConstraintName("fk_score_testtype");

            });
            modelBuilder.Entity<Semester>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("pk_semester");
            });
            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("pk_subject");
            });
            modelBuilder.Entity<TestType>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("pk_testType");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
    
}
