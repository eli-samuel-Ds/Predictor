using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels
{
    public class DataPredictorViewModel
    {
        [Required(ErrorMessage = "Campo obligatorio")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [Range(0.0000000001, double.MaxValue, ErrorMessage = "Dato debe ser mayor que cero")]
        public decimal Valor { get; set; }
    }
}
