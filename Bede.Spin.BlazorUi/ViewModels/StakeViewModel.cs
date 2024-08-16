using System.ComponentModel.DataAnnotations;

namespace Bede.Spin.BlazorUi.ViewModels;

public class StakeViewModel
{
    [Range(0, 999, ErrorMessage = "Stake must be between 0 and 999.")]
    public decimal Stake { get; set; } = 10;
}