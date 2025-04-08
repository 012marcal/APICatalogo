using APICatalogo.Context;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : Controller
    {

        private readonly IRepository<Categoria> _repository;
        private readonly ILogger<CategoriasController> _logger;


        public CategoriasController(IRepository<Categoria> repository, ILogger<CategoriasController> logger)
        {
            _repository = repository;
            _logger = logger;

        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Categoria>> Get() 
        {

            var categoria = _repository.GetAll();

            return Ok(categoria);
            
        }


        [HttpGet("{id:int}")]
        public ActionResult Get(int id) 
        {



            var categoria = _repository.Get(c => c.CategoriaId == id );

            if (categoria == null)
            {
                _logger.LogWarning($"{id} não encontrado");
                return NotFound($"{id} não encontrado");
            }

            return Ok(categoria);
        }


        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {

            
            var categoriaCriada = _repository.Create(categoria);

            return Ok(categoriaCriada) ;


        }

        [HttpPut("{id:int}")]
        public  ActionResult Put(int id, Categoria categoria)
        {
           _repository.Update(categoria);

            return Ok(categoria);

        }

        [HttpDelete("{id:int}")]
        public  ActionResult Delete(int id) 
        {

            var categoria = _repository.Get(c => c.CategoriaId == id);
            if (categoria == null)
            {
                _logger.LogWarning($"id {id} não encontrado");
                return NotFound($"id {id} não encontrado");
            }
           _repository.Delete(categoria);

            return Ok(categoria);
        }
    }
}
