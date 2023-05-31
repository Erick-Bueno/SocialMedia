public interface IRequestService
{
    public RequestsModel converteToRequestModel(RequestDto requestDto);
    public Task<bool> addRequest(RequestDto requestDto);
    public RequestsModel findRequest(Guid Receiver_id, Guid Requester_id);
}