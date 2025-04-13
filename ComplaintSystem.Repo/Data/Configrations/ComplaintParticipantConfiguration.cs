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
    public class ComplaintParticipantConfiguration : IEntityTypeConfiguration<ComplaintParticipant>
    {
        public void Configure(EntityTypeBuilder<ComplaintParticipant> builder)
        {
            // العلاقة مع Complaint
            builder.HasOne(p => p.Complaint)
                   .WithMany(c => c.Participants)
                   .HasForeignKey(p => p.ComplaintID)
                   .OnDelete(DeleteBehavior.Restrict);

            // العلاقة مع Workflow
            builder.HasOne(p => p.Workflow)
                   .WithMany()
                   .HasForeignKey(p => p.WorkflowID)
                   .OnDelete(DeleteBehavior.Restrict);

            // العلاقة مع User
            builder.HasOne(p => p.User)
                   .WithMany()
                   .HasForeignKey(p => p.UserID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
