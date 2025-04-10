using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace APICatalogo.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase

    {
        //private readonly IRepository<Produto> _repository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly ILogger<ProdutosController> _logger;

        public ProdutosController(IProdutoRepository produtoRepository, ILogger<ProdutosController> logger)//IRepository<Produto> repository 
        {

            //_repository = repository;    
            _produtoRepository = produtoRepository;
            _logger = logger;   

        }




        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {

            var produtos = _produtoRepository.GetAll();
            return Ok(produtos);

        }

        [HttpGet("produtosCategoria/{id}")]
        public ActionResult<IEnumerable<Produto>> GetProdutosCategorias(int categoriaId)
        {


            var produtosCategorias = _produtoRepository.GetProdutosCategoria(categoriaId);

            if (produtosCategorias is null) { 
            
                return NotFound($"Categoria {categoriaId} não encontrada.");
            
            }
            
            return Ok(produtosCategorias);


        }


        [HttpGet("{id:int:min(1)}", Name = "obterProduto")]
         public ActionResult<Produto> Get(int id)
        {
            var produto = _produtoRepository.Get(p => p.ProdutoId == id);


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

            

            var produtoCriado = _produtoRepository.Create(produto);

            return Ok(produtoCriado);  
        }

        [HttpPut("{id:int:min(1)}")]
        public ActionResult Put(int id, Produto produto) 
        {


            var p = _produtoRepository.Get(p => p.ProdutoId ==  id);
       
            if (p is null)
            {
                return NotFound($"id{id} não encontrado");
            }
           
            return Ok(_produtoRepository.Update(produto));
        }


        [HttpDelete("{id:int:min(1)}")]
        public ActionResult Delete(int id )
        {

            var produto = _produtoRepository.Get(p => p.ProdutoId == id);

            if (produto == null)
            {

                _logger.LogWarning($"No produto id {id}");
                return NotFound($"nao tem o produto {id}");

            }

            _produtoRepository.Delete(produto);

            return Ok(produto);
        }
    }
}
