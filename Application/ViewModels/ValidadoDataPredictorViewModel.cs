using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels
{
    public class ValidadoDataPredictorViewModel
    {

        [MinLength(20, ErrorMessage = "Debe de tener 20 datos")]
        [MaxLength(20, ErrorMessage = "Debe de tener 20 datos")]
        public List<DataPredictorViewModel> Items { get; set; } = new List<DataPredictorViewModel>();

        public ValidadoDataPredictorViewModel()
        {
            for (int i = 0; i < 20; i++)
            {
                Items.Add(new DataPredictorViewModel());
            }
        }
    }
}
