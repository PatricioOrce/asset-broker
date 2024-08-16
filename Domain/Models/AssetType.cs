using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class AssetType
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<FinancialAsset> FinancialAssets { get; set; } = new List<FinancialAsset>();
}
