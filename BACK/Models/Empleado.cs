using System;
using System.Collections.Generic;

namespace BACK.Models;

public partial class Empleado
{
    public int NumEntry { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Cedula { get; set; } = null!;

    public DateTime FechaNacimiento { get; set; }

    public int Compania { get; set; }

    public int Cargo { get; set; }

    public decimal Salary { get; set; }

    public int Departament { get; set; }

    public virtual Position CargoNavigation { get; set; } = null!;

    public virtual Department DepartamentNavigation { get; set; } = null!;

    public virtual ICollection<EmDesc> EmDescs { get; set; } = new List<EmDesc>();

    public virtual ICollection<EmIng> EmIngs { get; set; } = new List<EmIng>();
}
