using System.ComponentModel.DataAnnotations;

public class LikeRepository : ILikeRepository
{
    private readonly AppDbContext _context;

    public LikeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LikesModel> createLike(LikesModel Like)
    {
          try
        {
            var newLike = await _context.Likes.AddAsync(Like);
            await _context.SaveChangesAsync();
            return Like;
        }
        catch (ValidationException ex)
        {
            throw new ValidationException(ex.Message);
        }

    }

    public LikesModel findLike(Guid userId, Guid postId)
    {
        var registrationLikesTable =  _context.Likes.Where(l => l.postId == postId && l.userId == userId).FirstOrDefault();
        return registrationLikesTable;
    }

    public async Task<bool> removeLike(LikesModel likes)
    {
        var registrationRemoved = _context.Likes.Remove(likes);
        await _context.SaveChangesAsync();
        return true;
    }
}