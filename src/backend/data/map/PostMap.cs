using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PostMap : IEntityTypeConfiguration<PostModel>
{
    public void Configure(EntityTypeBuilder<PostModel> builder)
    {
        builder.HasKey(p => p.id);
        builder.Property(p => p.contentPost).HasColumnType("text");
        builder.Property(p => p.totalComments).HasDefaultValue(0);
        builder.Property(p => p.totalLikes).HasDefaultValue(0);

        builder.HasOne(p => p.user)
            .WithMany(u => u.posts)
            .HasForeignKey(p => p.userId);
        builder.HasMany(p => p.postsimages)
            .WithOne(pi => pi.posts)
            .HasForeignKey(pi => pi.postId);
        builder.HasMany(p => p.postLikes)
            .WithOne(l => l.postModel)
            .HasForeignKey(l => l.postId);
    }
}