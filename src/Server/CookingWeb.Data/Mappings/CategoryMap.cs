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
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Description)
                .HasMaxLength(500);

            builder.Property(c => c.UrlSlug)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.ShowOnMenu)
                .IsRequired()
                .HasDefaultValue(false);

            builder.HasOne(t => t.Topic)
                .WithMany(c => c.Categories)
                .HasForeignKey(p => p.TopicId)
                .HasConstraintName("FK_Categories_Topics")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
