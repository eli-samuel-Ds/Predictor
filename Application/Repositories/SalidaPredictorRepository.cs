using Application.Dtos;

namespace Application.Repositories
{
    public sealed class SalidaPredictorRepository
    {
        private SalidaPredictorRepository()
        {
            ListvalidadoDataPredictor = new SalidaValidadoDataPredictorDto
            {
                Items = new List<DataPredictorDto>(),
                RocValues = new List<decimal?>()
            };
        }

        public static SalidaPredictorRepository Instance { get; } = new SalidaPredictorRepository();

        public SalidaValidadoDataPredictorDto ListvalidadoDataPredictor { get; set; }
    }
}
