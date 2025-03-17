using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ProdutosController(DatabaseContext context)
        {

            _context = context;

        }


        [HttpGet]
        public async Task< ActionResult<IEnumerable<Produto>>> Get()
        {

            var produtos = await _context.Produtos.AsNoTracking().ToListAsync();

            if (produtos is null)
            {
                return NotFound();

            }
            return produtos;

        }


        [HttpGet("{id:int:min(1)}", Name = "obterProduto")]
         public async Task<ActionResult<Produto>> Get(int id)
        {

            var produto = await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.ProdutoId == id) ;

            if(produto is null)
            {
               return NotFound("Produto não existe");
            }
      
            return produto;
         

        }


        [HttpPost]
        public async Task< ActionResult >Post(Produto produto)
        {

            if (produto is null) 
            {
                return BadRequest("Nemhum Produto informado");  
            }

                  _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            return new CreatedAtRouteResult("obterProduto", new { id = produto.ProdutoId }, produto);

        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult> Put(int id, Produto produto) 
        {
        
            if (id != produto.ProdutoId) 
            {
                return BadRequest("este id não existe...");

            }

            _context.Entry(produto).State = EntityState.Modified;
            await _context.SaveChangesAsync(); 

            return Ok(produto);
        }


        [HttpDelete("{id:int:min(1)}")]
        public async Task< ActionResult >Delete(int id )
        {

            var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.ProdutoId == id);

            if(produto is null)
            {
                return NotFound("Produto não localizado...");
            }
            
                  _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync(); 
            return Ok(produto); 
        }
    }
}
