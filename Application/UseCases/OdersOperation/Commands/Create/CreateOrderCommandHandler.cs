using Application.UseCases.OdersOperation.Mappers;
using Application.UseCases.OdersOperation.Strategies;
using Application.UseCases.OdersOperation.Strategy;
using Domain.DTOs;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Utils;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.UseCases.OdersOperation.Commands.Create
{
    public class CreateOrderCommandHandlerRequest : IRequest<Response<CreateOrderCommandResponse>>
    {
        [JsonIgnore]
        public string? AccountId { get; set; }
        public int AssetId { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public char Operation { get; set; }

        [JsonIgnore]
        public string? ValidationMessage { get; set; }
    }
    public class CreateOrderCommandHandler(IOrderService _orderService, IAssetService _assetService) : IRequestHandler<CreateOrderCommandHandlerRequest, Response<CreateOrderCommandResponse>>
    {
        public async Task<Response<CreateOrderCommandResponse>> Handle(CreateOrderCommandHandlerRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var validator = new CreateOrderCommandValidator(_assetService);
                ValidationResult result = await validator.ValidateAsync(request);
                if(!result.IsValid)
                {
                    return Response<CreateOrderCommandResponse>.Create((int)HttpStatusCode.BadRequest, null, result.Errors.ConvertToFormattedText());
                }
               
                var asset = await _assetService.GetAssetById(request.AssetId);
                if (asset == null)
                {
                    return Response<CreateOrderCommandResponse>.Create((int)HttpStatusCode.NotFound, null, "Activo no encontrado.");
                }
                if (asset.AssetTypeId == (int)AssetTypeEnum.Accion)
                {
                    request.Price = asset.Price;
                }
                ICalculationStrategy strategy = asset.AssetTypeId switch
                {
                    (int)AssetTypeEnum.Bono => new BonoCalculationStrategy(),
                    (int)AssetTypeEnum.FCI => new FCICalculationStrategy(),
                    (int)AssetTypeEnum.Accion => new AccionCalculationStrategy(),
                    _ => throw new ArgumentException("Activo invalido", nameof(request.AssetId))
                };
                var orderDto = request.ToCreateOrderDto();
                orderDto.TotalAmount = strategy.CalculateAmount(orderDto);
                await _orderService.CreateOrderAsync(orderDto);

                return Response<CreateOrderCommandResponse>.Create(
                    (int)HttpStatusCode.Created,
                    new CreateOrderCommandResponse() { Estado = "En Progreso" },
                    "Orden creada exitosamente");
            }
            catch (Exception ex)
            {
                return Response<CreateOrderCommandResponse>.Create((int)HttpStatusCode.InternalServerError, null, ex.Message);
            }

        }
    }
}
