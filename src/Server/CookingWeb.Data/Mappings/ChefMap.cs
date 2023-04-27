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
    public class ChefMap : IEntityTypeConfiguration<Chef>
    {
        public void Configure(EntityTypeBuilder<Chef> builder) 
        {
            builder.ToTable("Chefs");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.FullName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Description)
                .HasMaxLength(200);

            builder.Property(c => c.UrlSlug)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.ImageUrl)
                .HasMaxLength(500);

            builder.Property(c => c.JoinedDate)
                .HasColumnType("datetime");
        }
    }
}
