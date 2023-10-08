using System.ComponentModel.DataAnnotations;

public class RequestService : IRequestService
{
    private readonly IRequestRepository requestRepository;
    private readonly IUserRepository userRepository;

    public RequestService(IRequestRepository requestRepository, IUserRepository userRepository)
    {
        this.requestRepository = requestRepository;
        this.userRepository = userRepository;
    }

    public async Task<bool> addRequest(RequestDto requestDto)
    {
        var userReceiverExists = await userRepository.findUser(requestDto.receiverId);
        var userRequesterExists = await userRepository.findUser(requestDto.requesterId);
        if (userReceiverExists == null || userRequesterExists == null)
        {
           return false;
        }
        var requestExists = requestRepository.findRequest(requestDto.receiverId, requestDto.requesterId);
        if (requestExists != null)
        {
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

    public bool deleteRequest(RequestDto requestDto)
    {
        var deleteRequest = requestRepository.deleteRequest(requestDto.receiverId, requestDto.requesterId);
        return deleteRequest;
    }

    public RequestsModel findRequest(Guid receiverId, Guid requesterId)
    {
        var requestData = requestRepository.findRequest(receiverId, requesterId);
        return requestData;
    }

    public List<RequestsListLinq> listRequests(Guid userId)
    {
        var listRequests = requestRepository.listRequest(userId);
        return listRequests;
    }
}