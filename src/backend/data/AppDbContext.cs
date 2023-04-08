using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<PostModel> Posts {get; set;}
    public DbSet<UserModel> Users {get;set;}
    public DbSet<PostImagesModel> Posts_images {get; set;}
    public DbSet<LikesModel> Likes {get; set;}
    public DbSet<CommentModel> Comments {get;set;}
    public DbSet<FriendsModel> Friends {get; set;}
    public DbSet<RequestsModel> Requests {get; set;}
    public DbSet<TokenModel> Token {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        modelBuilder.ApplyConfiguration(new PostMap());
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new PostImagesMap());
        modelBuilder.ApplyConfiguration(new CommentMap());
        modelBuilder.ApplyConfiguration(new FriendsMap());
        modelBuilder.ApplyConfiguration(new RequestsMap());
        modelBuilder.ApplyConfiguration(new TokenMap());
        base.OnModelCreating(modelBuilder);
    }
}