using Domain.DTOs;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AssetService(IAssetRepository _assetRepository) : IAssetService
    {
        public async Task<GetAssetDto> GetAssetById(int assetId)
        {
            var assetDto = await _assetRepository.GetByIdAsync(assetId);
            if(assetDto != null)
            {
                return new GetAssetDto
                {
                    AssetTypeId = assetDto.AssetTypeId,
                    Price = assetDto.UnitPrice
                };
            }
            return new GetAssetDto();
        }
    }
}
