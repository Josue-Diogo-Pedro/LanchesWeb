using Lanches.Models;

namespace Lanches.ViewModels;

public class HomeViewModel
{
    public IEnumerable<Lanche> LanchesPreferidos { get; set; }
}
