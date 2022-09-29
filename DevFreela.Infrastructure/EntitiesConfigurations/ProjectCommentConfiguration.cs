using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DevFreela.Infrastructure.EntitiesConfigurations
{
    public class ProjectCommentConfiguration : IEntityTypeConfiguration<ProjectComment>
    {
        public void Configure(EntityTypeBuilder<ProjectComment> builder)
        {
            builder
                .HasKey(pc => pc.Id);

            builder
                .HasOne(pc => pc.Project)
                .WithMany(p => p.Comments)
                .HasForeignKey(pc => pc.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(pc => pc.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(pc => pc.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
