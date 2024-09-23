using System;
using System.Collections.Generic;

namespace BACK.Models;

public partial class EmIng
{
    public int NumEntry { get; set; }

    public double Monto { get; set; }

    public DateTime CreatedAt { get; set; }

    public int NumEmp { get; set; }

    public int EmTypeIng { get; set; }

    public DateTime AplicationDate { get; set; }

    public virtual EmTypeIng EmTypeIngNavigation { get; set; } = null!;

    public virtual Empleado NumEmpNavigation { get; set; } = null!;
}
