using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebAPIBiz4Company.Models;

namespace WebAPIBiz4Company.Data
{
    public partial class WEBPUBLICBETAContext : DbContext
    {
        public WEBPUBLICBETAContext()
        {
        }

        public WEBPUBLICBETAContext(DbContextOptions<WEBPUBLICBETAContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Biz4> Biz4s { get; set; } = null!;
        public virtual DbSet<Job> Jobs { get; set; } = null!;
        public virtual DbSet<JobApplied> JobApplieds { get; set; } = null!;
        public virtual DbSet<JobApplier> JobAppliers { get; set; } = null!;
        public virtual DbSet<JobDescription> JobDescriptions { get; set; } = null!;
        public virtual DbSet<JobType> JobTypes { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:DatabaseAPIBiz4");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Biz4>(entity =>
            {
                entity.ToTable("biz4s");
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("Job");

                entity.Property(e => e.JobId).HasColumnName("jobId");

                entity.Property(e => e.JobAddress)
                    .HasMaxLength(255)
                    .HasColumnName("jobAddress");

                entity.Property(e => e.JobDateCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("jobDateCreated");

                entity.Property(e => e.JobDescription).HasColumnName("jobDescription");

                entity.Property(e => e.JobName)
                    .HasMaxLength(255)
                    .HasColumnName("jobName");

                entity.Property(e => e.JobType).HasColumnName("jobType");

                entity.Property(e => e.JobWorkingForm)
                    .HasMaxLength(50)
                    .HasColumnName("jobWorkingForm");

                entity.HasOne(d => d.JobTypeNavigation)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.JobType)
                    .HasConstraintName("FkJob_jobType");
            });

            modelBuilder.Entity<JobApplied>(entity =>
            {
                entity.ToTable("JobApplied");

                entity.Property(e => e.JobAppliedId).HasColumnName("jobAppliedId");

                entity.Property(e => e.JobAppliedApplier).HasColumnName("jobAppliedApplier");

                entity.Property(e => e.JobAppliedDate)
                    .HasColumnType("date")
                    .HasColumnName("jobAppliedDate");

                entity.Property(e => e.JobAppliedJob).HasColumnName("jobAppliedJob");

                entity.HasOne(d => d.JobAppliedApplierNavigation)
                    .WithMany(p => p.JobApplieds)
                    .HasForeignKey(d => d.JobAppliedApplier)
                    .HasConstraintName("FKJobApplied_JobApplier");

                entity.HasOne(d => d.JobAppliedJobNavigation)
                    .WithMany(p => p.JobApplieds)
                    .HasForeignKey(d => d.JobAppliedJob)
                    .HasConstraintName("FKJobApplied_Job");
            });

            modelBuilder.Entity<JobApplier>(entity =>
            {
                entity.ToTable("JobApplier");

                entity.Property(e => e.JobApplierId).HasColumnName("jobApplierId");

                entity.Property(e => e.JobApplierAddress).HasColumnName("jobApplierAddress");

                entity.Property(e => e.JobApplierBeInformed).HasColumnName("jobApplierBeInformed");

                entity.Property(e => e.JobApplierCv).HasColumnName("jobApplierCV");

                entity.Property(e => e.JobApplierEmail)
                    .HasMaxLength(255)
                    .HasColumnName("jobApplierEmail");

                entity.Property(e => e.JobApplierExperience).HasColumnName("jobApplierExperience");

                entity.Property(e => e.JobApplierFullname)
                    .HasMaxLength(255)
                    .HasColumnName("jobApplierFullname");

                entity.Property(e => e.JobApplierPhoneNumber)
                    .HasMaxLength(11)
                    .HasColumnName("jobApplierPhoneNumber");

                entity.Property(e => e.JobApplierPresentCompany)
                    .HasMaxLength(255)
                    .HasColumnName("jobApplierPresentCompany");
            });

            modelBuilder.Entity<JobDescription>(entity =>
            {
                entity.HasKey(e => e.Jdid)
                    .HasName("PK__JobDescr__00350037BDB87608");

                entity.ToTable("JobDescription");

                entity.Property(e => e.Jdid).HasColumnName("JDId");

                entity.Property(e => e.Jdjob).HasColumnName("JDJob");

                entity.Property(e => e.JdjobDescription).HasColumnName("JDJobDescription");

                entity.Property(e => e.JdresonToJoinJob).HasColumnName("JDResonToJoinJob");

                entity.Property(e => e.JdresonToLoveJob).HasColumnName("JDResonToLoveJob");

                entity.Property(e => e.JdskillsAndExperienceRequired).HasColumnName("JDSkillsAndExperienceRequired");

                entity.HasOne(d => d.JdjobNavigation)
                    .WithMany(p => p.JobDescriptions)
                    .HasForeignKey(d => d.Jdjob)
                    .HasConstraintName("FkJobDescription_JDJob");
            });

            modelBuilder.Entity<JobType>(entity =>
            {
                entity.ToTable("JobType");

                entity.Property(e => e.JobTypeId).HasColumnName("jobTypeId");

                entity.Property(e => e.JobTypeName)
                    .HasMaxLength(255)
                    .HasColumnName("jobTypeName");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.UserCompanyName)
                    .HasMaxLength(255)
                    .HasColumnName("userCompanyName");

                entity.Property(e => e.UserEmail)
                    .HasMaxLength(255)
                    .HasColumnName("userEmail");

                entity.Property(e => e.UserFullname)
                    .HasMaxLength(255)
                    .HasColumnName("userFullname");

                entity.Property(e => e.UserPhoneNumber)
                    .HasMaxLength(11)
                    .HasColumnName("userPhoneNumber");

                entity.Property(e => e.UserQuestion).HasColumnName("userQuestion");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
