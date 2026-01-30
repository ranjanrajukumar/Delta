using Delta.API.Controllers.Common;
using Delta.Application.DTOs.Common;
using Delta.Application.Interfaces.Common;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Delta.Tests.Controllers.Common
{
    public class CommonSearchControllerTests
    {
        private readonly Mock<ICommonSearchService> _mockService;
        private readonly CommonSearchController _controller;

        public CommonSearchControllerTests()
        {
            _mockService = new Mock<ICommonSearchService>();
            _controller = new CommonSearchController(_mockService.Object);
        }

        [Fact]
        public async Task Search_ValidParameters_ReturnsOkResult()
        {
            // Arrange
            var expectedResponse = new CommonSearchResponseDto
            {
                DisplayName = "Test Display",
                Headers = new List<string> { "Id", "Name" },
                Data = new List<CommonSearchRowDto>
                {
                    new CommonSearchRowDto
                    {
                        Id = 1,
                        Columns = new Dictionary<string, string> { { "Id", "1" }, { "Name", "Test" } }
                    }
                }
            };

            _mockService.Setup(s => s.SearchAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.Search("users", "id", "id,name", "Users", "test");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<CommonSearchResponseDto>(okResult.Value);
            Assert.Equal(expectedResponse.DisplayName, response.DisplayName);
            Assert.Equal(expectedResponse.Headers.Count, response.Headers.Count);
            Assert.Equal(expectedResponse.Data.Count, response.Data.Count);
        }

        [Fact]
        public async Task Search_WithAllParameters_CallsServiceWithCorrectParameters()
        {
            // Arrange
            var tableName = "users";
            var columnId = "id";
            var displayColumns = "id,name,email";
            var displayName = "User List";
            var searchTerm = "john";
            var otherCondition = "status = 'active'";
            var sortBy = "name ASC";

            _mockService.Setup(s => s.SearchAsync(
                tableName, columnId, displayColumns, displayName, searchTerm, otherCondition, sortBy))
                .ReturnsAsync(new CommonSearchResponseDto());

            // Act
            await _controller.Search(tableName, columnId, displayColumns, displayName, searchTerm, otherCondition, sortBy);

            // Assert
            _mockService.Verify(s => s.SearchAsync(
                tableName, columnId, displayColumns, displayName, searchTerm, otherCondition, sortBy), 
                Times.Once);
        }

        [Fact]
        public async Task Search_WithMinimalParameters_CallsServiceWithDefaults()
        {
            // Arrange
            var tableName = "products";
            var columnId = "product_id";
            var displayColumns = "product_id,name";
            var displayName = "Products";

            _mockService.Setup(s => s.SearchAsync(
                tableName, columnId, displayColumns, displayName, "", null, null))
                .ReturnsAsync(new CommonSearchResponseDto());

            // Act
            await _controller.Search(tableName, columnId, displayColumns, displayName);

            // Assert
            _mockService.Verify(s => s.SearchAsync(
                tableName, columnId, displayColumns, displayName, "", null, null), 
                Times.Once);
        }

        [Fact]
        public async Task Search_ServiceThrowsException_PropagatesException()
        {
            // Arrange
            _mockService.Setup(s => s.SearchAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .ThrowsAsync(new InvalidOperationException("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _controller.Search("users", "id", "id,name", "Users", "test"));
        }

        [Fact]
        public async Task Search_EmptyResponse_ReturnsOkWithEmptyData()
        {
            // Arrange
            var emptyResponse = new CommonSearchResponseDto
            {
                DisplayName = "Empty Results",
                Headers = new List<string>(),
                Data = new List<CommonSearchRowDto>()
            };

            _mockService.Setup(s => s.SearchAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .ReturnsAsync(emptyResponse);

            // Act
            var result = await _controller.Search("users", "id", "id,name", "Users", "nonexistent");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<CommonSearchResponseDto>(okResult.Value);
            Assert.Empty(response.Headers);
            Assert.Empty(response.Data);
        }
    }
}