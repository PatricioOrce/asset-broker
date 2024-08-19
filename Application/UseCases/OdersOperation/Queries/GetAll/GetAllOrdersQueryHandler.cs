using Application.UseCases.OdersOperation.Commands.Create;
using Application.UseCases.OdersOperation.Mappers;
using Domain.DTOs;
using Domain.Interfaces;
using Domain.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.UseCases.OdersOperation.Queries.GetAll
{
    public class GetAllOrdersQueryHandlerRequest : IRequest<Response<List<GetAllOrdersQueryResponse>>>
    {

    }
    public class GetAllOrdersQueryHandler(IOrderService _ordersService) : IRequestHandler<GetAllOrdersQueryHandlerRequest, Response<List<GetAllOrdersQueryResponse>>>
    {
        public async Task<Response<List<GetAllOrdersQueryResponse>>> Handle(GetAllOrdersQueryHandlerRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _ordersService.GetAllOrdersAsync();
                return Response<List<GetAllOrdersQueryResponse>>.Create(body: result.ToOrderDto());
            }
            catch (Exception ex)
            {
                return Response<List<GetAllOrdersQueryResponse>>.Create(statusCode: (int)HttpStatusCode.InternalServerError, message: ex.Message);
            }
        }
    }
}
