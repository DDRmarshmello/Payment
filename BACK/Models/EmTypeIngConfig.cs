using System;
using System.Collections.Generic;

namespace BACK.Models;

public partial class EmTypeIngConfig
{
    public int NumEntry { get; set; }

    public bool ThisTax { get; set; }

    public bool ThisExempt { get; set; }

    public bool ThisTaxMedical { get; set; }

    public bool ThisLaborBenefits { get; set; }

    public bool ThisChristmasSalary { get; set; }
}
