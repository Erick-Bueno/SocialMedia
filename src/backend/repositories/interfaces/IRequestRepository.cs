public interface IRequestRepository
{
    public Task<RequestsModel> createRequest(RequestsModel request);
    public RequestsModel findRequest(Guid Receiver_id, Guid Requester_id);
    
}