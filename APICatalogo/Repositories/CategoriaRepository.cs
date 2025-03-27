using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly DatabaseContext _context;


        public CategoriaRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Categoria> GetCategorias()
        {
            var categorias = _context.Categorias.AsNoTracking().ToList();



            return categorias;

        }

        public Categoria GetCategoriaId(int id)
        {
            return _context.Categorias.Find(id);
        }


        public Categoria Create(Categoria categoria)
        {

            if(categoria == null)
            {
                throw new ArgumentNullException(nameof(categoria));
            }
            _context.Add(categoria);
            _context.SaveChanges();
            return categoria;
        }

        public Categoria Update(Categoria categoria)
        {
            if (categoria == null)
            {
                throw new ArgumentNullException(nameof(categoria));
            }

            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges(); 
            return categoria;   

        }   
        public Categoria Delete(int id)
        {
           var categoria = _context.Categorias.Find(id);

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();
            return categoria;   

        }
    }
}
