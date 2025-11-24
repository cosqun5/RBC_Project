using Core.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        Task<List<Employee>> SearchLiveAsync(string term);

        Task<List<Employee>> GetSortedAsync(string orderBy, bool isAsc);
    }
}
