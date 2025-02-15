using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Concrete;
using Microsoft.Extensions.Configuration;
using static Shared.Enums;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistence
{
    //public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    //public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<JobSeeker> JobSeekers { get; set; }
        public DbSet<JobSeekerSkill> JobSeekerSkills { get; set; }
        public DbSet<JobSeekerExperience> JobSeekerExperiences { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Branch> Branch { get; set; }
        public DbSet<ApplicationPermission> Permissions { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<ApplicationUser>(entity => {
            //    entity.ToTable("MyCustomUsers");
            //});
            modelBuilder.Entity<ApplicationRole>(entity => {
                entity.ToTable("MyCustomRoles");
            });
            modelBuilder.Entity<ApplicationPermission>(entity => {
                entity.ToTable("MyCustomPermissions");
            });

            // User indexes
            modelBuilder.Entity<User>()
                .HasOne(x => x.UserCreatedBy)
                .WithMany(x => x.CreatedUsers)
                .HasForeignKey(x => x.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);


            // Branch indexes
            modelBuilder.Entity<Branch>()
                .HasOne(x => x.UserCreatedBy)
                .WithMany()
                .HasForeignKey(x => x.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Branch>()
                .HasOne(x => x.UserUpdatedBy)
                .WithMany()
                .HasForeignKey(x => x.UpdatedBy)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Branch>()
                .HasOne(x => x.UserDeletedBy)
                .WithMany()
                .HasForeignKey(x => x.DeletedBy)
            .OnDelete(DeleteBehavior.Restrict);


            // JobSeeker indexes
            modelBuilder.Entity<JobSeeker>()
                .HasIndex(js => js.Email)
                .IsUnique();
            modelBuilder.Entity<JobSeeker>()
                .Property(js => js.PhoneNumber)
                .HasMaxLength(15).IsRequired();


            // Specify relationships
            modelBuilder.Entity<JobSeekerSkill>()
                        .HasOne(js => js.JobSeeker)
                        .WithMany(j => j.Skills)
                        .HasForeignKey(js => js.JobSeekerId);

            modelBuilder.Entity<JobSeekerExperience>()
                     .HasOne(js => js.JobSeeker)
                     .WithMany(j => j.Experience)
                     .HasForeignKey(js => js.JobSeekerId);
        }
    }
}
