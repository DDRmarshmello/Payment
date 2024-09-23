using System;
using System.Collections.Generic;

namespace BACK.Models;

public partial class EmTypeDesc
{
    public int NumEntry { get; set; }

    public string Descripcion { get; set; } = null!;

    public int Configuracion { get; set; }

    public bool ThisLoan { get; set; }

    public int NumQuotas { get; set; }

    public double? ThisPercentage { get; set; }

    public virtual ICollection<EmDesc> EmDescs { get; set; } = new List<EmDesc>();
}
