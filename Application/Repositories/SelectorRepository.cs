using Application.Dtos;
using Application.Enums;       

namespace Application.Repositories
{
    public sealed class SelectorRepository
    {
        private SelectorRepository()
        {
            Selector = new SelectorPredictorDto
            {
                Opcion = PredictorType.sma   
            };
        }

        public static SelectorRepository Instance { get; } = new();

        public SelectorPredictorDto Selector { get; set; }
    }
}
