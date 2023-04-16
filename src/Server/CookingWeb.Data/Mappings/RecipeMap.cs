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
    public class RecipeMap : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.ToTable("Recipes");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Title)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(r => r.ShortDesciption)
                .IsRequired()
                .HasMaxLength(5000);

            builder.Property(r => r.Description)
                .IsRequired()
                .HasMaxLength(5000);

            builder.Property(r => r.Metadata)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(r => r.ImageUrl)
                .HasMaxLength(500);

            builder.Property(r => r.UrlSlug)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(r => r.Ingredient)
                .IsRequired()
                .HasMaxLength(5000);

            builder.Property(r => r.Step)
                .IsRequired()
                .HasMaxLength(10000);

            builder.Property(r => r.CreateDate)
                .HasColumnType("datetime");

            builder.Property(r => r.UpdateDate)
                .HasColumnType("datetime");

            builder.Property(r => r.Published)
                .IsRequired()
                .HasDefaultValue(false); 

            builder.Property(r => r.ViewCount)
                .IsRequired()
                .HasDefaultValue(0);

            builder.HasOne(a => a.Author)
                .WithMany(r => r.Recipes)
                .HasForeignKey(r => r.AuthorId)
                .HasConstraintName("FK_Recipes_Authors")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.Course)
                .WithMany(r => r.Recipes)
                .HasForeignKey(r => r.CourseId)
                .HasConstraintName("FK_Recipes_Courses")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
