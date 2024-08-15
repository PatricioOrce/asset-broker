using Domain.Utils;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.OdersOperation.Commands
{
    public class CreateOrderCommandHandlerRequest : IRequest<Response<CreateOrderCommandResponse>>
    {
        public int something { get; set; }
    }
    public class CreateOrderCommandHandler() : IRequestHandler<CreateOrderCommandHandlerRequest, Response<CreateOrderCommandResponse>>
    {
        public async Task<Response<CreateOrderCommandResponse>> Handle(CreateOrderCommandHandlerRequest request, CancellationToken cancellationToken)
        {
            var response = new Response<CreateOrderCommandResponse>()
            {
                Body = new CreateOrderCommandResponse()
                {
                    ResponseTest = 123
                }
            };
            response.SetSuccessResponse("Success");
            return response;
        }
    }
}
