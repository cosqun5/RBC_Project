using BusinessLayer.Services;
using System;

namespace RBCProjectMVC.Services
{
    public class WebFileEnvironment : IFileEnvironment
    {
        private readonly IWebHostEnvironment _environment;

        public WebFileEnvironment(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public string WebRootPath => _environment.WebRootPath;
    }
}
