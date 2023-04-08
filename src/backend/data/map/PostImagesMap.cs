using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PostImagesMap : IEntityTypeConfiguration<PostImagesModel>
{
    public void Configure(EntityTypeBuilder<PostImagesModel> builder)
    {
       builder.HasKey(pi => pi.id);
       builder.Property(pi => pi.imgUrl).IsRequired().HasMaxLength(600);
      
       builder.HasOne(p => p.posts)
        .WithMany(pi => pi.postsimages)
        .HasForeignKey(pi => pi.posts_id);
    }
}