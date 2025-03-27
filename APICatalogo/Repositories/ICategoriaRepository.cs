using APICatalogo.Models;

namespace APICatalogo.Repositories
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> GetCategorias();
        Categoria GetCategoriaId(int id);
        Categoria Create(Categoria categoria); 
        Categoria Update(Categoria categoria);
        Categoria Delete(int id);   
    }
}
