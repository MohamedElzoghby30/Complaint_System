using ComplaintSystem.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;

namespace ComplaintSystem.Repo.Data.Configrations
{
    public class ComplaintConfiguration : IEntityTypeConfiguration<Complaint>

    {
        public void Configure(EntityTypeBuilder<Complaint> builder)
        {


            
            builder.HasKey(c => c.Id);

            builder
            .Property(t => t.Status)
                .HasConversion<string>();

           
            builder.Property(c => c.Status)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(c => c.Description)
                   .IsRequired()
                   .HasMaxLength(500);


            // تعيين العلاقة مع User (مقدم الشكوى)
            builder.HasOne(c => c.User)
                   .WithMany(u => u.Complaints)
                   .HasForeignKey(c => c.UserID)
                   .OnDelete(DeleteBehavior.Restrict);

            // تعيين العلاقة مع ComplaintType
            builder.HasOne(c => c.ComplaintType)
                   .WithMany(ct => ct.Complaints)
                   .HasForeignKey(c => c.ComplaintTypeID)
                   .OnDelete(DeleteBehavior.Restrict);

            // تعيين العلاقة مع Workflow (الخطوة الحالية)
            builder.HasOne(c => c.CurrentStep)
                   .WithMany(w => w.Complaints)
                   .HasForeignKey(c => c.CurrentStepID)
                   .OnDelete(DeleteBehavior.Restrict);

            // تعيين العلاقة مع User (المسند إليه)
            builder.HasOne(c => c.AssignedTo)
                   .WithMany(u => u.AssignedComplaints)
                   .HasForeignKey(c => c.AssignedToID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder
                   .HasMany(c => c.Attachments)
                      .WithOne(a => a.Complaint)
                   .HasForeignKey(a => a.ComplaintId)
                  .OnDelete(DeleteBehavior.Cascade);

           


        }
    }
}
