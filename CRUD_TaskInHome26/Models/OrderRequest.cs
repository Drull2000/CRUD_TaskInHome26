using System.ComponentModel.DataAnnotations;

namespace CRUD_TaskInHome26.Models;

public class OrderRequest
{
    [Required(ErrorMessage = "Number is required.")]
    public string? Number { get; set; }

    [Required(ErrorMessage = "Total is required.")]
    [Range(typeof(decimal), "0", "79228162514264337593543950335", ErrorMessage = "Total must be a non-negative decimal value.")]
    public decimal? Total { get; set; }
}
