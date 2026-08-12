using System;
using System.Web.Mvc;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;

namespace GateGold.Filters 
{
    public class CustomExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
    
            if (filterContext.ExceptionHandled)
                return;

            try
            {
                var exceptionMessage = filterContext.Exception.Message;
                var stackTrace = filterContext.Exception.StackTrace;
                var controllerName = filterContext.RouteData.Values["controller"]?.ToString();
                var actionName = filterContext.RouteData.Values["action"]?.ToString();

               
                using (var context = new AppDbContext()) 
                {
                    var log = new AdminLog
                    {
                        Process = $"Hata: {controllerName}/{actionName}",
                        ProcessDate = DateTime.Now,
                        Info = exceptionMessage,
                        
                    };

                    context.AdminLogs.Add(log);
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                
            }


            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/Shared/Error.cshtml"
            };

            filterContext.ExceptionHandled = true;
        }
    }
}