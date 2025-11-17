using System;
using System.Collections.Generic;

namespace KSTDotNetTrainingBatch3.ExpenseTrackerDatabase.AppDbContextModels;

public partial class TblCategory
{
    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public DateTime? CreatedDateTime { get; set; }

    public DateTime? ModifiedDateTime { get; set; }

    public bool? DeleteFlag { get; set; }
}
