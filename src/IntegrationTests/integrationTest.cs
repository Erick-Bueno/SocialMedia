





using System.Text;
using System.Text.Json;
using Microsoft.OpenApi.Expressions;

namespace IntegrationTests;

public class intregationTest
{
    [Fact]
    public async void should_to_return_unauthorized_for_find_user()
    {
        Environment.SetEnvironmentVariable("JWT_SECRET", "72ba27d1c9b3d61d75008987546a09c20bf732d1");
        var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();

        var response = await client.GetAsync("api/User/08db62dc-df52-403b-8de7-7bc6f2367e67");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);


    }
    [Fact]
    public async void should_to_return_unauthorized_for_create_like()
    {
        Environment.SetEnvironmentVariable("JWT_SECRET", "72ba27d1c9b3d61d75008987546a09c20bf732d1");
        var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();

        var body = new
        {
            userId = Guid.NewGuid(),
            postId = Guid.NewGuid()
        };
        var json = JsonSerializer.Serialize(body);

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("api/Like/", content);
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);


    }
    [Fact]
    public async void should_to_return_unauthorized_for_add_friend()
    {
        Environment.SetEnvironmentVariable("JWT_SECRET", "72ba27d1c9b3d61d75008987546a09c20bf732d1");
        var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();

        var body = new
        {
            requesterId = Guid.NewGuid(),
            receiverId = Guid.NewGuid()
        };
        var json = JsonSerializer.Serialize(body);

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("api/Friends", content);
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);


    }
    [Fact]
    public async void should_to_return_unauthorized_for_list_friends()
    {
        Environment.SetEnvironmentVariable("JWT_SECRET", "72ba27d1c9b3d61d75008987546a09c20bf732d1");
        var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();


        var response = await client.GetAsync("api/friends/08db62dc-df52-403b-8de7-7bc6f2367e67");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);


    }
    [Fact]
    public async void should_to_return_unauthorized_for_create_post()
    {

        Environment.SetEnvironmentVariable("JWT_SECRET", "72ba27d1c9b3d61d75008987546a09c20bf732d1");
        var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();



        var formdata = new MultipartFormDataContent();


        var response = await client.PostAsync("api/Post", formdata);
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);

    }
    [Fact]
    public async void should_to_return_unauthorized_for_list_posts_user_liked()
    {
        Environment.SetEnvironmentVariable("JWT_SECRET", "72ba27d1c9b3d61d75008987546a09c20bf732d1");
        var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();


        var response = await client.GetAsync("api/Post/08db62dc-df52-403b-8de7-7bc6f2367e67");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    [Fact]
    public async void should_to_return_unauthorized_for_list_posts_user_like_see_more()
    {
        Environment.SetEnvironmentVariable("JWT_SECRET", "72ba27d1c9b3d61d75008987546a09c20bf732d1");
        var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();


        var response = await client.GetAsync("api/Post/seemorelike/08db62dc-df52-403b-8de7-7bc6f2367e67");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    [Fact]
    public async void should_to_return_unauthorized_for_list_post_user_created()
    {
        Environment.SetEnvironmentVariable("JWT_SECRET", "72ba27d1c9b3d61d75008987546a09c20bf732d1");
        var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();


        var response = await client.GetAsync("api/Post/userposts/08db62dc-df52-403b-8de7-7bc6f2367e67");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    [Fact]
    public async void should_to_return_unauthorized_for_list_post_user_created_see_more()
    {
        Environment.SetEnvironmentVariable("JWT_SECRET", "72ba27d1c9b3d61d75008987546a09c20bf732d1");
        var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();

        var body = new
        {

        };

        var json = JsonSerializer.Serialize(body);

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("api/Post/userpostseemore/08db62dc-df52-403b-8de7-7bc6f2367e67", content);
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    [Fact]
    public async void should_to_return_unauthorized_for_create_comment()
    {
        Environment.SetEnvironmentVariable("JWT_SECRET", "72ba27d1c9b3d61d75008987546a09c20bf732d1");
        var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();

        var body = new
        {

        };

        var json = JsonSerializer.Serialize(body);

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("api/Comment", content);
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    
}