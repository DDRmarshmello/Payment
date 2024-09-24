namespace BACK.Models.DTO
{
    public class EmpDto
    {
        public int NumEntry { get; set; }

        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public string Cedula { get; set; } = null!;

        public DateTime FechaNacimiento { get; set; }

        public int Compania { get; set; }

        public int Cargo { get; set; }
        public decimal Salary { get; set; }
        // Lista de ingresos asociados
        public List<EmIngDTO> EmIngs { get; set; } = new List<EmIngDTO>();

        // Lista de descripciones de empleados
        public List<DesEmpDto> EmDescs { get; set; } = new List<DesEmpDto>();
    }

    public class EmTypeDesc
    {
        public int NumEntry { get; set; }

        public string Descripcion { get; set; } = null!;

        public int Configuracion { get; set; }

        public bool ThisLoan { get; set; }

        public int NumQuotas { get; set; }

        public double? ThisPercentage { get; set; }
    }

    public class EmTypePayment
    {
        public int NumEntry { get; set; }

        public string Descripcion { get; set; } = null!;

        public int Configuracion { get; set; }

        public bool ThisTax { get; set; }

        public bool ThisExempt { get; set; }

        public bool ThisTaxMedical { get; set; }

        public bool ThisLaborBenefits { get; set; }

        public bool ThisChristmasSalary { get; set; }

    }

    public class EmIngDTO
    {
        public int NumEntry { get; set; }

        public double Monto { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime AplicationDate { get; set; }
        public int NumEmp { get; set; }

        public int EmTypeIng { get; set; }
        public EmTypePayment EmTypeIngNavigation { get; set; } = null!;

    }

    public class DesEmpDto
    {
        public int NumEntry { get; set; }

        public double Monto { get; set; }

        public DateTime CreatedAt { get; set; }

        public int NumEmp { get; set; }
        public DateTime AplicationDate { get; set; }
        public int EmTypeDesc { get; set; }

        // Aquí añadimos una instancia para el objeto EmTypeDesc que represente la navegación
        public EmTypeDesc? EmTypeDescNavigation { get; set; }
    }


    public class EmpDtoResponse
    {
        public int NumEntry { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Cedula { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public int Compania { get; set; }
        public string Cargo { get; set; }
        public decimal Salary { get; set; }
        public string Departament { get; set; }
    }
}
