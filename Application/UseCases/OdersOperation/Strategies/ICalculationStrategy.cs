using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.OdersOperation.Strategy
{
    public interface ICalculationStrategy
    {
        decimal CalculateAmount(CreateOrderDto order);
    }
}
