using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class LikesMap : IEntityTypeConfiguration<LikesModel>
{
    public void Configure(EntityTypeBuilder<LikesModel> builder)
    {
        builder.HasKey(l => l.id);
       
        builder.HasOne(p =>p.postModel)
            .WithMany(l =>l.postLikes)
            .HasForeignKey(l => l.Posts_id);

        builder.HasOne(u=> u.userModel)
            .WithMany(l => l.userlikes)
            .HasForeignKey(l => l.Users_id);
    }
}