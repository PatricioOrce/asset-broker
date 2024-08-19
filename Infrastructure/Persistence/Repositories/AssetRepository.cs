using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class AssetRepository : Repository<Asset>, IAssetRepository
    {
        public AssetRepository(CoreDbContext context) : base(context)
        {
        }
    }
}
