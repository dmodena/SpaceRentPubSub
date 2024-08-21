using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using SpaceRent.Entities;
using SpaceRent.Repositories;

namespace SpaceRentTests.Endpoints;

public class BookingRequestsTests
{
    private readonly BookingRequest _bookingRequest;
    private readonly Mock<IOwnerRepository> _ownerRepositoryMock;
    private readonly Mock<ITenantRepository> _tenantRepositoryMock;
    private readonly Mock<Booking> _bookingMock;
    private readonly WebApplicationFactory<Program> _factory;

    public BookingRequestsTests()
    {
        _ownerRepositoryMock = new Mock<IOwnerRepository>();
        _ownerRepositoryMock.Setup(repo => repo.GetByPropertyId(It.IsAny<int>()))
            .Returns(new Owner { Id = 0, FullName = "Jane O. Test" });

        _tenantRepositoryMock = new Mock<ITenantRepository>();
        _tenantRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>()))
            .Returns(new Tenant { Id = 0, FullName = "John T. Test" });

        _bookingMock = new Mock<Booking>();
        _bookingRequest = new BookingRequest(0, 0, DateTime.Now, DateTime.Now.AddDays(1));

        _factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddScoped(_ => _ownerRepositoryMock.Object);
                    services.AddScoped(_ => _tenantRepositoryMock.Object);
                    services.AddSingleton(_ => _bookingMock.Object);
                });
            });
    }

    [Fact]
    public async Task Post_ReturnsCreated()
    {
        var client = _factory.CreateClient();
        var requestBody = new StringContent(JsonConvert.SerializeObject(_bookingRequest), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/bookingrequests", requestBody);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task Post_CallsBookingSave()
    {
        var client = _factory.CreateClient();
        var requestBody = new StringContent(JsonConvert.SerializeObject(_bookingRequest), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/bookingrequests", requestBody);
        response.EnsureSuccessStatusCode();

        _bookingMock.Verify(b => b.Save(It.IsAny<BookingData>()), Times.Once);
    }
}
