using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace APICatalogo.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoRepository _repository;
        private readonly ILogger<ProdutosController> _logger;

        public ProdutosController(IProdutoRepository repository, ILogger<ProdutosController> logger)
        {
            _repository = repository;
            _logger = logger;   

        }




        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {

            var produtos = _repository.GetProdutos();
            return Ok(produtos);

        }


        [HttpGet("{id:int:min(1)}", Name = "obterProduto")]
         public ActionResult<Produto> Get(int id)
        {
            var produto = _repository.GetProdutoId(id);


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

            

            var produtoCriado = _repository.Create(produto);

            return Ok(produtoCriado);  
        }

        [HttpPut("{id:int:min(1)}")]
        public ActionResult Put(int id, Produto produto) 
        {

            
            var p  = _repository.GetProdutoId(id);
       
            if (p is null)
            {
                return NotFound($"id{id} não encontrado");
            }
            var produtoEditado = _repository.Update(produto);
            return Ok(produtoEditado);
        }


        [HttpDelete("{id:int:min(1)}")]
        public ActionResult Delete(int id )
        {

            var produto = _repository.GetProdutoId(id);

            if (produto == null)
            {

                _logger.LogWarning($"No produto id {id}");
                return NotFound($"nao tem o produto {id}");

            }

            _repository.Delete(id);

            return Ok(produto);
        }
    }
}
