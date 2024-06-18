namespace AccessCodes.Api.Tests;

public class AccessCodeTests(AccessCodeApi factory)
    : IClassFixture<AccessCodeApi>
{
    [Fact]
    public async Task GetFromRoot()
    {
        var client = await factory.CreateClient();
        var response = await client.GetAsync("/");
        Assert.Equal("Hello World! 0", await response.Content.ReadAsStringAsync());
    }
}