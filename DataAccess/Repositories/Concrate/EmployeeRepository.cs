using Core.Concrate;
using DataAccess.Repositories.Abstract;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrate
{
    public class EmployeeRepository : BaseRepository<Employee, AppDbContext>, IEmployeeRepository
    {
        private readonly AppDbContext _context;
        public EmployeeRepository (AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetSortedAsync(string orderBy, bool isAsc)
        {
            var query = _context.Employees.AsQueryable();

            if (string.IsNullOrEmpty(orderBy))
                return await query.ToListAsync();

            switch (orderBy.ToLower())
            {
                case "fullname":
                    query = isAsc ? query.OrderBy(e => e.FullName) : query.OrderByDescending(e => e.FullName);
                    break;
                case "position":
                    query = isAsc ? query.OrderBy(e => e.Position) : query.OrderByDescending(e => e.Position);
                    break;
                case "department":
                    query = isAsc ? query.OrderBy(e => e.Department) : query.OrderByDescending(e => e.Department);
                    break;
                case "hiredate":
                    query = isAsc ? query.OrderBy(e => e.HireDate) : query.OrderByDescending(e => e.HireDate);
                    break;
                case "email":
                    query = isAsc ? query.OrderBy(e => e.Email) : query.OrderByDescending(e => e.Email);
                    break;
                case "phone":
                    query = isAsc ? query.OrderBy(e => e.Phone) : query.OrderByDescending(e => e.Phone);
                    break;
                case "salary":
                    query = isAsc ? query.OrderBy(e => e.Salary) : query.OrderByDescending(e => e.Salary);
                    break;
                default:
                    query = isAsc ? query.OrderBy(e => e.EmployeId) : query.OrderByDescending(e => e.EmployeId);
                    break;
            }

            return await query.ToListAsync();
        }


        public Task<List<Employee>> SearchLiveAsync(string trim)
        {
            return _context.Employees
                .FromSqlRaw("EXEC sp_SearchEmployees @term = {0}", trim)
                .ToListAsync();
        }


    }
}
