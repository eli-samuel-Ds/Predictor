using Application.Dtos;
using Application.Enums;
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

        // GET Index
        public IActionResult Index()
        {
            var vm = new ValidadoDataPredictorViewModel();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Index(ValidadoDataPredictorViewModel vm)
        {
            // Si hay errores de validación en el ViewModel, vuelves a Index(vm)
            if (!ModelState.IsValid)
                return View(vm);

            // Mapeo al DTO
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

            // Guardamos y redirigimos a Result pasando mensaje de éxito
            _svc.Save(dto, selector);
            return RedirectToRoute(new
            {
                controller = "Predictor",
                action = "Result",
                message = "¡Operación realizada correctamente!",
                messageType = "alert-success"
            });
        }

        // GET Result
        [HttpGet]
        public IActionResult Result(string message = null, string messageType = null)
        {
            // Solo se mostrará en la vista si viene por query
            ViewBag.Message = message;
            ViewBag.MessageType = messageType;

            var dto = _svc.GetAll();
            var selector = _selSvc.Get().Opcion;

            if (selector == PredictorType.sma)
                return View("ResultadoSMA", dto);
            if (selector == PredictorType.regresionLineal)
                return View("ResultRegresion", dto);
            if (selector == PredictorType.roc)
                return View("ResultRoc", dto);

            return View("ResultSma", dto);
        }
    }
}
