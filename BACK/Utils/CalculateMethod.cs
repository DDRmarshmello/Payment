namespace BACK.Utils
{
    public static partial class utils
    {
        private const decimal AFP_PORCENTAJE = 0.0287M;
        private const decimal SFS_PORCENTAJE = 0.0304M;

        // Topes máximos en pesos dominicanos
        private const decimal TOPE_AFP = 387050.00M;
        private const decimal TOPE_SFS = 193525.00M;

        public static decimal CalculateISR(decimal annualIncome)
        {
            decimal taxAmount = 0;

            // Tramo 1: Hasta RD$ 416,220.00: Exento
            if (annualIncome <= 416220.00m)
            {
                return taxAmount;  // Exento de impuestos
            }

            // Tramo 2: De RD$ 416,220.01 hasta RD$ 624,329.00: 15% del excedente de RD$ 416,220.01
            if (annualIncome <= 624329.00m)
            {
                taxAmount = (annualIncome - 416220.00m) * 0.15m;
                return taxAmount;
            }

            // Tramo 3: De RD$ 624,329.01 hasta RD$ 867,123.00: RD$ 31,216.35 + 20% del excedente de RD$ 624,329.01
            if (annualIncome <= 867123.00m)
            {
                taxAmount = 31216.35m + (annualIncome - 624329.00m) * 0.20m;
                return taxAmount;
            }

            // Tramo 4: Más de RD$ 867,123.01: RD$ 79,776.85 + 25% del excedente de RD$ 867,123.01
            taxAmount = 79776.85m + (annualIncome - 867123.00m) * 0.25m;

            return taxAmount;
        }

        // Método para calcular el ISR mensual
        public static decimal CalculateMonthlyISR(decimal monthlyIncome)
        {
            decimal annualIncome = monthlyIncome * 12;
            decimal annualTax = CalculateISR(annualIncome);
            decimal monthlyTax = annualTax / 12;
            return monthlyTax;
        }

        public static decimal CalculateAFP(decimal salarioMensual)
        {
            // Aplicar tope máximo si el salario supera el tope
            decimal baseAFP = salarioMensual > TOPE_AFP ? TOPE_AFP : salarioMensual;
            return baseAFP * AFP_PORCENTAJE;
        }

        public static decimal CalculateSFS(decimal salarioMensual)
        {
            // Aplicar tope máximo si el salario supera el tope
            decimal baseSFS = salarioMensual > TOPE_SFS ? TOPE_SFS : salarioMensual;
            return baseSFS * SFS_PORCENTAJE;
        }

        public static decimal CalculateMonthlyTax(decimal monthlyIncome)
        {
            return CalculateAFP(monthlyIncome) + CalculateSFS(monthlyIncome);
        }
    }
}
