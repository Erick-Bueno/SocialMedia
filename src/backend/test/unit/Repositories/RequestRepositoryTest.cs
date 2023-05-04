using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

public class RequestRepositoryTest
{
    [Fact]
   async public void Request_create_return_Test()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName:"teste").Options;

        var AppDbContextMock = new Mock<AppDbContext>(options);

        RequestsModel requests = new RequestsModel();

        requests.id = Guid.NewGuid();
        requests.Receiver_id = Guid.NewGuid();
        requests.Requester_id = Guid.NewGuid();
        requests.RequestDate = DateTime.UtcNow;
        requests.status = StatusEnum.accepted;


        AppDbContextMock.Setup(db => db.Requests.AddAsync(It.IsAny<RequestsModel>(),CancellationToken.None)).ReturnsAsync(AppDbContextMock.Object.Entry(requests));

        var requestRepository = new RequestRepository(AppDbContextMock.Object);

        var result = await requestRepository.createRequest(requests);

        Assert.Equal(requests, result);
    }
    [Fact]
    public void Find_requests_return_Test()
    {
        RequestsModel requests = new RequestsModel();
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName:"teste").Options;

        var AppDbContextMock = new Mock<AppDbContext>(options);

        var RequestDbSetMock = new Mock<DbSet<RequestsModel>>();
    

        requests.id = Guid.NewGuid();
        requests.Receiver_id = Guid.NewGuid();
        requests.Requester_id = Guid.NewGuid();
        requests.RequestDate = DateTime.UtcNow;
        requests.status = StatusEnum.accepted;

        var list = new List<RequestsModel>{requests}.AsQueryable();

        RequestDbSetMock.As<IQueryable<RequestsModel>>().Setup(x => x.Provider).Returns(list.AsQueryable().Provider);
        RequestDbSetMock.As<IQueryable<RequestsModel>>().Setup(x => x.Expression).Returns(list.AsQueryable().Expression);
        RequestDbSetMock.As<IQueryable<RequestsModel>>().Setup(x => x.ElementType).Returns(list.AsQueryable().ElementType);
        RequestDbSetMock.As<IQueryable<RequestsModel>>().Setup(x => x.GetEnumerator()).Returns(list.AsQueryable().GetEnumerator());

        AppDbContextMock.Setup(db => db.Requests).Returns(RequestDbSetMock.Object);

        var RequestRepository = new RequestRepository(AppDbContextMock.Object);

        var result = RequestRepository.FindRequest(requests.Receiver_id,requests.Requester_id);
        Assert.Equal(result, requests);
    }

    [Fact]
    public void Exception_request_exists_Test()
    {
       
    }

}