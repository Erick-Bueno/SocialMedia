using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

public class RequestRepositoryTest
{
    [Fact]
   async public void should_return_request_data_when_is_created()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName:"teste").Options;

        var appDbContextMock = new Mock<AppDbContext>(options);

        RequestsModel requests = new RequestsModel();

        requests.id = Guid.NewGuid();
        requests.receiverId = Guid.NewGuid();
        requests.requesterId= Guid.NewGuid();
        requests.RequestDate = DateTime.UtcNow;
        requests.status = StatusEnum.accepted;


        appDbContextMock.Setup(db => db.Requests.AddAsync(It.IsAny<RequestsModel>(),CancellationToken.None)).ReturnsAsync(appDbContextMock.Object.Entry(requests));

        var requestRepository = new RequestRepository(appDbContextMock.Object);

        var result = await requestRepository.createRequest(requests);

        Assert.Equal(requests, result);
    }
    [Fact]
    public void should_to_find_and_return_requests_according_to_requesterid_and_receiverid()
    {
        RequestsModel requests = new RequestsModel();
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName:"teste").Options;

        var appDbContextMock = new Mock<AppDbContext>(options);

        var requestDbSetMock = new Mock<DbSet<RequestsModel>>();
    

        requests.id = Guid.NewGuid();
        requests.receiverId = Guid.NewGuid();
        requests.requesterId = Guid.NewGuid();
        requests.RequestDate = DateTime.UtcNow;
        requests.status = StatusEnum.accepted;

        var list = new List<RequestsModel>{requests}.AsQueryable();

        requestDbSetMock.As<IQueryable<RequestsModel>>().Setup(x => x.Provider).Returns(list.AsQueryable().Provider);
        requestDbSetMock.As<IQueryable<RequestsModel>>().Setup(x => x.Expression).Returns(list.AsQueryable().Expression);
        requestDbSetMock.As<IQueryable<RequestsModel>>().Setup(x => x.ElementType).Returns(list.AsQueryable().ElementType);
        requestDbSetMock.As<IQueryable<RequestsModel>>().Setup(x => x.GetEnumerator()).Returns(list.AsQueryable().GetEnumerator());

        appDbContextMock.Setup(db => db.Requests).Returns(requestDbSetMock.Object);

        var RequestRepository = new RequestRepository(appDbContextMock.Object);

        var result = RequestRepository.findRequest(requests.receiverId,requests.requesterId);
        Assert.Equal(result, requests);
    }

}