using BACK.Models;
using BACK.Models.DTO;
using BACK.Services;
using BACK.Utils;
using Microsoft.AspNetCore.Mvc;

namespace BACK.Controllers
{
    [Route("api/[controller]")]
    public class EmpController : Controller
    {
        private readonly EmpServices _empServices;
        public EmpController(EmpServices empServices) 
        { 
            _empServices = empServices;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int NumEntry)
        {
            if (NumEntry > 0)
            {
                var emp = await _empServices.GetempleadoId(NumEntry);
                if (emp == null)
                    return NotFound();
                var js = utils.ConvertTo<Empleado, EmpDto>(emp);

                return Ok(js);
            }
            var data = await _empServices.Getempleados();
            if (data == null)
                return NotFound();

            var dt = new List<EmpDto>();

            foreach (var item in data)
            {
                dt.Add(utils.ConvertTo<Empleado, EmpDto>(item));
            }
            return Ok(dt);
        }


        [Route("AddEmp")]
        [HttpPost]
        public async Task<IActionResult> AddEmp([FromBody] Empleado emp)
        {
            var reuslt = await _empServices.AddEmpleado(emp);

            if (reuslt == null)
                return BadRequest();

            return Ok(reuslt); 

        }

        [Route("PutEmp")]
        [HttpPut]
        public async Task<IActionResult> UpdateEmp([FromBody] Empleado emp)
        {
            var reuslt = await _empServices.AddEmpleado(emp);
            if (reuslt == null)
                return BadRequest();

            return Ok(reuslt);
        }

        [Route("DeleteEmp")]
        [HttpPost]
        public async Task<IActionResult> DeleteEmp([FromBody] Empleado emp)
        {
            var reuslt = await _empServices.AddEmpleado(emp);
            if (reuslt == null)
                return BadRequest();

            return Ok(reuslt);
        }

        [HttpGet]
        [Route("EmployeeResponse")]
        public async Task<IActionResult> EmployeeResponse()
        {
            var emp = await _empServices.Getempleados(true, 1);
            var data = new List<EmpDtoResponse>();
            foreach (var item in emp)
            {
                data.Add(new EmpDtoResponse
                {
                    Apellido = item.Apellido,
                    Cargo = item.CargoNavigation.Description,
                    Cedula = item.Cedula,
                    Compania = item.Compania,
                    Departament = item.DepartamentNavigation.Description,
                    FechaNacimiento = item.FechaNacimiento,
                    Nombre = item.Nombre,
                    NumEntry = item.NumEntry,
                    Salary = item.Salary
                });
            }
            return Ok(data);
        }
    }
}
