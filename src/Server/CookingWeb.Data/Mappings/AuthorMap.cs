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
    public class AuthorMap : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("Authors");
            
            builder.HasKey(a => a.Id);

            builder.Property(a => a.FullName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Description)
                .HasMaxLength(2000);

            builder.Property(a => a.UrlSlug)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.ImageUrl)
                .HasMaxLength(500);

            builder.Property(a => a.JoinedDate)
                .HasColumnType("datetime");
        }
    }
}
