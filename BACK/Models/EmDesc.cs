using System;
using System.Collections.Generic;

namespace BACK.Models;

public partial class EmDesc
{
    public int NumEntry { get; set; }

    public double Monto { get; set; }

    public DateTime CreatedAt { get; set; }

    public int NumEmp { get; set; }

    public int EmTypeDesc { get; set; }

    public DateTime AplicationDate { get; set; }

    public virtual EmTypeDesc EmTypeDescNavigation { get; set; } = null!;

    public virtual Empleado NumEmpNavigation { get; set; } = null!;
}
