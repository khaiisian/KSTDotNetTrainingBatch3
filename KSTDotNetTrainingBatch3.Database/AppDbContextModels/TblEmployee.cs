using System;
using System.Collections.Generic;

namespace KSTDotNetTrainingBatch3.Database.AppDbContextModels;

public partial class TblEmployee
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public decimal? Salary { get; set; }
}
