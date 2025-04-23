using ComplaintSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Repo.Data.Configrations
{
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
             builder.Property(x => x.Value)
                        .IsRequired();

              builder.HasOne(x => x.Complaint)
                        .WithOne(c => c.Rating)
                        .HasForeignKey<Rating>(x => x.ComplaintId)
                        .OnDelete(DeleteBehavior.Cascade); 


             builder.HasOne(x => x.User)
                   .WithMany()
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
