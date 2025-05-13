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
    public class ComplaintTypeConfiguration : IEntityTypeConfiguration<ComplaintType>
    {
        public void Configure(EntityTypeBuilder<ComplaintType> builder)
        {

            builder.HasKey(ct => ct.Id);

            builder.Property(ct => ct.TypeName)
                   .IsRequired()
                   .HasMaxLength(100);

            // تعيين العلاقة مع Department
            builder.HasOne(ct => ct.Department)
                   .WithMany(d => d.ComplaintTypes)
                   .HasForeignKey(ct => ct.DepartmentID)
                   .OnDelete(DeleteBehavior.Restrict);

            // تعيين العلاقة مع Complaints
            builder.HasMany(ct => ct.Complaints)
                   .WithOne(c => c.ComplaintType)
                   .HasForeignKey(c => c.ComplaintTypeID)
                   .OnDelete(DeleteBehavior.Restrict);
            //// تعيين العلاقة مع ComplaintType
            builder.HasMany(ct => ct.Workflows)
                    .WithOne(w => w.ComplaintType)
                    .HasForeignKey(w => w.ComplaintTypeID) // لازم تتفعل
                    .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
