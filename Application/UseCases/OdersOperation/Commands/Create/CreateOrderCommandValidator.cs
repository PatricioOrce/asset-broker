using Domain.Enums;
using Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.OdersOperation.Commands.Create
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommandHandlerRequest>
    {
        private readonly IAssetService _assetService;

        public CreateOrderCommandValidator(IAssetService assetService)
        {
            _assetService = assetService;

            RuleFor(x => x.AssetId)
                .NotEmpty().WithMessage("AssetId is mandatory.");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than 0.");

            RuleFor(x => x.Operation)
                .Must(op => op == 'C' || op == 'V')
                .WithMessage("Operation must be either 'C' (Compra) or 'V' (Venta).");

            RuleFor(x => x.Price)
                .MustAsync(async (request, price, cancellation) =>
                {
                    var asset = await _assetService.GetAssetById(request.AssetId);
                    if (asset == null)
                    {
                        request.ValidationMessage = "Activo invalido";
                        return false;
                    }

                    if (asset.AssetTypeId == (int)AssetTypeEnum.Accion && price > 0)
                    {
                        request.ValidationMessage = "El precio de la accion no debe ser especificado manualmente";
                        return false;
                    }

                    if (asset.AssetTypeId != (int)AssetTypeEnum.Accion && price <= 0)
                    {
                        request.ValidationMessage = "El precio debe ser mayor a 0";
                        return false;
                    }

                    return true;
                })
                .WithMessage(x => x.ValidationMessage);
        }
    }

}
