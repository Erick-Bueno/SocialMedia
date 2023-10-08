public interface IRequestRepository
{
    public Task<RequestsModel> createRequest(RequestsModel request);
    public RequestsModel findRequest(Guid Receiver_id, Guid Requester_id);
    public List<RequestsListLinq> listRequest(Guid userId);
    public bool deleteRequest(Guid receiverId, Guid requesterId);
    
}