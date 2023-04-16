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
    public class PostMap : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder) 
        {
            builder.ToTable("Posts");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(p => p.ShortDescription)
                .IsRequired()
                .HasMaxLength(5000);

            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(5000);

            builder.Property(p => p.ImageUrl)
                .HasMaxLength(500);

            builder.Property(p => p.UrlSlug)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(p => p.Metadata)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(p => p.CreateDate)
                .HasColumnType("datetime");

            builder.Property(p => p.UpdateDate)
                .HasColumnType("datetime");

            builder.Property(p => p.Published)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(p => p.ViewCount)
                .IsRequired()
                .HasDefaultValue(0);

            builder.HasOne(a => a.Author)
                .WithMany(p => p.Posts)
                .HasForeignKey(p => p.AuthorId)
                .HasConstraintName("FK_Posts_Authors")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Category)
                .WithMany(p => p.Posts)
                .HasForeignKey(p => p.CategoryId)
                .HasConstraintName("FK_Posts_Categories")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
