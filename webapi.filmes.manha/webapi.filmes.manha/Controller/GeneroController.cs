using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.filmes.manha.Domains;
using webapi.filmes.manha.Interfaces;
using webapi.filmes.manha.Repositories;

namespace webapi.filmes.manha.Controller
{
    //Define que a rota de uma requisição será no seguinte formato 
    //dominio/api/nomeController 
    //exemplo: http://localhost:5000/api/genero
    [Route("api/[controller]")]

    //Definir que é um controlador APi
    [ApiController]

    

    //Definir que o tipo de resposta da API será no formato JSON
    [Produces("application/json")]

    //Métodos controlador que herda da controller base
    //Onde seá criado os Endopoins(Rotas)
    public class GeneroController : ControllerBase
    {
        ///<summary>
        ///Objeto_generoRepository que irá receber todos os métos definidos na interface IGeneroRepository
        /// </summary>

        private IGeneroRepository _generoRepository { get; set; }
        /// <summary>
        /// Instancia o objeto generoRepository para que haja referência aos métodos no repositório
        /// </summary>


        public GeneroController()
        {
            _generoRepository = new GeneroRepository();
        }
        /// <summary>
        /// Endpoint que aciona o método ListarTodos no repositório e retorna a resposta para o usúario(front-end)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                //Cria uma lista que recebe os dados da requisição
                List<GeneroDomain> ListaGeneros = _generoRepository.ListarTodos();

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
        /// Endpoint que aciona o método de cadastro de gênero
        /// </summary>
        /// <param name="novoGenero">Objeto recebido na requisição</param>
        /// <returns>Resposta para o usuário(front-end)</returns>
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public IActionResult Post(GeneroDomain novoGenero)
        {
            try
            {
                //Fazendo a chamada para o método cadastrar passando o objeto como parâmetro
                _generoRepository.Cadastrar(novoGenero);

                //Retorna um status code 201 - Created
                return StatusCode(201);
            }
            catch (Exception erro)
            {
                //Retorna um status code 400(BadRequest) e a mensagem do erro
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Endepoint que aciona o método de deletar gênero
        /// </summary>
        /// <param name="Id"> Id do gênero a ser deletr gênero</param>
        /// <returns> Status Code </returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public IActionResult Deletar(int id)
        {
            try
            {
                _generoRepository.Deletar(id);

                return StatusCode(201);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrador, Comum")]
        public IActionResult SearchById(int id)
        {
            try
            {
                //Cria um objeto generoBuscado que irá receber o gênero buscado no banco de dados 
                GeneroDomain generoBuscado = _generoRepository.BuscarPorId(id);

                //Verifica se nenhum genero foi encontrado
                if (generoBuscado != null)
                {
                    //Caos seja encontrado, retorna ao genero buscado com um status code 200 - Ok
                    return Ok(generoBuscado);
                }
                else
                {
                    //Caso não seja encontrado, retorna o gênero buscado com um status code 404 - Not Found com a mensagem personalizada
                    return NotFound("Nenhum Genero foi encontrado");
                }

            }
            catch (Exception erro)
            {
                //Retorna um status code BadRequest(400) e a mensagem do erro
                return BadRequest(erro.Message);
            }

        }

        //**************************************************************************** GET UPDATE PELO URL ******************************************************
        /// <summary>
        /// Endpoint que aciona o metodo ListarPorUrl
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Genero"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        public IActionResult Put(int id, GeneroDomain Genero)
        {
            try
            {
                _generoRepository.AtualizarIdUrl(id, Genero);
                return StatusCode(200);
            }
            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Endepoint que atualiza o corpo do Genero
        /// </summary>
        /// <param name="Genero"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "Administrador")]
        public IActionResult Put(GeneroDomain Genero)
        {
            try
            {
                _generoRepository.AtualizarIdCorpo(Genero);
                return StatusCode(200);
            }
            catch(Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

    }
}
