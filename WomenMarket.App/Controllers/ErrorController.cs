namespace WomenMarket.App.Controllers
{
    using System.Web.Mvc;
    public class ErrorController : Controller
    {
        // GET: Error
        public ViewResult Error404()
        {
            Response.StatusCode = 404; 
            return View();
        }

        public ViewResult Error500()
        {
            Response.StatusCode = 500;
            return View();
        }
    }
}