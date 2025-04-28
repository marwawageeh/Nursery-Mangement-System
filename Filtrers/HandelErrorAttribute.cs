using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace mvc_project.Filtrers
{
    public class HandelErrorAttribute : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            ViewResult viewResult = new ViewResult();
            viewResult.ViewName = "Error";

            context.Result = viewResult;
        }
    }
}
