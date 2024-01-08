using JobApplication.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Service.Services
{
    public class JobApplicationBaseService : IJobApplicationBaseService
    {

        public JobApplicationBaseService(IServiceProvider serviceProvider)
        {
            DbContext = serviceProvider.GetRequiredService<StoreContext>();
        }

        public StoreContext DbContext{ get; set; }
    }
}
