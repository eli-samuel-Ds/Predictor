using Application.Dtos;
using Application.Repositories;

namespace Application.Services
{
    public class SelectorPredictorServices
    {
        private readonly SelectorRepository _repo;
        public SelectorPredictorServices() => _repo = SelectorRepository.Instance;

        public void Save(SelectorPredictorDto dto) => _repo.Selector = dto;
        public SelectorPredictorDto Get() => _repo.Selector;
    }
}
