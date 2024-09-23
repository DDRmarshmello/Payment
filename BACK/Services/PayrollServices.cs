using BACK.Models;
using BACK.Models.DTO;
using BACK.Utils;

namespace BACK.Services
{
    public class PayrollServices
    {
        public List<payrollDetails> CalculateDetailPayroll (List<Empleado> data)
        {
            var details = new List<payrollDetails>();

            foreach (var emp in data)
            {
                decimal totalEmpentry = Math.Round(emp.Salary + (decimal)emp.EmIngs.Sum(x => x.Monto), 2);
                decimal totalpftax = emp.Salary + (decimal)emp.EmIngs.Where(x => x.EmTypeIngNavigation.ThisTaxMedical == true && x.EmTypeIngNavigation.ThisExempt != true && x.EmTypeIngNavigation.ThisTax == true).Sum(x => x.Monto);
                decimal totalTaxMedical = utils.CalculateMonthlyTax(totalpftax + (decimal)emp.EmIngs.Where(x => x.EmTypeIngNavigation.ThisTaxMedical == false && x.EmTypeIngNavigation.ThisExempt != true && x.EmTypeIngNavigation.ThisTax == true).Sum(x => x.Monto));
                decimal totalTax = Math.Round(utils.CalculateMonthlyISR(totalpftax - totalTaxMedical), 2);
                decimal totalTaxAmount = totalTaxMedical + (decimal)emp.EmDescs.Sum(x => x.Monto);

                details.Add(new payrollDetails
                {
                    empDto = utils.ConvertTo<Empleado, EmpDto>(emp),
                    totalProfits = totalEmpentry,
                    totalAmount = Math.Round(totalTaxAmount, 2),
                    totalTax = totalTax,
                    payrollAmount = Math.Round(totalEmpentry - totalTaxAmount - totalTax, 2)
                });
            }

            return details;
        }

        public payrollDetails CalculateDetailPayrollEmp(Empleado emp)
        {
            var details = new payrollDetails();

                decimal totalEmpentry = Math.Round(emp.Salary + (decimal)emp.EmIngs.Sum(x => x.Monto), 2);
                decimal totalpftax = emp.Salary + (decimal)emp.EmIngs.Where(x => x.EmTypeIngNavigation.ThisTaxMedical == true && x.EmTypeIngNavigation.ThisExempt != true && x.EmTypeIngNavigation.ThisTax == true).Sum(x => x.Monto);
                decimal totalTaxMedical = utils.CalculateMonthlyTax(totalpftax + (decimal)emp.EmIngs.Where(x => x.EmTypeIngNavigation.ThisTaxMedical == false && x.EmTypeIngNavigation.ThisExempt != true && x.EmTypeIngNavigation.ThisTax == true).Sum(x => x.Monto));
                decimal totalTax = Math.Round(utils.CalculateMonthlyISR(totalpftax - totalTaxMedical), 2);
                decimal totalTaxAmount = totalTaxMedical + (decimal)emp.EmDescs.Sum(x => x.Monto);

            details = new payrollDetails
            {
                empDto = utils.ConvertTo<Empleado, EmpDto>(emp),
                totalProfits = totalEmpentry,
                totalAmount = Math.Round(totalTaxAmount, 2),
                totalTax = totalTax,
                payrollAmount = Math.Round(totalEmpentry - totalTaxAmount - totalTax, 2)
            };
              
            return details;
            }
        }
    }

