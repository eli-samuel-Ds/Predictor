using System;
using System.Linq;
using Application.Dtos;
using Application.Enums;
using Application.Repositories;

namespace Application.Services
{
    public class PredictorServices
    {
        private readonly SalidaPredictorRepository _repository = SalidaPredictorRepository.Instance;

        public void Save(SalidaValidadoDataPredictorDto dataDto, SelectorPredictorDto selectorDto)
        {
            switch (selectorDto.Opcion)
            {
                case PredictorType.sma:
                    ApplySma(dataDto);
                    break;

                case PredictorType.regresionLineal:
                    ApplyRegression(dataDto);
                    break;

                case PredictorType.roc:
                    ApplyRoc(dataDto, period: 5);
                    break;
            }

            _repository.ListvalidadoDataPredictor = dataDto;
        }

        private static void ApplySma(SalidaValidadoDataPredictorDto dto)
        {
            var values = dto.Items.Select(item => item.Valor).ToList();
            var longWindow = values.TakeLast(20).ToList();
            var shortWindow = longWindow.TakeLast(5).ToList();

            dto.SMALong = Math.Round(longWindow.Average(), 4);
            dto.SMAShort = Math.Round(shortWindow.Average(), 4);
            dto.Trend = dto.SMAShort.CompareTo(dto.SMALong) switch
            {
                > 0 => "Tendencia alcista",
                < 0 => "Tendencia bajista",
                _ => "Sin cruce (estática)"
            };
        }

        private static void ApplyRegression(SalidaValidadoDataPredictorDto dto)
        {
            int n = dto.Items.Count;
            var xSeq = Enumerable.Range(1, n).Select(i => (decimal)i);
            var ySeq = dto.Items.Select(d => d.Valor);

            var sumX = xSeq.Sum();
            var sumY = ySeq.Sum();
            var sumXY = xSeq.Zip(ySeq, (x, y) => x * y).Sum();
            var sumX2 = xSeq.Select(x => x * x).Sum();

            var m = (sumXY - sumX * sumY / n) / (sumX2 - sumX * sumX / n);
            var b = sumY / n - m * (sumX / n);
            var forecast = m * (n + 1) + b;

            dto.RegressionSlope = Math.Round(m, 4);
            dto.RegressionIntercept = Math.Round(b, 4);
            dto.PredictedValue = Math.Round(forecast, 4);
            dto.Trend = forecast > dto.Items.Last().Valor
                                      ? "Tendencia alcista"
                                      : "Tendencia bajista";
        }

        private static void ApplyRoc(SalidaValidadoDataPredictorDto dto, int period)
        {
            var prices = dto.Items.Select(d => d.Valor).ToList();
            var prefix = Enumerable.Repeat<decimal?>(null, period);
            var rocList = prices
                .Skip(period)
                .Select((current, idx) => (decimal?)Math.Round(((current / prices[idx]) - 1) * 100, 4));

            dto.RocValues = prefix.Concat(rocList).ToList();

            var lastRoc = dto.RocValues.LastOrDefault();
            dto.Trend = lastRoc > 0
                        ? "Tendencia alcista"
                        : "Tendencia bajista";
        }
        public SalidaValidadoDataPredictorDto GetAll()
            => _repository.ListvalidadoDataPredictor;
    }
}
