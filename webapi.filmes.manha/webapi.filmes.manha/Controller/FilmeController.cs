using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.filmes.manha.Domains;
using webapi.filmes.manha.Interfaces;
using webapi.filmes.manha.Repositories;

namespace webapi.filmes.manha.Controller

{
    //Define que a rota de uma requisição será no seguinte formato 
    //dominio/api/nomeController 
    //exemplo: http://localhost:5000/api/filme
    [Route("api/[controller]")]

    //Definir que é um controlador API
    [ApiController]
    [Produces("application/json")]

    public class FilmeController : ControllerBase
    {
        private IFilmeRepository _filmeRepository { get; set; }

        public FilmeController()
        {
            _filmeRepository = new FilmeRepository();
        }

        /// <summary>
        /// Endpoint para Listar a tabela Filme
        /// </summary>
        /// <returns></returns>

        [HttpGet]

        public IActionResult Get()
        {
            try
            {
                //Cria uma lista que recebe os dados da requisição
                List<FilmeDomains> ListaGeneros = _filmeRepository.ListarTodos();

                //Retorna a lista no formato JSON com o status code Ok(200)  
                return Ok(ListaGeneros);
            }
            catch (Exception erro)
            {
                //Retorna um status code BadRequest(400) e a mensagem do erro
                return BadRequest(erro.Message);
            }

        }

        /// <summary>
        /// Endpoint que acona o método de cadastro de Filme
        /// </summary>
        /// <param name="filmeNovo"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(FilmeDomains filmeNovo)
        {
            try
            {
                _filmeRepository.Cadastre(filmeNovo);

                return StatusCode(201);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Endpoint deletar cadastro de filme
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Deletar(int id)
        {
            try
            {
                _filmeRepository.Deletar(id);

                return StatusCode(201);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Procurar filme pela a Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult SearchById(int id)
        {
            try
            {
                FilmeDomains filmeBuscado = _filmeRepository.BuscarPorId(id);

                if(filmeBuscado != null)
                {
                    return Ok(filmeBuscado);
                }
                else
                {
                    return NotFound("Nenhum Filme foi encontrado");
                }
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);    
            }
        }


        /// <summary>
        /// Endpoint para Atualizar os filmes pela URL
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Filme"></param>
        /// <returns></returns>
        [HttpPut("{id}")]

        public IActionResult Put(int id, FilmeDomains Filme)
        {

            try
            {
                _filmeRepository.AtulaizarIdUrl(id, Filme);
                return StatusCode(200);
            }
            catch(Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Endepoint que atualiza o Corpo do Filme
        /// </summary>
        /// <param name="Filme"></param>
        /// <returns></returns>
       
        [HttpPut]
        public IActionResult Put(FilmeDomains Filme)
        {
            try
            {
                _filmeRepository.AtualizarIdCorpo(Filme);
                return StatusCode(200);
            }
            catch(Exception erro)
            {
                return BadRequest(erro.Message);
            }

        }
    }
}
