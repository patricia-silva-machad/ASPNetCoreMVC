using Lanches.Context;
using Lanches.Models;
using Lanches.Repositories.Interfaces;


// classe concreta que implementa a interface
namespace Lanches.Repositories {
    public class CategoryRepository : ICategoryRepository 
    {   
        //injetando uma instancia do contexto no construtor do repositorio
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context) {
            _context = context;
        }

        //implementando a propriedade categoria, retornando todas as categorias da tabela
        public IEnumerable<Category> Categories => _context.Categories;
    }
}
