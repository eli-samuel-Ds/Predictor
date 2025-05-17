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
                    int n = dto.Items.Count;

                    var xVals = Enumerable.Range(1, n).Select(i => (decimal)i).ToList();
                    var yVals = dto.Items.Select(d => d.Valor).ToList();

                    decimal sumX = xVals.Sum();
                    decimal sumY = yVals.Sum();
                    decimal sumXY = xVals.Zip(yVals, (x, y) => x * y).Sum();
                    decimal sumX2 = xVals.Select(x => x * x).Sum();

                    decimal m = (sumXY - (sumX * sumY) / n)
                                / (sumX2 - (sumX * sumX) / n);
                    decimal b = (sumY / n) - m * (sumX / n);

                    decimal xPred = n + 1;
                    decimal yPred = m * xPred + b;

                    dto.PredictedValue = Math.Round(yPred, 4);
                    dto.RegressionSlope = Math.Round(m, 4);
                    dto.RegressionIntercept = Math.Round(b, 4);
                    dto.Trend = yPred > yVals.Last()
                                ? "Tendencia alcista"
                                : "Tendencia bajista";

                    _repo.ListvalidadoDataPredictor = dto;
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
