using ComplaintSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Repo.Data.Configrations
{
    public class ComplaintAttachmentConfiguration : IEntityTypeConfiguration<ComplaintAttachment>
    {
        public void Configure(EntityTypeBuilder<ComplaintAttachment> builder)
        {

            builder.HasKey(c => c.Id);


            builder.Property(ca => ca.FileUrl)
                   .IsRequired()
                   .HasMaxLength(500);

          
            builder.HasOne(ca => ca.Complaint)
                   .WithMany(c => c.Attachments)
                   .HasForeignKey(ca => ca.ComplaintId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
