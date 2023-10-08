using Castle.DynamicProxy.Generators;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

public class RequestRepositoryTest
{
    [Fact]
    async public void should_return_request_data_when_is_created()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "teste").Options;

        var appDbContextMock = new Mock<AppDbContext>(options);

        RequestsModel requests = new RequestsModel();

        requests.id = Guid.NewGuid();
        requests.receiverId = Guid.NewGuid();
        requests.requesterId = Guid.NewGuid();
        requests.RequestDate = DateTime.UtcNow;
        requests.status = StatusEnum.accepted;


        appDbContextMock.Setup(db => db.Requests.AddAsync(It.IsAny<RequestsModel>(), CancellationToken.None)).ReturnsAsync(appDbContextMock.Object.Entry(requests));

        var requestRepository = new RequestRepository(appDbContextMock.Object);

        var result = await requestRepository.createRequest(requests);

        Assert.Equal(requests, result);
    }
    [Fact]
    public void should_to_find_and_return_requests_according_to_requesterid_and_receiverid()
    {
        RequestsModel requests = new RequestsModel();
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "teste").Options;

        var appDbContextMock = new Mock<AppDbContext>(options);

        var requestDbSetMock = new Mock<DbSet<RequestsModel>>();


        requests.id = Guid.NewGuid();
        requests.receiverId = Guid.NewGuid();
        requests.requesterId = Guid.NewGuid();
        requests.RequestDate = DateTime.UtcNow;
        requests.status = StatusEnum.accepted;

        var list = new List<RequestsModel> { requests }.AsQueryable();

        requestDbSetMock.As<IQueryable<RequestsModel>>().Setup(x => x.Provider).Returns(list.AsQueryable().Provider);
        requestDbSetMock.As<IQueryable<RequestsModel>>().Setup(x => x.Expression).Returns(list.AsQueryable().Expression);
        requestDbSetMock.As<IQueryable<RequestsModel>>().Setup(x => x.ElementType).Returns(list.AsQueryable().ElementType);
        requestDbSetMock.As<IQueryable<RequestsModel>>().Setup(x => x.GetEnumerator()).Returns(list.AsQueryable().GetEnumerator());

        appDbContextMock.Setup(db => db.Requests).Returns(requestDbSetMock.Object);

        var RequestRepository = new RequestRepository(appDbContextMock.Object);

        var result = RequestRepository.findRequest(requests.receiverId, requests.requesterId);
        Assert.Equal(result, requests);
    }
    [Fact]
    public void should_to_list_user_requests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "teste").Options;

        var appDbContextMock = new Mock<AppDbContext>(options);

        var requestRepository = new RequestRepository(appDbContextMock.Object);

        var listUsers = new List<UserModel> { }.AsQueryable();
        var listRequest = new List<RequestsModel> { }.AsQueryable();

        var dbsetUsers = new Mock<DbSet<UserModel>>();
        var dbsetRequests = new Mock<DbSet<RequestsModel>>();

        dbsetRequests.As<IQueryable<RequestsModel>>().Setup(db => db.Provider).Returns(listRequest.Provider);
        dbsetRequests.As<IQueryable<RequestsModel>>().Setup(db => db.ElementType).Returns(listRequest.ElementType);
        dbsetRequests.As<IQueryable<RequestsModel>>().Setup(db => db.Expression).Returns(listRequest.Expression);
        dbsetRequests.As<IQueryable<RequestsModel>>().Setup(db => db.GetEnumerator()).Returns(listRequest.GetEnumerator());

        dbsetUsers.As<IQueryable<UserModel>>().Setup(db => db.Provider).Returns(listUsers.Provider);
        dbsetUsers.As<IQueryable<UserModel>>().Setup(db => db.ElementType).Returns(listUsers.ElementType);
        dbsetUsers.As<IQueryable<UserModel>>().Setup(db => db.Expression).Returns(listUsers.Expression);
        dbsetUsers.As<IQueryable<UserModel>>().Setup(db => db.GetEnumerator()).Returns(listUsers.GetEnumerator());

        appDbContextMock.Setup(db => db.Users).Returns(dbsetUsers.Object);
        appDbContextMock.Setup(db => db.Requests).Returns(dbsetRequests.Object);


        RequestsModel requests = new RequestsModel();
        requests.id = Guid.NewGuid();
        requests.receiverId = Guid.NewGuid();
        requests.requesterId = Guid.NewGuid();
        requests.RequestDate = DateTime.UtcNow;
        requests.status = StatusEnum.accepted;

        var result = requestRepository.listRequest(requests.receiverId);

        Assert.IsType<List<RequestsListLinq>>(result);

    }
    [Fact]
    public void should_to_delete_request()
    {
         var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "teste").Options;

        var appDbContextMock = new Mock<AppDbContext>(options);

        var requestRepository = new RequestRepository(appDbContextMock.Object);

        var listUsers = new List<UserModel> { }.AsQueryable();
        var listRequest = new List<RequestsModel> { }.AsQueryable();

        var dbsetUsers = new Mock<DbSet<UserModel>>();
        var dbsetRequests = new Mock<DbSet<RequestsModel>>();

        dbsetRequests.As<IQueryable<RequestsModel>>().Setup(db => db.Provider).Returns(listRequest.Provider);
        dbsetRequests.As<IQueryable<RequestsModel>>().Setup(db => db.ElementType).Returns(listRequest.ElementType);
        dbsetRequests.As<IQueryable<RequestsModel>>().Setup(db => db.Expression).Returns(listRequest.Expression);
        dbsetRequests.As<IQueryable<RequestsModel>>().Setup(db => db.GetEnumerator()).Returns(listRequest.GetEnumerator());

        appDbContextMock.Setup(db => db.Requests).Returns(dbsetRequests.Object);

        var friends = new FriendsDto();

        var result = requestRepository.deleteRequest(friends.receiverId, friends.requesterId);

        Assert.True(result);
    }
}