using Microsoft.AspNetCore.Mvc;
using RequestController.Controllers;
using Moq;
using Xunit;

public class RequestControllerTest
{
  [Fact]
  public void should_to_return_list_requests()
  {
    var requestService = new Mock<IRequestService>();
    var requestController = new Request(requestService.Object);
    var userModelTest = new UserModel();
    var requestsList = new List<RequestsListLinq>();
    requestService.Setup(rs => rs.listRequests(userModelTest.id)).Returns(requestsList);

    var result = requestController.listRequests(userModelTest.id);

    var OkObjectResult = Assert.IsType<ActionResult<List<RequestsModel>>>(result);

    var content = Assert.IsType<OkObjectResult>(OkObjectResult.Result);

    Assert.Equal(content.Value, requestsList);

  }
  [Fact]
  public void should_to_return_bool_when_request_deleted()
  {
    var requestService = new Mock<IRequestService>();
    var requestController = new Request(requestService.Object);

    var requestDto = new RequestDto();

    requestService.Setup(rs => rs.deleteRequest(requestDto)).Returns(true);
    var result = requestController.deleteRequest(requestDto);
    var OkObjectResult = Assert.IsType<ActionResult<Boolean>>(result);

    var content = Assert.IsType<OkObjectResult>(OkObjectResult.Result);

    Assert.Equal(content.Value, true);
  }
}