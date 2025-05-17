using Application.Dtos;
using Application.Services;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Predictor.Controllers
{
    public class ModoController : Controller
    {
        private readonly SelectorPredictorServices _selectorService;

        public ModoController()
        {
            _selectorService = new SelectorPredictorServices();
        }

        [HttpGet]
        public IActionResult Modo()
        {
            var dto = _selectorService.Get();
            var vm = new SelectorPredictorViewModel
            {
                OpcionSelector = dto.Opcion
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Modo(SelectorPredictorViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var dto = new SelectorPredictorDto
            {
                Opcion = vm.OpcionSelector
            };

            _selectorService.Save(dto);

            return RedirectToAction(nameof(Index), "Predictor");
        }
    }
}
