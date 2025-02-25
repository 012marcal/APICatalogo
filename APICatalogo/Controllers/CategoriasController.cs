using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CategoriasController : Controller
    {
      private readonly DatabaseContext _context;


        public CategoriasController(DatabaseContext contexto)
        {
                _context = contexto;    
        }



        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {

                return _context.Categorias.Include(p => p.Produtos).ToList();
            

        }


        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get() 
        { 
            var categoria = _context.Categorias.AsNoTracking().ToList();

            if(categoria is null  ) 
            {
                
                return NotFound();
            }
            return categoria;
            
        }


        [HttpGet("{id:int}", Name = "obterCategoria")]
        public ActionResult<Categoria> Get(int id) 
        {

            var categoria = _context.Categorias.AsNoTracking().FirstOrDefault(p => p.CategoriaId == id);

            if( categoria is null )
            {

                return NotFound();

            }
            return categoria;


        }


        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            
            if(categoria is null )
            {

                return NotFound("Nemhum Categoria Encontrado...");  
            }

            _context.Add(categoria);
            _context.SaveChanges();

            return new CreatedAtRouteResult("obterCategoria", new {id = categoria.CategoriaId }, categoria);


        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {

            if(id != categoria.CategoriaId)
            {
                BadRequest("Id não encontrado...");
            }

            
            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(categoria);

        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id) 
        {
            
            var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
            
            if(categoria is null ) 
            {
                return NotFound("Id não encontrado...");


            }

            _context.Categorias.Remove(categoria);
            _context.SaveChanges(); 
            return Ok(categoria);   

            
        }
    }
}
