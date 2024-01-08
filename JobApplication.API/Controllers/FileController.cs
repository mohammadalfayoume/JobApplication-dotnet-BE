using JobApplication.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FileController : JobApplicationBaseController<FileService>
    {
        public FileController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
        [HttpGet]
        public async Task<IActionResult> GetFileById(int id)
        {
            var file = await CurrentService.GetFileByIdAsync(id);
            return File(file.Content, file.ContentType);
        }
    }
}
