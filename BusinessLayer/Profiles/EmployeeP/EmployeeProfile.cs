using AutoMapper;
using Entities;
using Entities.ViewModels.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles.EmployeeP
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeCreateVM, Employee>();
            CreateMap<EmployeeUpdateVM, Employee>()
                        .ForMember(dest => dest.EmployeId, opt => opt.Ignore())
                        .ForMember(dest => dest.FileBlob, opt => opt.Ignore())
                        .ForMember(dest => dest.FilePath, opt => opt.Ignore());
        }

    }
}
