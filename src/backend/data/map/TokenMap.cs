using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TokenMap : IEntityTypeConfiguration<TokenModel>
{
    public void Configure(EntityTypeBuilder<TokenModel> builder)
    {
        builder.HasKey(t => t.id);
        builder.Property(t => t.Email).IsRequired().HasMaxLength(200);
        builder.Property(t => t.jwt).IsRequired().HasMaxLength(500);
    }
}