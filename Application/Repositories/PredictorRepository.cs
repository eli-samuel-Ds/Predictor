using Application.Dtos;

namespace Application.Repositories
{
    public sealed class PredictorRepository
    {
        private PredictorRepository() { }

        public static PredictorRepository Instance { get; } = new();

        public ValidadoDataPredictorDto ListvalidadoDataPredictor { get; set;  }
    }
}
