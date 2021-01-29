using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.PlatformAbstractions;
using System.Diagnostics;

namespace Solution.Api.Controllers
{
    [AllowAnonymous]
    public class HomeController: Controller
    {
        [HttpGet]
        [Route("")]        
        public object Get()
        {
            string appPath = PlatformServices.Default.Application.ApplicationBasePath;
            string appName = PlatformServices.Default.Application.ApplicationName;

            var fileVersionInfo = FileVersionInfo.GetVersionInfo(appPath + $"{appName}.exe");

            return new
            {
                Description = "Aplication - WEB API",
                Version = fileVersionInfo.ProductVersion
            };
        }

    }
}
