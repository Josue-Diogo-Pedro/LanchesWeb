using Lanches.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lanches.Components;

public class CategoriaMenu : ViewComponent
{
    private readonly ICategoriaRepository _categoriaRepository;

	public CategoriaMenu(ICategoriaRepository categoriaRepository)
	{
		_categoriaRepository = categoriaRepository;
	}

	public IViewComponentResult Invoke()
	{
		var categorias = _categoriaRepository.Categorias.OrderBy(l => l.CategoriaNome);
		return View(categorias);
	}
}
