using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels
{
    public class DataPredictorViewModel
    {
        [Required(ErrorMessage = "Campo obligatorio")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public decimal Valor { get; set; }
    }
}
