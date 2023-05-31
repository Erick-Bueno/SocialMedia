public class RequestService : IRequestService
{
    private readonly IRequestRepository requestRepository;

    public RequestService(IRequestRepository requestRepository)
    {
        this.requestRepository = requestRepository;
    }

    public async Task<bool> addRequest(RequestDto requestDto)
    {
        var requestExists = requestRepository.findRequest(requestDto.receiverId, requestDto.requesterId);
        if(requestExists != null){
           return false;
        }
        var requestModel = converteToRequestModel(requestDto);
        var addRequest = await requestRepository.createRequest(requestModel);
        return true;
    }

    public RequestsModel converteToRequestModel(RequestDto requestDto)
    {
        RequestsModel requests = new RequestsModel();
        requests.receiverId = requestDto.receiverId;
        requests.requesterId = requestDto.requesterId;
        requests.status = requestDto.status;

        return requests;
    }

    public  RequestsModel findRequest(Guid receiverId, Guid requesterId)
    {
        var requestData = requestRepository.findRequest(receiverId, requesterId);
        return requestData;
    }
}