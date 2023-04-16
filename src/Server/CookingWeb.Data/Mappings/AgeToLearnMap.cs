using CookingWeb.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingWeb.Data.Mappings
{
    public class AgeToLearnMap : IEntityTypeConfiguration<AgeToLearn>
    {
        public void Configure(EntityTypeBuilder<AgeToLearn> builder)
        {
            builder.ToTable("AgeToLearns");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}
