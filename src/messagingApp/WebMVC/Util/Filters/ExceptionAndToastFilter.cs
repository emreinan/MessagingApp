using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using WebMVC.Util.ExceptionHandling;

namespace WebMVC.Util.Filters;

public class ExceptionAndToastFilter(IToastNotification toastNotification
) : IActionFilter, // Controller action çalşırken veya çalıştıkran sonra neler yapılacağını belirtiyoruz
	IExceptionFilter // Controller action, exception throw ettiğinde neler yapılacağını belirtiyoruz
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Controller is Controller controller)
        {
            if (controller.TempData["ErrorMessage"] != null)
            {
                var errorMessage = controller.TempData["ErrorMessage"]!.ToString();
                toastNotification.AddErrorToastMessage(errorMessage, new ToastrOptions
                {
                    Title = "Hata"
                });
            }

            if (controller.TempData["SuccessMessage"] != null)
            {
                var errorMessage = controller.TempData["SuccessMessage"]!.ToString(); // Bu satır olursa auth servistekiyle aynı olur 2 kez yazar.
				toastNotification.AddSuccessToastMessage(errorMessage, new ToastrOptions
                {
                    Title = "Başarılı"
                });
            }
        }
    }

    public void OnActionExecuting(ActionExecutingContext context) { }

    public void OnException(ExceptionContext context)
    {

        if (context.Exception is ApiException apiEx)
        {
            if(apiEx.ApiError.Type == "https://example.com/probs/business")
                toastNotification.AddErrorToastMessage(apiEx.ApiError.Detail);

            else if (apiEx.ApiError.Type == "https://example.com/probs/validation")
                toastNotification.AddWarningToastMessage(apiEx.ApiError.Detail);

            else if (apiEx.ApiError.Type == "https://example.com/probs/notfound")
                toastNotification.AddInfoToastMessage(apiEx.ApiError.Detail);

			else if (apiEx.ApiError.Type == "https://example.com/probs/internal")
				toastNotification.AddAlertToastMessage(apiEx.ApiError.Detail);

			else if (apiEx.ApiError.Type == "https://example.com/probs/unauthorized")
				toastNotification.AddErrorToastMessage(apiEx.ApiError.Detail);

			else
				toastNotification.AddErrorToastMessage(apiEx.ApiError.Detail);

			//TODO: implement other exception types
		}


        if (context.HttpContext.Request.Method == "GET")
        {
            context.Result = new RedirectToActionResult("Error", "Home", null);
        }
        else if (context.HttpContext.Request.Method == "POST")
        {
            var routeValues = context.RouteData.Values;
            var controller = routeValues["controller"];
            var action = routeValues["action"];

            context.Result = new RedirectToActionResult(action.ToString(), controller.ToString(), null);
        }

        context.ExceptionHandled = true;
    }
}