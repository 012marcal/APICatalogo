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

        private readonly IUnitOfWork _uof;
       // private readonly ICategoriaRepository _repository;
        private readonly ILogger<CategoriasController> _logger;


        public CategoriasController(IUnitOfWork uof, ILogger<CategoriasController> logger)
        {
            _uof = uof;
            _logger = logger;

        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Categoria>> Get() 
        {

            var categoria = _uof.CategoriaRepository.GetAll();

            return Ok(categoria);
            
        }


        [HttpGet("{id:int}")]
        public ActionResult Get(int id) 
        {



            var categoria = _uof.CategoriaRepository.Get(c => c.CategoriaId == id );

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

            
            var categoriaCriada = _uof.CategoriaRepository.Create(categoria);
            _uof.Commit();

            return Ok(categoriaCriada) ;


        }

        [HttpPut("{id:int}")]
        public  ActionResult Put(int id, Categoria categoria)
        {
           _uof.CategoriaRepository.Update(categoria);
            _uof.Commit();

            return Ok(categoria);

        }

        [HttpDelete("{id:int}")]
        public  ActionResult Delete(int id) 
        {

            var categoria = _uof.CategoriaRepository.Get(c => c.CategoriaId == id);
            if (categoria == null)
            {
                _logger.LogWarning($"id {id} não encontrado");
                return NotFound($"id {id} não encontrado");
            }
           _uof.CategoriaRepository.Delete(categoria);

            _uof.Commit();

            return Ok(categoria);
        }
    }
}
