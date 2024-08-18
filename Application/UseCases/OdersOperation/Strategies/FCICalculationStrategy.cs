using Application.UseCases.OdersOperation.Strategy;
using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.OdersOperation.Strategies
{
    public class FCICalculationStrategy : ICalculationStrategy
    {
        public decimal CalculateAmount(CreateOrderDto order)
        {
            return order.Price * order.Amount;
        }
    }
}
