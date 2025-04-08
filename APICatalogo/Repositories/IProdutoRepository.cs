using APICatalogo.Models;
using System.Runtime.CompilerServices;

namespace APICatalogo.Repositories
{
    public interface IProdutoRepository : IRepository<Produto>
    {

        IEnumerable<Produto> GetProdutosCategoria(int id);

    }
}
