
using Microsoft.Extensions.Logging.Abstractions;
using PentiaCase.Web.DTOs;
using PentiaCase.Web.Mappings;
using PentiaCase.Web.Services;
using PentiaCase.Web.Services.Interfaces;

namespace PentiaCase.Tests;

public class SalesPersonServiceTests
{
    [Fact]
    public async Task GetSalesPersonsAsync_WhenNoOrders_ReturnsSalesPersonWithEmptyOrders()
    {
        // Arrange
        var apiClientMock = new Mock<IWingineersApiClient>();

        apiClientMock
            .Setup(client => client.GetSalesPersonsAsync(
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<SalesPersonDto>
            {
                new SalesPersonDto
                {
                    Id = 1,
                    Name = "John"
                }
            });

        apiClientMock
            .Setup(client => client.GetOrderLinesAsync(
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<OrderLineDto>());

        var mapperConfig = new MapperConfiguration(
            config => config.AddProfile<MappingProfile>(), NullLoggerFactory.Instance);

        var mapper = mapperConfig.CreateMapper();

        var service = new SalesPersonService(
            apiClientMock.Object,
            mapper);

        // Act
        var result = await service.GetSalesPersonsAsync();

        // Assert
        var person = Assert.Single(result);

        Assert.Equal("John", person.Name);
        Assert.Empty(person.Orders);
    }
}
