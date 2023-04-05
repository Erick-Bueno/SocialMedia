using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CommentMap : IEntityTypeConfiguration<CommentModel>
{
    public void Configure(EntityTypeBuilder<CommentModel> builder)
    {
        builder.HasKey(c => c.id);

        builder.HasOne(u => u.userModel)
            .WithMany(c => c.userComments)
            .HasForeignKey(c=> c.User_id);
        builder.HasOne(u => u.postModel)
            .WithMany(c => c.commentsPost)
            .HasForeignKey(c => c.Posts_id);
    }
}