using AutoMapper;
using Business.Extensions;
using Business.Services.Abstract;
using BusinessLayer.Services;
using DataAccess.Repositories.Abstract;
using Entities;
using Entities.ViewModels.Employee;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
namespace Business.Services.Concrate
{


    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;
        private readonly IFileEnvironment _environment;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository repository,
                               IFileEnvironment environment,
                               IMapper mapper)
        {
            _repository = repository;
            _environment = environment;
            _mapper = mapper;
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _repository.GetList();
        }

        public async Task<List<Employee>> SearchLiveAsync(string term)
        {
            return await _repository.SearchLiveAsync(term);
        }

        public async Task<List<Employee>> GetSortedAsync(string orderBy, bool isAsc)
        {
            return await _repository.GetSortedAsync(orderBy, isAsc);
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            var employee = await _repository.GetById(id);
            if (employee == null) throw new Exception("Employee not found");
            return employee;
        }

        public async Task AddAsync(EmployeeCreateVM createVM)
        {
            if (createVM == null) throw new ArgumentNullException(nameof(createVM));

            var employee = _mapper.Map<Employee>(createVM);

            if (createVM.File != null)
                await SaveFileAsync(employee, createVM.File);

            await _repository.Insert(employee);
            await _repository.SaveAsync();
        }

        public async Task UpdateAsync(EmployeeUpdateVM updateVM)
        {
            if (updateVM == null) throw new ArgumentNullException(nameof(updateVM));

            var existing = await _repository.GetById(updateVM.EmployeeId);
            if (existing == null) throw new Exception("Employee not found");

            _mapper.Map(updateVM, existing);

            if (updateVM.File != null)
                await SaveFileAsync(existing, updateVM.File, overwrite: true);

            _repository.Update(existing);
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _repository.GetById(id);
            if (existing == null) throw new Exception("Employee not found");

            string rootPath = Path.Combine(_environment.WebRootPath, "Uploads");

            if (!string.IsNullOrEmpty(existing.FilePath))
            {
                string fullPath = Path.Combine(rootPath, existing.FilePath);
                if (File.Exists(fullPath))
                    File.Delete(fullPath);
            }

            existing.FileBlob = null;

            _repository.Delete(existing);
            await _repository.SaveAsync();
        }

        private async Task SaveFileAsync(Employee employee, IFormFile file, bool overwrite = false)
        {
            string rootPath = Path.Combine(_environment.WebRootPath, "Uploads");

            if (overwrite && !string.IsNullOrEmpty(employee.FilePath))
            {
                string oldFullPath = Path.Combine(rootPath, employee.FilePath);
                if (File.Exists(oldFullPath))
                    File.Delete(oldFullPath);
            }

            string filePath = await file.SaveAsync(rootPath); 

            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                employee.FileBlob = ms.ToArray();
            }

            employee.FilePath = filePath;
        }
    }


}
