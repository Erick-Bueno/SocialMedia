public interface IRequestService
{
    public RequestsModel ConverteToRequestModel(RequestDto requestDto);
    public Task<bool> addRequest(RequestDto requestDto);
    public RequestsModel FindRequest(Guid Receiver_id, Guid Requester_id);
}