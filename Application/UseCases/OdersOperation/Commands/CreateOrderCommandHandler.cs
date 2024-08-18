using Application.UseCases.OdersOperation.Mappers;
using Application.UseCases.OdersOperation.Strategies;
using Application.UseCases.OdersOperation.Strategy;
using Domain.DTOs;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Utils;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.UseCases.OdersOperation.Commands
{
    public class CreateOrderCommandHandlerRequest : IRequest<Response<CreateOrderCommandResponse>>
    {
        [JsonIgnore]
        public string? AccountId { get; set; }
        public int AssetId { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public char Operation { get; set; }
    }
    public class CreateOrderCommandHandler(IOrderService _orderService) : IRequestHandler<CreateOrderCommandHandlerRequest, Response<CreateOrderCommandResponse>>
    {
        public async Task<Response<CreateOrderCommandResponse>> Handle(CreateOrderCommandHandlerRequest request, CancellationToken cancellationToken)
        {
            var response = new Response<CreateOrderCommandResponse>();
            //hay que conseguir el assetType desde el asset que trae el asset id.
            ICalculationStrategy strategy = request.AssetId switch
            {
                (int)AssetTypeEnum.Bono => new BonoCalculationStrategy(),
                (int)AssetTypeEnum.Accion => new AccionCalculationStrategy(),
                (int)AssetTypeEnum.FCI => new FCICalculationStrategy(),
                _ => throw new ArgumentException("Invalid asset type", nameof(request.AssetId))
            };
            var orderDto = request.ToCreateOrderDto();
            orderDto.TotalAmount = strategy.CalculateAmount(orderDto);
            await _orderService.CreateOrderAsync(orderDto);
            return response;
        }
    }
}
