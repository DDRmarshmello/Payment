namespace BACK.Models.DTO
{
    public class PayrollDto
    {
        public decimal totalAmount {  get; set; }
        public string payrollId { get; set; }
        public int payrollType { get; set; }
        public string payrollDescription { get; set; }
        public DateTime? payrollDate { get; set; } = default(DateTime?);
        public DateTime payrollPeriodStar { get; set; }
        public DateTime payrollPeriodEnd { get; set; }
        public DateTime payrollPeriodPay { get; set; }
        public int payrollPeriodProcces { get; set; } 
        public int totalEmps { get; set; }
        public List<payrollDetails> payrollDetails { get; set; } = new List<payrollDetails>();

    }

    public class payrollDetails
    {
        public EmpDto empDto { get; set; } 
        public decimal totalAmount { get; set; }
        public decimal totalTax { get; set; }
        public decimal totalProfits { get; set; }
        public decimal payrollAmount { get; set; }
    }

}

