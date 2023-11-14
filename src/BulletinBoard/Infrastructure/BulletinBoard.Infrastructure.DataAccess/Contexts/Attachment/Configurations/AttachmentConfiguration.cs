using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BulletinBoard.Domain.Attachments;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Attachments.Configurations
{
    /// <summary>
    /// Конфигурация отношения Attachment.
    /// </summary>
    public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.ToTable(nameof(Attachment));

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id).ValueGeneratedOnAdd();
            builder.Property(a => a.Content).IsRequired();

            builder.HasOne(a => a.Post)
                .WithMany(ad => ad.Attachments)
                .HasForeignKey(a => a.PostId);
        }
    }
}
