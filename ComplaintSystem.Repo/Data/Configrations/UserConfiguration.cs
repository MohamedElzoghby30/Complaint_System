using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplaintSystem.Core.Entities;

namespace ComplaintSystem.Repo.Data.Configrations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {


            // تعيين المفتاح الأساسي
            builder.HasKey(u => u.Id);

            // تعيين العلاقة مع Department
            builder.HasOne(u => u.Department)
                   .WithMany(d => d.Users)
                   .HasForeignKey(u => u.DepartmentID)
                   .OnDelete(DeleteBehavior.Restrict);

            // تعيين العلاقة مع Complaints
            builder.HasMany(u => u.Complaints)
                   .WithOne(c => c.User)
                   .HasForeignKey(c => c.UserID)
                   .OnDelete(DeleteBehavior.Restrict);

            // تعيين العلاقة مع AssignedComplaints
            builder.HasMany(u => u.AssignedComplaints)
                   .WithOne(c => c.AssignedTo)
                   .HasForeignKey(c => c.AssignedToID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
