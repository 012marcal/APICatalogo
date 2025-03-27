using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace APICatalogo.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {

        private readonly DatabaseContext _context;

        public ProdutoRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Produto> GetProdutos()
        {
            return _context.Produtos.ToList();
        }

        public Produto GetProdutoId(int id)
        {
            return _context.Produtos.Find(id);
        }


        public Produto Create(Produto produto)
        {

            if (produto == null)
            {
                throw new ArgumentNullException(nameof(produto));
            }
           
            _context.Produtos.Add(produto);
            _context.SaveChanges(); 
            return produto; 
        }

        public Produto Update(Produto produto)
        {
          
                _context.Entry(produto).State = EntityState.Modified;
                _context.SaveChanges();
                return produto;
            
        }

        public Produto Delete(int id)
        {
            var produto = _context.Produtos.Find(id);

            _context.Remove(produto);
            _context.SaveChanges();
            return produto;

        }
    }
}
