using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class FriendsMap : IEntityTypeConfiguration<FriendsModel>
{
    public void Configure(EntityTypeBuilder<FriendsModel> builder)
    {
        builder.HasKey(f => f.id);
       
        builder.HasOne(u => u.userModel)
        .WithMany( u=> u.UsersFriends)
        .HasForeignKey(f => f.user_id);

        builder.HasOne(u => u.userModel2)
        .WithMany( u=> u.UsersFriends2)
        .HasForeignKey(f => f.user_id_2);
    }
}