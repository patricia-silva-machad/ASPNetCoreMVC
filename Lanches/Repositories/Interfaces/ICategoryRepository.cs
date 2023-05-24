using Lanches.Models;

namespace Lanches.Repositories.Interfaces {
    public interface ICategoryRepository {
        //definindo uma propriedade somente leitura que vai retornar uma coleção de objetos categorias
        IEnumerable<Category> Categories { get; }
    }
}
