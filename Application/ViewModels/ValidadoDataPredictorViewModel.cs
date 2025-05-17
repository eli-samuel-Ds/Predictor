namespace Application.ViewModels
{
    public class ValidadoDataPredictorViewModel
    {
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
