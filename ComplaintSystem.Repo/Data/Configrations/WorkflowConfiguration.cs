using ComplaintSystem.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ComplaintSystem.Repo.Data.Configrations
{
    public class WorkflowConfiguration : IEntityTypeConfiguration<Workflow>
    {
        public void Configure(EntityTypeBuilder<Workflow> builder)
        {

            builder.HasKey(w => w.Id);

            builder.Property(w => w.StepName)
                   .IsRequired()
                   .HasMaxLength(100);

          
            builder.HasOne(w => w.NextStep)
                   .WithMany()
                   .HasForeignKey(w => w.NextStepID)
                   .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(w => w.ComplaintType)
                   .WithMany(ct => ct.Workflows)
                   .HasForeignKey(w => w.ComplaintTypeID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(w => w.User)
                   .WithMany(u => u.Workflows)
                   .HasForeignKey(w => w.UserId)
                   .OnDelete(DeleteBehavior.SetNull);

            //// تعيين العلاقة مع Role
            //builder.HasOne(w => w.Role)
            //       .WithMany()
            //       .HasForeignKey(w => w.RoleID)
            //       .OnDelete(DeleteBehavior.Restrict);
            //علاقه مع Complaint
            builder.HasMany(w => w.Complaints)
                   .WithOne(c => c.CurrentStep)
                   .HasForeignKey(c => c.CurrentStepID)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
