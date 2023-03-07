using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using wallpaperapi.Models.Response;

namespace wallpaperapi.Utils.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.Result = new ObjectResult(new ErrorResponse
            {
                Error = true,
                Code = 500,
                Message = "Ocurrio un error no controlado",
                ErrorMessage = context.Exception.Message,
                StackTrace = context.Exception.ToString(),
                Data = context.Exception.Data.ToString(),
            });
        }
    }
}
