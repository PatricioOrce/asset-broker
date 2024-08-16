﻿using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Order
{
    public int Id { get; set; }

    public int AccountId { get; set; }

    public int AssetId { get; set; }

    public int Amount { get; set; }

    public decimal Price { get; set; }

    public string Operation { get; set; } = null!;

    public int? StatusId { get; set; }

    public decimal? TotalAmount { get; set; }

    public virtual OrderStatus? Status { get; set; }
}
