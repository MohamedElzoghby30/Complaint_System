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
    public class CommentComplaineronConfigurtion : IEntityTypeConfiguration<CommentComplainer>
    {
        public void Configure(EntityTypeBuilder<CommentComplainer> builder)
        {
            builder.Property(com => com.CommentText)
                   .IsRequired();


            builder.Property(com => com.CreatedAt)
               .IsRequired();

            builder.HasOne(com => com.Complaint)
                   .WithMany(c => c.CommentsComplainer)
                   .HasForeignKey(com => com.ComplaintID)
                   .OnDelete(DeleteBehavior.Cascade); 

            builder.HasOne(com => com.User)
                   .WithMany(u => u.CommentsComplainer) 
                   .HasForeignKey(com => com.UserId)
                   .OnDelete(DeleteBehavior.Cascade); 
        }
    }
} 

