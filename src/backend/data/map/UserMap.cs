using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserMap : IEntityTypeConfiguration<UserModel>
{
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
        builder.HasKey(u => u.id);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(200);
        builder.Property(u => u.Password).IsRequired().HasMaxLength(600);
        builder.Property(u => u.Telephone).IsRequired();
        builder.Property(u=>u.User_Photo).HasMaxLength(600);
        builder.Property(u=>u.UserName).IsRequired().HasMaxLength(200);
        builder.HasMany(u => u.posts)
        .WithOne(p => p.user)
        .HasForeignKey(p=> p.User_id);
        builder.HasMany(u => u.userlikes)
            .WithOne(l => l.userModel)
            .HasForeignKey(l => l.Users_id);

        builder.HasMany(f => f.UsersFriends).
            WithOne(u => u.userModel)
            .HasForeignKey(f => f.user_id);

        builder.HasMany(f => f.UsersFriends2)
            .WithOne(u => u.userModel2)
            .HasForeignKey(f => f.user_id_2);

        builder.HasMany(r => r.usersRequests)
            .WithOne(u => u.userModel)
            .HasForeignKey( u => u.Requester_id);
        builder.HasMany(r => r.usersRequests2)
            .WithOne(u => u.UserModel2)
            .HasForeignKey(u => u.Receiver_id);    
    }
}