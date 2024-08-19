namespace ChallengeTests
{
    using Application.UseCases.OdersOperation.Commands.Create;
    using Domain.DTOs;
    using Domain.Enums;
    using Domain.Interfaces;
    using FluentValidation.Results;
    using Moq;
    using System;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class CreateOrderCommandHandlerTests
    {
        private readonly Mock<IOrderService> _orderServiceMock;
        private readonly Mock<IAssetService> _assetServiceMock;
        private readonly CreateOrderCommandHandler _handler;

        public CreateOrderCommandHandlerTests()
        {
            _orderServiceMock = new Mock<IOrderService>();
            _assetServiceMock = new Mock<IAssetService>();
            _handler = new CreateOrderCommandHandler(_orderServiceMock.Object, _assetServiceMock.Object);
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsCreatedResponse()
        {
            // Arrange
            var request = new CreateOrderCommandHandlerRequest
            {
                AssetId = 8,
                Amount = 10,
                Price = 0,
                Operation = 'C',
                AccountId = "123"
            };

            _assetServiceMock.Setup(x => x.GetAssetById(It.IsAny<int>()))
                .ReturnsAsync(new GetAssetDto { AssetTypeId = (int)AssetTypeEnum.Accion, Price = 100 });

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal((int)HttpStatusCode.Created, result.StatusCode);
            Assert.Equal("Orden creada exitosamente", result.Message);
        }

        [Fact]
        public async Task Handle_AssetNotFound_ReturnsNotFoundResponse()
        {
            // Arrange
            var request = new CreateOrderCommandHandlerRequest
            {
                AssetId = 8,
                Amount = 10,
                Price = 100,
                Operation = 'C',
                AccountId = "123"
            };

            _assetServiceMock.SetupSequence(x => x.GetAssetById(It.IsAny<int>()))
                .ReturnsAsync(new GetAssetDto { AssetTypeId = 1, Price = 100 }) 
                .ReturnsAsync((GetAssetDto)null); 

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
            Assert.Equal("Activo no encontrado.", result.Message);
        }

    }

}