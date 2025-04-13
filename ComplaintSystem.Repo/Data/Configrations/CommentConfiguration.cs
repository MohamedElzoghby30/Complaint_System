using ComplaintSystem.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Repo.Data.Configrations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(com => com.CommentText)
                   .IsRequired();

            // العلاقة مع Complaint
            builder.HasOne(com => com.Complaint)
                   .WithMany(c => c.Comments)
                   .HasForeignKey(com => com.ComplaintID)
                   .OnDelete(DeleteBehavior.Restrict);

            // العلاقة مع ComplaintParticipant
            builder.HasOne(com => com.Participant)
                   .WithMany()
                   .HasForeignKey(com => com.ParticipantID)
                   .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
