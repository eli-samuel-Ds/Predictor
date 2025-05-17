using Application.Dtos;
using Application.Services;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Predictor.Controllers
{
    public class PredictorController : Controller
    {
        private readonly PredictorServices _svc;
        private readonly SelectorPredictorServices _selSvc;

        public PredictorController()
        {
            _svc = new PredictorServices();
            _selSvc = new SelectorPredictorServices();
        }

        public IActionResult Index()
        {
            var selectorDto = _selSvc.Get();
            var vm = new ValidadoDataPredictorViewModel();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Index(ValidadoDataPredictorViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var dto = new SalidaValidadoDataPredictorDto
            {
                Items = vm.Items
                          .Select(x => new DataPredictorDto
                          {
                              Fecha = x.Fecha,
                              Valor = x.Valor
                          })
                          .ToList()
            };

            var selector = _selSvc.Get();

            _svc.Save(dto, selector);

            return RedirectToAction(nameof(ResultadoSMA));
        }

        [HttpGet]
        public IActionResult ResultadoSMA()
        {
            var dto = _svc.GetAll();
            return View(dto);
        }
    }
}
