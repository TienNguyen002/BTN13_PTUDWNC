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
    public class StudentMap : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.FullName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(s => s.Mobile)
                .IsRequired()
                .HasMaxLength(11);

            builder.Property(s => s.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.UrlSlug)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(s => s.RegisterDate)
                .HasColumnType("datetime");

            builder.Property(s => s.Notes)
                .HasMaxLength(500);

            builder.HasMany(c => c.Courses)
                .WithMany(s => s.Students)
                .UsingEntity(pt => pt.ToTable("CoursesStudents"));
        }
    }
}
