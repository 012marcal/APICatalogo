using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ProdutosController(DatabaseContext context)
        {

            _context = context;

        }


        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {

            var produtos = _context.Produtos.ToList();

            if (produtos is null)
            {
                return NotFound();

            }
            return produtos;

        }


        [HttpGet("{id:int}", Name = "obterProduto")]
         public ActionResult<Produto> Get(int id)
        {

            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id) ;

            if(produto is null)
            {
               return NotFound("Produto não existe");
            }
      
            return produto;
         

        }


        [HttpPost]
        public ActionResult Post(Produto produto)
        {

            if (produto is null) 
            {
                return BadRequest("Nemhum Produto informado");  
            }

            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return new CreatedAtRouteResult("obterProduto", new { id = produto.ProdutoId }, produto);

        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto) 
        {
        
            if (id != produto.ProdutoId) 
            {
                return BadRequest("este id já existe...");

            }

            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(produto);
        }


        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id )
        {

            var produto = _context.Produtos.FirstOrDefault(p=> p.ProdutoId == id );

            if(produto is null)
            {
                return NotFound("Produto não localizado...");
            }
            
            _context.Produtos.Remove(produto);
            _context.SaveChanges(); 
            return Ok(produto); 
        }
    }
}
