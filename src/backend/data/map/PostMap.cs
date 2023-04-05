using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PostMap : IEntityTypeConfiguration<PostModel>
{
    public void Configure(EntityTypeBuilder<PostModel> builder)
    {
        builder.HasKey(p => p.id);
        builder.Property(p=>p.ContentPost).IsRequired().HasMaxLength(600);
        
        builder.HasOne(p => p.user)
            .WithMany(u => u.posts)
            .HasForeignKey(p => p.User_id);
        builder.HasMany(p=> p.postsimages)
            .WithOne(pi => pi.posts)
            .HasForeignKey(pi => pi.posts_id);
        builder.HasMany(p => p.postLikes)
            .WithOne(l => l.postModel)
            .HasForeignKey(l => l.Posts_id);
    }
}