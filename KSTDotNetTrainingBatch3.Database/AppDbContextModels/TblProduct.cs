using System;
using System.Collections.Generic;

namespace KSTDotNetTrainingBatch3.Database.AppDbContextModels;

public partial class TblProduct
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public bool? DeleteFlag { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }
}
