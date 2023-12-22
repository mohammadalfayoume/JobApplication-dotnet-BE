using JobApplication.Data;
using JobApplication.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.API.Controllers
{
    public class JobApplicationBaseController<T> : ControllerBase where T : IJobApplicationBaseService
    {
        public JobApplicationBaseController(IServiceProvider serviceProvider)
        {
            CurrentService = serviceProvider.GetRequiredService<T>();
            DbContext = serviceProvider.GetRequiredService<StoreContext>();
        }

        public T CurrentService { get; set; }
        public StoreContext DbContext { get; set; }

        
    }
}
