using BACK.Models;
using BACK.Models.DTO;
using BACK.Services;
using BACK.Utils;
using Microsoft.AspNetCore.Mvc;

namespace BACK.Controllers
{
    [Route("api/[controller]")]
    public class PayrollController : Controller
    {
        private readonly EmpServices _empServices;
        private readonly PayrollServices _payrollServices;
        public PayrollController(EmpServices empServices, PayrollServices payrollServices) 
        { 
            _empServices = empServices;
            _payrollServices = payrollServices;
        }

        [Route("PayrollEntry")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _empServices.Getempleados(true);

            var dt = _payrollServices.CalculateDetailPayroll(data);
            var payroll = new PayrollDto
            {
                payrollDate = DateTime.Now,
                payrollDetails = dt,
                payrollId = Guid.NewGuid().ToString(),
                payrollPeriodEnd = DateTime.Now,
                payrollPeriodStar = DateTime.Now,
                payrollPeriodPay = DateTime.Now.AddDays(1),
                payrollDescription = "Payroll Wendensday",
                payrollPeriodProcces = 2,
                payrollType = 1,
                totalAmount = dt.Sum(x=>x.payrollAmount),
                totalEmps = data.Count,
            };

            return Ok(payroll);
        }

        [Route("PayrollEntry/emp/{NumEntry}")]
        [HttpGet]
        public async Task<IActionResult> EmpPayroll(int NumEntry)
        {
            var data = await _empServices.GetempleadoId(NumEntry);

            var dt = _payrollServices.CalculateDetailPayrollEmp(data);

            return Ok(dt);
        }
    }
}
