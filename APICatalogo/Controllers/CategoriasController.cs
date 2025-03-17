using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : Controller
    {
      private readonly DatabaseContext _context;


        public CategoriasController(DatabaseContext contexto)
        {
                _context = contexto;    
        }



        [HttpGet("produtos")]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoriasProdutos()
        {

                return await _context.Categorias.AsNoTracking().Include(p => p.Produtos).ToListAsync();
            

        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> Get() 
        { 
            var categoria = await _context.Categorias.AsNoTracking().ToListAsync();

            if(categoria is null  ) 
            {
                
                return NotFound();
            }
            return categoria;
            
        }


        [HttpGet("{id:int}", Name = "obterCategoria")]
        public async Task<ActionResult<Categoria>> Get(int id) 
        {

            var categoria = await _context.Categorias.AsNoTracking().FirstOrDefaultAsync(p => p.CategoriaId == id);

            if( categoria is null )
            {

                return NotFound();

            }
            return categoria;


        }


        [HttpPost]
        public async Task<ActionResult>Post(Categoria categoria)
        {
            
            if(categoria is null )
            {

                return NotFound("Nemhum Categoria Encontrado...");  
            }

            await _context.AddAsync(categoria);
            await _context.SaveChangesAsync();

            return new CreatedAtRouteResult("obterCategoria", new {id = categoria.CategoriaId }, categoria);


        }

        [HttpPut("{id:int}")]
        public async Task< ActionResult> Put(int id, Categoria categoria)
        {

            if(id != categoria.CategoriaId)
            {
                BadRequest("Id não encontrado...");
            }

            
                 _context.Entry(categoria).State = EntityState.Modified;
           await _context.SaveChangesAsync();

            return Ok(categoria);

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id) 
        {
            
            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.CategoriaId == id);
            
            if(categoria is null ) 
            {
                return NotFound("Id não encontrado...");


            }

                  _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync(); 
            return Ok(categoria);   

            
        }
    }
}
