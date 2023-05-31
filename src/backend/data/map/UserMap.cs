using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserMap : IEntityTypeConfiguration<UserModel>
{
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
        builder.HasKey(u => u.id);
        builder.Property(u => u.email).IsRequired().HasMaxLength(200);
        builder.Property(u => u.password).IsRequired().HasMaxLength(600);
        builder.Property(u => u.telephone).IsRequired();
        builder.Property(u => u.userPhoto).HasMaxLength(600);
        builder.Property(u => u.userName).IsRequired().HasMaxLength(200);
        builder.HasIndex(u => u.email);
        builder.HasMany(u => u.posts)
        .WithOne(p => p.user)
        .HasForeignKey(p => p.userId);
        builder.HasMany(u => u.userlikes)
            .WithOne(l => l.userModel)
            .HasForeignKey(l => l.userId);

        builder.HasMany(f => f.UsersFriends).
            WithOne(u => u.userModel)
            .HasForeignKey(f => f.userId);

        builder.HasMany(f => f.UsersFriends2)
            .WithOne(u => u.userModel2)
            .HasForeignKey(f => f.userId2);

        builder.HasMany(r => r.usersRequests)
            .WithOne(u => u.userModel)
            .HasForeignKey(u => u.requesterId);
        builder.HasMany(r => r.usersRequests2)
            .WithOne(u => u.UserModel2)
            .HasForeignKey(u => u.receiverId);
    }
}