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
}