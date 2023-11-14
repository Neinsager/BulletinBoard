using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BulletinBoard.Domain.Categories;
using BulletinBoard.Domain.Users;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Categories.Configurations
{
    /// <summary>
    /// Конфигурация категории.
    /// </summary>
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable(nameof(Category));

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.CategoryName).IsRequired().HasMaxLength(30);

            builder.HasOne(c => c.Parent)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Posts)
                .WithOne(a => a.Category)
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new Category
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    CategoryName = "Base",
                    ParentId = null
                });
        }
    }
}