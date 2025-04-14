using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(DatabaseContext context) : base(context)
        {
        }

        public IEnumerable<Produto> GetProdutosCategoria(int categoriaId)
        {
            return GetAll().Where(c => c.CategoriaId == categoriaId);
        }
    }
}
