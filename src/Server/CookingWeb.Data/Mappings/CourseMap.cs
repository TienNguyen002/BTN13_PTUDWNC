using CookingWeb.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingWeb.Data.Mappings
{
    public class CourseMap : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(c => c.ShortDescription)
                .IsRequired()
                .HasMaxLength(5000);

            builder.Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(5000);

            builder.Property(c => c.ImageUrl)
                .HasMaxLength(500);

            builder.Property(c => c.UrlSlug)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(c => c.CreateDate)
                .HasColumnType("datetime");

            builder.Property(c => c.UpdateDate)
                .HasColumnType("datetime");

            builder.Property(c => c.AgeToLearn)
                .HasMaxLength(200);

            builder.Property(c => c.Price)
                .HasMaxLength(200);

            builder.Property(c => c.NumberOfSessions)
                .HasMaxLength(200);

            builder.Property(c => c.Published)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(c => c.RegisterCount)
                .HasDefaultValue(0);

            builder.HasOne(t => t.Demand)
                .WithMany(c => c.Courses)
                .HasForeignKey(t => t.DemandId)
                .HasConstraintName("FK_Courses_Demands")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Chefs)
                .WithMany(c => c.Courses)
                .UsingEntity(pt => pt.ToTable("CoursesChefs"));

            builder.HasMany(s => s.Students)
                .WithMany(c => c.Courses)
                .UsingEntity(pt => pt.ToTable("CoursesStudents"));

        }
    }
}
