using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace HeadHunterVer1._0.Filters
{
    /// <summary>
    /// Для фильтрации исключений в действиях контроллеров был создан специальный пользовательский фильтр.
    /// </summary>
    public class CustomExceptionFilterAttribute : Attribute, IExceptionFilter
    {
            /// <summary>
            /// метод обработки ошибки и последующий вывод ошибки во вьюшку
            /// </summary>
            /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            string actionName = context.ActionDescriptor.DisplayName;
            string exceptionMessage = context.Exception.Message;
            string exceptionStack = context.Exception.StackTrace;
            int errorCode = context.HttpContext.Response.StatusCode;
            // Отобразить кастомную страницу с ошибкой
            context.Result = new ViewResult
            {
                ViewName = "Error", // Название представления ошибки
                ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    { "ExceptionMessage", exceptionMessage }, // Передать сообщение об ошибке в представление
                    { "ExceptionStack", exceptionStack }, // Передать стек исключения в представление
                    { "ActionName", actionName }, // Передать имя действия, где произошла ошибка
                    { "ErrorCode", errorCode } // Передать текущий код ошибки
                }
            };

            context.ExceptionHandled = true;
        }
    }
}