public interface IRequestRepository
{
    public Task<RequestsModel> createRequest(RequestsModel request);
    public RequestsModel FindRequest(Guid Receiver_id, Guid Requester_id);
    
}