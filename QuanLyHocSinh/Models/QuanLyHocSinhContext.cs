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

            });

            base.OnModelCreating(modelBuilder);
        }
    }
    
}
