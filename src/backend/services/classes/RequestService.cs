public class RequestService : IRequestService
{
    private readonly IRequestRepository requestRepository;

    public RequestService(IRequestRepository requestRepository)
    {
        this.requestRepository = requestRepository;
    }

    public async Task<bool> addRequest(RequestDto requestDto)
    {
        var requestExists = requestRepository.FindRequest(requestDto.Receiver_id, requestDto.Requester_id);
        if(requestExists != null){
           return false;
        }
        var requestmodel = ConverteToRequestModel(requestDto);
        var addRequest = await requestRepository.createRequest(requestmodel);
        return true;
    }

    public RequestsModel ConverteToRequestModel(RequestDto requestDto)
    {
        RequestsModel requests = new RequestsModel();
        requests.Receiver_id = requestDto.Receiver_id;
        requests.Requester_id = requestDto.Requester_id;
        requests.status = requestDto.status;

        return requests;
    }

    public  RequestsModel FindRequest(Guid Receiver_id, Guid Requester_id)
    {
        var RequestData = requestRepository.FindRequest(Receiver_id, Requester_id);
        return RequestData;
    }
}