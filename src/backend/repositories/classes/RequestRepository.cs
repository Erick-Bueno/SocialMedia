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

    public  RequestsModel findRequest(Guid Receiver_id, Guid Requester_id)
    {
        var findedRequestOrNot = _context.Requests.Where(r => r.receiverId == Receiver_id && r.requesterId == Requester_id).FirstOrDefault();
        return findedRequestOrNot;
    }
}