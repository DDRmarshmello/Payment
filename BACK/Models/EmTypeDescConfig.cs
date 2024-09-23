using System;
using System.Collections.Generic;

namespace BACK.Models;

public partial class EmTypeDescConfig
{
    public int NumEntry { get; set; }

    public bool ThisLoan { get; set; }

    public int NumQuotas { get; set; }

    public double? ThisPercentage { get; set; }
}
