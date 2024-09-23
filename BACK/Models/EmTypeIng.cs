using System;
using System.Collections.Generic;

namespace BACK.Models;

public partial class EmTypeIng
{
    public int NumEntry { get; set; }

    public string Descripcion { get; set; } = null!;

    public int Configuracion { get; set; }

    public bool ThisTax { get; set; }

    public bool ThisExempt { get; set; }

    public bool ThisTaxMedical { get; set; }

    public bool ThisLaborBenefits { get; set; }

    public bool ThisChristmasSalary { get; set; }

    public virtual ICollection<EmIng> EmIngs { get; set; } = new List<EmIng>();
}
