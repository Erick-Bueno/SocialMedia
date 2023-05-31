using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CommentMap : IEntityTypeConfiguration<CommentModel>
{
    public void Configure(EntityTypeBuilder<CommentModel> builder)
    {
        builder.HasKey(c => c.id);
        builder.Property(c => c.comment).IsRequired().HasMaxLength(600);

        builder.HasOne(u => u.userModel)
            .WithMany(c => c.userComments)
            .HasForeignKey(c => c.userId);
        builder.HasOne(u => u.postModel)
            .WithMany(c => c.commentsPost)
            .HasForeignKey(c => c.postId);
    }
}