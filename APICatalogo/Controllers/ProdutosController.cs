using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace APICatalogo.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase

    {
        private readonly IUnitOfWork _uof;
        //private readonly IRepository<Produto> _repository;
        //private readonly IProdutoRepository _produtoRepository;
        private readonly ILogger<ProdutosController> _logger;

        public ProdutosController(IUnitOfWork uof, ILogger<ProdutosController> logger)//IRepository<Produto> repository IProdutoRepository produtoRepository
        {
            _uof = uof;
            //_repository = repository;    
            //_produtoRepository = produtoRepository;
            _logger = logger;   

        }




        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {

            var produtos = _uof.ProdutoRepository.GetAll();
            return Ok(produtos);

        }

        [HttpGet("produtosCategoria/{categoriaId}")]
        public ActionResult<IEnumerable<Produto>> GetProdutosCategorias(int categoriaId)
        {


            var produtosCategorias = _uof.ProdutoRepository.GetProdutosCategoria(categoriaId);

            if (produtosCategorias == null) { 
            
                return NotFound($"Categoria {categoriaId} não encontrada.");
            
            }
            
            return Ok(produtosCategorias);


        }


        [HttpGet("{id:int:min(1)}", Name = "obterProduto")]
         public ActionResult<Produto> Get(int id)
        {
            var produto = _uof.ProdutoRepository.Get(p => p.ProdutoId == id);


            if (produto == null)
            {
                _logger.LogWarning($"{id} não encontrado");
                return NotFound($"{id} não encontrado");
            }

            return Ok(produto);
        }


        [HttpPost]
        public ActionResult Post(Produto produto)
        {

      
            var produtoCriado = _uof.ProdutoRepository.Create(produto);
            _uof.Commit();
            return Ok(produtoCriado);  
        }

        [HttpPut("{id:int:min(1)}")]
        public ActionResult Put(int id, Produto produto) 
        {
            if (id != produto.ProdutoId) 
            {
                return BadRequest($"id{id} invalido");
            }
            _uof.ProdutoRepository.Update(produto);
            _uof.Commit();
            return Ok(produto);
        }


        [HttpDelete("{id:int:min(1)}")]
        public ActionResult Delete(int id )
        {

            var produto = _uof.ProdutoRepository.Get(p => p.ProdutoId == id);

            if (produto == null)
            {

                _logger.LogWarning($"No produto id {id}");
                return NotFound($"nao tem o produto {id}");

            }

            _uof.ProdutoRepository.Delete(produto);
            _uof.Commit();

            return Ok(produto);
        }
    }
}
