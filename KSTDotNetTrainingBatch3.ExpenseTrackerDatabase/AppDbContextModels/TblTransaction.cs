using System;
using System.Collections.Generic;

namespace KSTDotNetTrainingBatch3.ExpenseTrackerDatabase.AppDbContextModels;

public partial class TblTransaction
{
    public int ExpenseId { get; set; }

    public int CategoryId { get; set; }

    public string? Type { get; set; }

    public decimal? Amount { get; set; }

    public string? Remark { get; set; }

    public DateTime? TransactionDate { get; set; }

    public bool? DeleteFlag { get; set; }
}
