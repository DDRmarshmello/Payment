using System;
using System.Collections.Generic;

namespace BACK.Models;

public partial class Position
{
    public int NumEntry { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
