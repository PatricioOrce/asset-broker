using Application.UseCases.OdersOperation.Mappers;
using Application.UseCases.OdersOperation.Queries.GetAll;
using Domain.Interfaces;
using Domain.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.OdersOperation.Queries.GetById
{
    public class GetOrderByIdQueryHandlerRequest : IRequest<Response<GetOrderByIdQueryResponse>>
    {
        public int OrderId { get; set; }
    }
    public class GetOrderByIdQueryHandler(IOrderService _ordersService) : IRequestHandler<GetOrderByIdQueryHandlerRequest, Response<GetOrderByIdQueryResponse>>
    {
        public async Task<Response<GetOrderByIdQueryResponse>> Handle(GetOrderByIdQueryHandlerRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _ordersService.GetOrderById(request.OrderId);
                if (result == null)
                {
                    return Response<GetOrderByIdQueryResponse>.Create(statusCode: (int)HttpStatusCode.NotFound);
                }
                return Response<GetOrderByIdQueryResponse>.Create(body: result.ToSingleOrderDto());
            }
            catch (Exception ex)
            {
                return Response<GetOrderByIdQueryResponse>.Create(statusCode: (int)HttpStatusCode.InternalServerError, message: ex.Message);
            }
        }
    }
}
