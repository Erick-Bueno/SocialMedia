public interface IRequestService
{
    public RequestsModel converteToRequestModel(RequestDto requestDto);
    public Task<bool> addRequest(RequestDto requestDto);
    public RequestsModel findRequest(Guid Receiver_id, Guid Requester_id);
    public List<RequestsListLinq> listRequests (Guid userId);
    public bool deleteRequest(RequestDto requestDto);
}