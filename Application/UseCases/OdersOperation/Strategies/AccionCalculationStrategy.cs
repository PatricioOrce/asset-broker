using Application.UseCases.OdersOperation.Strategy;
using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.OdersOperation.Strategies
{
    public class AccionCalculationStrategy : ICalculationStrategy
    {
        public decimal CalculateAmount(CreateOrderDto order)
        {
            throw new NotImplementedException();
        }
    }
}
//● FCI: El precio del mismo recibido es unitario y al calcular el Monto Total (precio
//unitario * cantidad) no se aplican comisiones ni impuestos.
//● Acción: No debe recibir precio, ya que este debe estar en la BBDD. Al calcular el
//Monto Total se debe multiplicar el precio por la cantidad y discriminar las
//comisiones e impuestos que se van a pagar. Las comisiones son de 0.6% sobre el
//total y los impuestos el 21% de las comisiones
//● Bono: Se recibe precio y cantidad, discriminar las comisiones e impuestos que se
//van a pagar. Las comisiones son de 0.2% sobre el total y los impuestos el 21% de
//las comisiones