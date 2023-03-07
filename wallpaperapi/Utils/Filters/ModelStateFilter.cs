using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using wallpaperapi.Models.Response;

namespace wallpaperapi.Utils.Filters
{
    public class ModelStateFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new ObjectResult(new InvalidModelResponse(context.ModelState));
            }
        }
    }
}
