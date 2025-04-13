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
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {

            builder.HasKey(d => d.Id);

            builder.Property(d => d.DepartmentName)
                   .IsRequired()
                   .HasMaxLength(100);

            // تعيين العلاقة مع Users
            builder.HasMany(d => d.Users)
                   .WithOne(u => u.Department)
                   .HasForeignKey(u => u.DepartmentID)
                   .OnDelete(DeleteBehavior.Restrict);

            // تعيين العلاقة مع ComplaintTypes
            builder.HasMany(d => d.ComplaintTypes)
                   .WithOne(ct => ct.Department)
                   .HasForeignKey(ct => ct.DepartmentID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
