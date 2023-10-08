
using backend.Migrations;

public class RequestRepository : IRequestRepository
{
    private readonly AppDbContext _context;

    public RequestRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<RequestsModel> createRequest(RequestsModel request)
    {
        var addingRequest = await _context.Requests.AddAsync(request);

        return request;
    }

    public RequestsModel findRequest(Guid Receiver_id, Guid Requester_id)
    {
        var findedRequestOrNot = _context.Requests.Where(r => r.receiverId == Receiver_id && r.requesterId == Requester_id).FirstOrDefault();
        return findedRequestOrNot;
    }

    public List<RequestsListLinq> listRequest(Guid userId)
    {
        var listRequest = (from requests in _context.Requests
                           join user in _context.Users on requests.requesterId equals user.id
                           where requests.receiverId == userId
                           select new RequestsListLinq
                           {
                               id = user.id,
                               userName = user.userName,
                               urlPhoto = user.userPhoto
                           }).ToList();

        return listRequest;
    }
    public bool deleteRequest(Guid receiverId, Guid requesterId)
    {
            var requestRecord = _context.Requests.Where(r => r.requesterId == requesterId && r.receiverId == receiverId).FirstOrDefault();
            _context.Requests.Remove(requestRecord);
            _context.SaveChanges();
            return true;

    }
}