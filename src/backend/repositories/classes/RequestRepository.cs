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

    public  RequestsModel FindRequest(Guid Receiver_id, Guid Requester_id)
    {
        var FindedRequestOrNot = _context.Requests.Where(r => r.Receiver_id == Receiver_id && r.Requester_id == Requester_id).FirstOrDefault();
        return FindedRequestOrNot;
    }
}