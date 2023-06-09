public class PostsLinq
{
    public string contentPost { get; set; }
    public Guid postId { get; set; }
    public List<string> postImages { get; set; }
    public DateTime postDate { get; set; }
    public int totalComments { get; set; }
    public int totalLikes { get; set; }
    public string userName { get; set; }
    public string? userPhoto { get; set; }
}