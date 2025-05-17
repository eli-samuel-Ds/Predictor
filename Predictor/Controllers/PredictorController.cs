using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Predictor.Controllers
{
    public class PredictorController : Controller
    {
        public IActionResult Index()
        {
            var viewModel = new ValidadoDataPredictorViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(ValidadoDataPredictorViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            return RedirectToRoute(new { controller = "Predictor", action = "Index"});
        }
    }
}
