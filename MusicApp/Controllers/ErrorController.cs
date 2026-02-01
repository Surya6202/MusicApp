using System.Web.Mvc;

namespace MusicApp.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult BadRequest()   // 400
        {
            ViewBag.Message = ViewBag.Message ?? "Bad Request – The server could not understand your request.";
            return View("Error");
        }

        public ActionResult Forbidden()    // 403
        {
            ViewBag.Message = ViewBag.Message ?? "Forbidden – You don’t have permission to access this resource.";
            return View("Error");
        }

        public ActionResult NotFound()     // 404
        {
            ViewBag.Message = ViewBag.Message ?? "Not Found – The requested resource could not be found.";
            return View("Error");
        }

        public ActionResult Unprocessable() // 422
        {
            ViewBag.Message = ViewBag.Message ?? "Unprocessable Entity – The request was well-formed but could not be processed.";
            return View("Error");
        }

        public ActionResult ServerError()  // 500
        {
            ViewBag.Message = ViewBag.Message ?? "Internal Server Error – Something went wrong on our side.";
            return View("Error");
        }

        public ActionResult ServiceUnavailable() // 503
        {
            ViewBag.Message = ViewBag.Message ?? "Service Unavailable – The server is temporarily overloaded or down.";
            return View("Error");
        }

        public ActionResult GatewayTimeout() // 504
        {
            ViewBag.Message = ViewBag.Message ?? "Gateway Timeout – The server did not respond in time.";
            return View("Error");
        }
    }
}