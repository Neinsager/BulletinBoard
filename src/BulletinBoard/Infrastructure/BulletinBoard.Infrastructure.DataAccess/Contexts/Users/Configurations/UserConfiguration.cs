using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BulletinBoard.Domain.Users;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Users.Configurations
{
    /// <summary>
    /// Конфигурация отношения пользователя.
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedOnAdd();
            builder.Property(u => u.Name).IsRequired().HasMaxLength(50);
            builder.Property(u => u.Login).IsRequired().HasMaxLength(50);
            builder.Property(u => u.PasswordHash).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Role).IsRequired().HasMaxLength(25);

            builder.HasIndex(u => u.Login).IsUnique();

            builder.HasMany(u => u.Posts)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
            new User
            {
                Id = new Guid("62d5d65f-7fd5-414c-8606-2045360f66c2"),
                Name = "Admin",
                Login = "Admin",
                Email = "admin@admin.com",
                PasswordHash = "562061253624346052140999182121123971381626070134955611791154493616316819712631",
                PhoneNumber = "+12345678901",
                Role = "Admin"
            });
        }
    }
}
