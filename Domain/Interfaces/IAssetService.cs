using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAssetService
    {
        Task<GetAssetDto> GetAssetById(int assetId);
    }
}
