using Application.UseCases.OdersOperation.Strategy;
using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.OdersOperation.Strategies
{
    public class BonoCalculationStrategy : ICalculationStrategy
    {
        const decimal FEES_PERC = 0.2m;
        const int TAX_PERC = 21;
        public decimal CalculateAmount(CreateOrderDto order)
        {
            var totalAmount = order.Price * order.Amount;
            var fees = (totalAmount * FEES_PERC) / 100;
            var taxes = (fees * TAX_PERC) / 100;
            return totalAmount + (taxes + fees);
        }
    }
}
