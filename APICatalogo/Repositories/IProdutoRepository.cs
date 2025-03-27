using APICatalogo.Models;
using System.Runtime.CompilerServices;

namespace APICatalogo.Repositories
{
    public interface IProdutoRepository
    {

        IEnumerable<Produto> GetProdutos();
        Produto GetProdutoId(int id);
        Produto Create(Produto produto);
        Produto Update(Produto produto);
        Produto Delete(int id);   

    }
}
