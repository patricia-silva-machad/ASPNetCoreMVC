using Lanches.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lanches.Components {
    public class CategoriaMenu : ViewComponent 
    {
        private readonly ICategoriaRepository _CategoriaRepository;

        public CategoriaMenu(ICategoriaRepository CategoriaRepository) 
        {
            _CategoriaRepository = CategoriaRepository;
        }

        public IViewComponentResult Invoke() {

            var categoria = _CategoriaRepository.Categorias.OrderBy(c => c.CategoriaNome);

            return View(categoria);
        }
    }
}
