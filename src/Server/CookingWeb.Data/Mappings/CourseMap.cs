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

            builder.Property(c => c.Price)
                .HasMaxLength(200);

            builder.Property(c => c.NumberOfSessions)
                .HasMaxLength(200);

            builder.Property(c => c.Published)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(c => c.RegisterCount)
                .HasDefaultValue(0);

            builder.HasOne(d => d.Demand)
                .WithMany(c => c.Courses)
                .HasForeignKey(t => t.DemandId)
                .HasConstraintName("FK_Courses_Demands")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Price)
                .WithMany(c => c.Courses)
                .HasForeignKey(t => t.PriceId)
                .HasConstraintName("FK_Courses_Prices")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(n => n.NumberOfSessions)
                .WithMany(c => c.Courses)
                .HasForeignKey(t => t.NumberOfSessionsId)
                .HasConstraintName("FK_Courses_NumberOfSessions")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Chef)
                .WithMany(c => c.Courses)
                .HasForeignKey(t => t.ChefId)
                .HasConstraintName("FK_Courses_Chefs")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
