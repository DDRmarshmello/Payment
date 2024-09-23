using BACK.Models;
using BACK.Models.DTO;
using BACK.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BACK.Services
{
    public interface IempServices
    {
        Task<List<Empleado>> Getempleados(bool include);
        Task<Empleado> GetempleadoId(int id);

    }
    public class EmpServices : IempServices
    {
        private readonly IRepository<Empleado> _repository;
        public EmpServices(IRepository<Empleado> bookRepository) 
        {
            _repository = bookRepository;
        }

        public async Task<List<Empleado>> Getempleados(bool include = false)
        {
            if (include)
                return (List<Empleado>)await _repository.GetAllAsync(
                q => q.Include(e => e.EmDescs).ThenInclude(d => d.EmTypeDescNavigation),
                q => q.Include(e => e.EmIngs).ThenInclude(d=> d.EmTypeIngNavigation));

            return (List<Empleado>)await _repository.GetAllAsync(
                q => q.Include(e => e.EmDescs),
                q => q.Include(e => e.EmIngs));
        }

        public async Task<Empleado> GetempleadoId(int id)
        {
            return await _repository.GetByIdAsync(
                id,
                q => q.Include(e => e.EmDescs).ThenInclude(d => d.EmTypeDescNavigation),
                q => q.Include(e => e.EmIngs).ThenInclude(d => d.EmTypeIngNavigation));  // Si tienes relaciones adicionales   // Incluir la navegación dentro de EmIngs

        }

        public async Task<ResponseDto> DeleteEmp(Empleado empleado)
        {
            return await _repository.DeleteAsync(empleado.NumEntry);
        }

        public async Task<Empleado> AddEmpleado(Empleado empleado)
        {

            return await _repository.AddAsync(empleado);
        }
    }
}
