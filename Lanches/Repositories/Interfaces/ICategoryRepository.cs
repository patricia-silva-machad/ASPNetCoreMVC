using Lanches.Models;

namespace Lanches.Repositories.Interfaces {
    public interface ICategoriaRepository {
        //definindo uma propriedade somente leitura que vai retornar uma coleção de objetos categorias
        IEnumerable<Categoria> Categorias { get; }
    }
}
