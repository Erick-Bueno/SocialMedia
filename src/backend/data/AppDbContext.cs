using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    public virtual DbSet<PostModel> Posts { get; set; }
    public virtual DbSet<UserModel> Users { get; set; }
    public virtual DbSet<PostImagesModel> Posts_images { get; set; }
    public virtual DbSet<LikesModel> Likes { get; set; }
    public virtual DbSet<CommentModel> Comments { get; set; }
    public virtual DbSet<FriendsModel> Friends { get; set; }
    public virtual DbSet<RequestsModel> Requests { get; set; }
    public virtual DbSet<TokenModel> Token { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PostMap());
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new PostImagesMap());
        modelBuilder.ApplyConfiguration(new CommentMap());
        modelBuilder.ApplyConfiguration(new FriendsMap());
        modelBuilder.ApplyConfiguration(new RequestsMap());
        modelBuilder.ApplyConfiguration(new TokenMap());
        modelBuilder.ApplyConfiguration(new LikesMap());
        base.OnModelCreating(modelBuilder);
    }
}