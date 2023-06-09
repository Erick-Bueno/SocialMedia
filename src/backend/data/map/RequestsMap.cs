using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RequestsMap : IEntityTypeConfiguration<RequestsModel>
{
    public void Configure(EntityTypeBuilder<RequestsModel> builder)
    {
        builder.HasKey(r => r.id);
        builder.Property(r => r.status).IsRequired();
        builder.HasOne(u => u.userModel)
            .WithMany(r => r.usersRequests)
            .HasForeignKey(r => r.requesterId);
        builder.HasOne(u => u.UserModel2)
            .WithMany(r => r.usersRequests2)
            .HasForeignKey(r => r.receiverId);
    }
}