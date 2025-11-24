using Entities;
using Entities.ViewModels.Employee;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAllAsync();                           
        Task<List<Employee>> SearchLiveAsync(string term);            
        Task<List<Employee>> GetSortedAsync(string orderBy, bool isAsc);
        Task<Employee> GetByIdAsync(int id);
        Task AddAsync(EmployeeCreateVM creatVM);
        Task UpdateAsync(EmployeeUpdateVM updateVM);

        Task DeleteAsync(int id);
    }

}
