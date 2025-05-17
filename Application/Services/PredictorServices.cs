using Application.Dtos;
using Application.Enums;
using Application.Repositories;

namespace Application.Services
{
    public class PredictorServices
    {
        private readonly SalidaPredictorRepository _repo = SalidaPredictorRepository.Instance;
        public void Save(SalidaValidadoDataPredictorDto dto, SelectorPredictorDto selector)
        {
            switch (selector.Opcion)
            {
                case PredictorType.sma:
                    if (dto.Items.Count < 20)
                        throw new ArgumentException("Se requieren 20 valores para SMA.");

                    var last20 = dto.Items.TakeLast(20).Select(x => x.Valor).ToList();
                    var last5 = last20.TakeLast(5).ToList();

                    dto.SMAShort = Math.Round(last5.Average(), 4);
                    dto.SMALong = Math.Round(last20.Average(), 4);

                    dto.Trend = dto.SMAShort > dto.SMALong
                                ? "Tendencia alcista"
                                : (dto.SMAShort < dto.SMALong
                                    ? "Tendencia bajista"
                                    : "Sin cruce (estática)");

                    _repo.ListvalidadoDataPredictor = dto;
                    break;
                case PredictorType.regresionLineal:
                    break;
                case PredictorType.roc:
                    break;
                default:
                    break;
            }
        }

        public SalidaValidadoDataPredictorDto GetAll()
            => _repo.ListvalidadoDataPredictor;
    }
}
