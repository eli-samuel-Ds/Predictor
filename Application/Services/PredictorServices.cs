using Application.Dtos;
using Application.Enums;
using Application.Repositories;

namespace Application.Services
{
    public class PredictorServices
    {
        private readonly PredictorRepository _repo;

        public PredictorServices()
        {
            _repo = PredictorRepository.Instance;
        }

        public void Save(ValidadoDataPredictorDto dto, SelectorPredictorDto selector)
        {
            switch (selector.Opcion)
            {
                case
                    break;
                case
                    break;
                case
                    break;
                default:
                    break;
            }
            //_repo.ListvalidadoDataPredictor = dto;
        }

        public ValidadoDataPredictorDto Get()
        {
            return _repo.ListvalidadoDataPredictor;
        }
    }
}
