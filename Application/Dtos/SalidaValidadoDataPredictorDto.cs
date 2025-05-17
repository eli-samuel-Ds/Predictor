namespace Application.Dtos
{
    public class SalidaValidadoDataPredictorDto
    {
        public required List<DataPredictorDto> Items { get; set; }

        public decimal SMAShort { get; set; }
        public decimal SMALong { get; set; }
        public string Trend { get; set; } = string.Empty;
    }
}
