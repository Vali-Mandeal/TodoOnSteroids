using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Domain.Common.CustomFilters
{
    public class DevOnlyActionFilter : IActionFilter
    {
        private IHostingEnvironment HostingEnv { get; }
        public DevOnlyActionFilter(IHostingEnvironment hostingEnv)
        {
            HostingEnv = hostingEnv;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!HostingEnv.IsDevelopment())
            {
                context.Result = new BadRequestObjectResult("Object is null");
                return;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
